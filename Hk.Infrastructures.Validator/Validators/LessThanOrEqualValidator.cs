

namespace Hk.Infrastructures.Validator.Validators {
	using System;
	using System.Linq.Expressions;
	using System.Reflection;
	using Attributes;
	using Internal;
	using Resources;

	public class LessThanOrEqualValidator : AbstractComparisonValidator {
		public LessThanOrEqualValidator(IComparable value) : base(value, () => Messages.lessthanorequal_error) {
		}

		public LessThanOrEqualValidator(Func<object, object> valueToCompareFunc, MemberInfo member)
			: base(valueToCompareFunc, member, () => Messages.lessthanorequal_error) {
		}

		public override bool IsValid(IComparable value, IComparable valueToCompare) {
			if (valueToCompare == null)
				return false;

			return Comparer.GetComparisonResult(value, valueToCompare) <= 0;
		}

		public override Comparison Comparison {
			get { return Comparison.LessThanOrEqual; }
		}
	}
}