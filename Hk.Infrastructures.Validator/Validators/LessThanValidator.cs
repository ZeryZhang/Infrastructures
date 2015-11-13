

namespace Hk.Infrastructures.Validator.Validators {
	using System;
	using System.Linq.Expressions;
	using System.Reflection;
	using Attributes;
	using Internal;
	using Resources;

	public class LessThanValidator : AbstractComparisonValidator {
		public LessThanValidator(IComparable value) : base(value, () => Messages.lessthan_error) {
		}

		public LessThanValidator(Func<object, object> valueToCompareFunc, MemberInfo member)
			: base(valueToCompareFunc, member, () => Messages.lessthan_error) {
		}

		public override bool IsValid(IComparable value, IComparable valueToCompare) {
			if (valueToCompare == null)
				return false;

			return Comparer.GetComparisonResult(value, valueToCompare) < 0;
		}

		public override Comparison Comparison {
			get { return Validators.Comparison.LessThan; }
		}
	}
}