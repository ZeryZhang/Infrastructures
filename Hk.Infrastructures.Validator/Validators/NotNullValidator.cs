

namespace Hk.Infrastructures.Validator.Validators {
	using Resources;

	public class NotNullValidator : PropertyValidator, INotNullValidator {
		public NotNullValidator() : base(() => Messages.notnull_error) {
		}

		protected override bool IsValid(PropertyValidatorContext context) {
			if (context.PropertyValue == null) {
				return false;
			}
			return true;
		}
	}

	public interface INotNullValidator : IPropertyValidator {
	}
}