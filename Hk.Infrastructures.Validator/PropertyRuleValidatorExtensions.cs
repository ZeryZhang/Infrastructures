

namespace Hk.Infrastructures.Validator {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using Internal;
	using Validators;

	public static class  PropertyRuleValidatorExtensions {
		/// <summary>
		/// Replace the first validator of this type and remove all the others.
		/// </summary>
		[Obsolete("Run-time modification of validators is not recommended or supported.")]
		public static void ReplaceRule<T>(this IValidator<T> validators,
		                                  Expression<Func<T, object>> expression,
		                                  IPropertyValidator newValidator) {
			var property = expression.GetMember();
			if(property == null) throw new ArgumentException("Property could not be identified", "expression");
			var type = newValidator.GetType();

			var rules = validators as IEnumerable<IValidationRule>;
			if (rules == null) {
				throw new NotSupportedException(string.Format("Validator '{0}' does not support replacing rules.", validators.GetType().Name));
			}

			// replace the first validator of this type, then remove the others
			bool replaced = false;
			foreach (var rule in rules.OfType<PropertyRule>()) {
				if (rule.Member == property) {
					foreach (var original in rule.Validators.Where(v => v.GetType() == type).ToArray()) {
						if (!replaced) {
							rule.ReplaceValidator(original, newValidator);
							replaced = true;
						}
						else {
							rule.RemoveValidator(original);
						}
					}
				}
			}
		}

		/// <summary>
		/// Remove all validators of the specifed type.
		/// </summary>
		[Obsolete("Run-time modification of validators is not recommended or supported.")]
		public static void RemoveRule<T>(this IValidator<T> validators,
		                                 Expression<Func<T, object>> expression, Type oldValidatorType) {
			var property = expression.GetMember();
			if (property == null) throw new ArgumentException("Property could not be identified", "expression");

			var rules = validators as IEnumerable<IValidationRule>;
			if (rules == null) {
				throw new NotSupportedException(string.Format("Validator '{0}' does not support replacing rules.", validators.GetType().Name));
			}

			foreach (var rule in rules.OfType<PropertyRule>()) {
				if (rule.Member == property) {
					foreach (var original in rule.Validators.Where(v => v.GetType() == oldValidatorType).ToArray()) {
						rule.RemoveValidator(original);
					}
				}
			}
		}

		/// <summary>
		/// Remove all validators for the given property.
		/// </summary>
		[Obsolete("Run-time modification of validators is not recommended or supported.")]		
		public static void ClearRules<T>(this IValidator<T> validators,
		                                 Expression<Func<T, object>> expression) {
			var property = expression.GetMember();
			if (property == null) throw new ArgumentException("Property could not be identified", "expression");

			var rules = validators as IEnumerable<IValidationRule>;
			if (rules == null) {
				throw new NotSupportedException(string.Format("Validator '{0}' does not support replacing rules.", validators.GetType().Name));
			}

			foreach (var rule in rules.OfType<PropertyRule>()) {
				if (rule.Member == property) {
					rule.ClearValidators();
				}
			}
		}
	}
}