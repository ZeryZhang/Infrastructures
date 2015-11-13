

namespace Hk.Infrastructures.Validator.TestHelper {
	using System;
	using System.Linq;
	using System.Linq.Expressions;

	public class ValidatorTester<T, TValue> where T : class {
		private readonly IValidator<T> validator;
		private readonly TValue value;
		private readonly MemberAccessor<T, TValue> accessor;
		private readonly string ruleSet;

		public ValidatorTester(Expression<Func<T, TValue>> expression, IValidator<T> validator, TValue value, string ruleSet = null) {
			this.validator = validator;
			this.value = value;
			accessor = expression;
		this.ruleSet = ruleSet;
		}

		public void ValidateNoError(T instanceToValidate) {
			accessor.Set(instanceToValidate, value);
			var count = validator.Validate(instanceToValidate, ruleSet: ruleSet).Errors.Count(x => x.PropertyName == accessor.Member.Name);

			if (count > 0) {
				throw new ValidationTestException(string.Format("Expected no validation errors for property {0}", accessor.Member.Name));
			}
		}

		public void ValidateError(T instanceToValidate) {
			accessor.Set(instanceToValidate, value);
			var count = validator.Validate(instanceToValidate, ruleSet: ruleSet).Errors.Count(x => x.PropertyName == accessor.Member.Name);

			if (count == 0) {
				throw new ValidationTestException(string.Format("Expected a validation error for property {0}", accessor.Member.Name));
			}
		}
	}
}