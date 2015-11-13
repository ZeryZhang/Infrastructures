

namespace Hk.Infrastructures.Validator.Validators {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Hk.Infrastructures.Validator.Internal;
	using Resources;
	using Results;

	public class DelegatingValidator : IPropertyValidator, IDelegatingValidator {
		private readonly Func<object, bool> condition;
		public IPropertyValidator InnerValidator { get; private set; }

		public virtual bool IsAsync {
			get { return InnerValidator.IsAsync; }
		}

		public DelegatingValidator(Func<object, bool> condition, IPropertyValidator innerValidator) {
			this.condition = condition;
			InnerValidator = innerValidator;
		}

		public IStringSource ErrorMessageSource {
			get { return InnerValidator.ErrorMessageSource; }
			set { InnerValidator.ErrorMessageSource = value; }
		}

		public IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context) {
			if (condition(context.Instance)) {
				return InnerValidator.Validate(context);
			}
			return Enumerable.Empty<ValidationFailure>();
		}

		public Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context) {
			if (condition(context.Instance)) {
			    return InnerValidator.ValidateAsync(context);
			}
			return TaskHelpers.FromResult(Enumerable.Empty<ValidationFailure>());
		}

		public ICollection<Func<object, object, object>> CustomMessageFormatArguments {
			get { return InnerValidator.CustomMessageFormatArguments; }
		}

		public bool SupportsStandaloneValidation {
			get { return false; }
		}

		public Func<object, object> CustomStateProvider {
			get { return InnerValidator.CustomStateProvider; }
			set { InnerValidator.CustomStateProvider = value; }
		}

		IPropertyValidator IDelegatingValidator.InnerValidator {
			get { return InnerValidator; }
		}

		public bool CheckCondition(object instance) {
			return condition(instance);
		}
	}

	public interface IDelegatingValidator : IPropertyValidator {
		IPropertyValidator InnerValidator { get; }
		bool CheckCondition(object instance);
	}
}