

using System.Threading;

namespace Hk.Infrastructures.Validator.Validators {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using Hk.Infrastructures.Validator.Internal;
	using Resources;
	using Results;

	public abstract class PropertyValidator : IPropertyValidator {
		private readonly List<Func<object, object, object>> customFormatArgs = new List<Func<object, object, object>>();
		private IStringSource errorSource;

		public virtual bool IsAsync {
			get { return false; }
		}

		public Func<object, object> CustomStateProvider { get; set; }

		public ICollection<Func<object, object, object>> CustomMessageFormatArguments {
			get { return customFormatArgs; }
		}

		protected PropertyValidator(string errorMessageResourceName, Type errorMessageResourceType) {
			errorSource = new LocalizedStringSource(errorMessageResourceType, errorMessageResourceName, new FallbackAwareResourceAccessorBuilder());
		}

		protected PropertyValidator(string errorMessage) {
			errorSource = new StaticStringSource(errorMessage);
		}

		protected PropertyValidator(Expression<Func<string>> errorMessageResourceSelector) {
			errorSource = LocalizedStringSource.CreateFromExpression(errorMessageResourceSelector, new FallbackAwareResourceAccessorBuilder());
		}

		public IStringSource ErrorMessageSource {
			get { return errorSource; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				errorSource = value;
			}
		}

		public virtual IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context) {
			context.MessageFormatter.AppendPropertyName(context.PropertyDescription);
			context.MessageFormatter.AppendArgument("PropertyValue", context.PropertyValue);

			if (!IsValid(context)) {
				return new[] { CreateValidationError(context) };
			}

			return Enumerable.Empty<ValidationFailure>();
		}

		public virtual Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context) {
			context.MessageFormatter.AppendPropertyName(context.PropertyDescription);
			context.MessageFormatter.AppendArgument("PropertyValue", context.PropertyValue);

			return
				IsValidAsync(context)
				.Then(
					valid => valid ? Enumerable.Empty<ValidationFailure>() : new[] { CreateValidationError(context) }.AsEnumerable(),
					runSynchronously: true
				);
		}

		protected abstract bool IsValid(PropertyValidatorContext context);

		protected virtual Task<bool> IsValidAsync(PropertyValidatorContext context) {
			return TaskHelpers.FromResult(IsValid(context));
		}

		/// <summary>
		/// Creates an error validation result for this validator.
		/// </summary>
		/// <param name="context">The validator context</param>
		/// <returns>Returns an error validation result.</returns>
		protected virtual ValidationFailure CreateValidationError(PropertyValidatorContext context) {
			Func<PropertyValidatorContext, string> errorBuilder = context.Rule.MessageBuilder ?? BuildErrorMessage;
			var error = errorBuilder(context);

			var failure = new ValidationFailure(context.PropertyName, error, context.PropertyValue);
			failure.FormattedMessageArguments = context.MessageFormatter.AdditionalArguments;
			failure.FormattedMessagePlaceholderValues = context.MessageFormatter.PlaceholderValues;
			failure.ErrorCode = errorSource.ResourceName;
			if (CustomStateProvider != null) {
				failure.CustomState = CustomStateProvider(context.Instance);
			}

			return failure;
		}

		string BuildErrorMessage(PropertyValidatorContext context) {
			context.MessageFormatter.AppendAdditionalArguments(
				customFormatArgs.Select(func => func(context.Instance, context.PropertyValue)).ToArray()
				);

			string error = context.MessageFormatter.BuildMessage(errorSource.GetString());
			return error;
		}
	}
}