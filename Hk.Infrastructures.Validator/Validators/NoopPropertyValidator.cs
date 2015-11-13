

namespace Hk.Infrastructures.Validator.Validators {
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using Hk.Infrastructures.Validator.Internal;
	using Resources;
	using Results;

	public abstract class NoopPropertyValidator : IPropertyValidator {
		public IStringSource ErrorMessageSource {
			get { return null; }
			set { }
		}

		public virtual bool IsAsync {
			get { return false; }
		}

		public abstract IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context);

		public virtual Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context) {
			return TaskHelpers.FromResult(Validate(context));
		}

		public virtual ICollection<Func<object, object, object>> CustomMessageFormatArguments {
			get { return new List<Func<object, object, object>>(); }
		}

		public virtual bool SupportsStandaloneValidation {
			get { return false; }
		}

		public Func<object, object> CustomStateProvider {
			get { return null; }
			set { }
		}
	}
}