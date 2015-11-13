

namespace Hk.Infrastructures.Validator.Internal {
	using System;
	using Validators;

	/// <summary>
	/// Builds a validation rule and constructs a validator.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	public class RuleBuilder<T, TProperty> : IRuleBuilderOptions<T, TProperty>, IRuleBuilderInitial<T, TProperty> {
		readonly PropertyRule rule;

		/// <summary>
		/// The rule being created by this RuleBuilder.
		/// </summary>
		public PropertyRule Rule {
			get { return rule; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="RuleBuilder{T,TProperty}">RuleBuilder</see> class.
		/// </summary>
		public RuleBuilder(PropertyRule rule) {
			this.rule = rule;
		}

		/// <summary>
		/// Sets the validator associated with the rule.
		/// </summary>
		/// <param name="validator">The validator to set</param>
		/// <returns></returns>
		public IRuleBuilderOptions<T, TProperty> SetValidator(IPropertyValidator validator) {
			validator.Guard("Cannot pass a null validator to SetValidator.");
			rule.AddValidator(validator);
			return this;
		}

		/// <summary>
		/// Sets the validator associated with the rule. Use with complex properties where an IValidator instance is already declared for the property type.
		/// </summary>
		/// <param name="validator">The validator to set</param>
		public IRuleBuilderOptions<T, TProperty> SetValidator(IValidator<TProperty> validator) {
			validator.Guard("Cannot pass a null validator to SetValidator");
			SetValidator(new ChildValidatorAdaptor(validator));
			return this;
		}

		IRuleBuilderOptions<T, TProperty> IConfigurable<PropertyRule, IRuleBuilderOptions<T, TProperty>>.Configure(Action<PropertyRule> configurator) {
			configurator(rule);
			return this;
		}

		IRuleBuilderInitial<T, TProperty> IConfigurable<PropertyRule, IRuleBuilderInitial<T, TProperty>>.Configure(Action<PropertyRule> configurator) {
			configurator(rule);
			return this;
		}
	}
}