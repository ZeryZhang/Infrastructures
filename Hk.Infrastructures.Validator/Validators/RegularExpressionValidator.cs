

namespace Hk.Infrastructures.Validator.Validators {
	using System;
	using System.Text.RegularExpressions;
	using Attributes;
	using Internal;
	using Resources;
	using Results;

	public class RegularExpressionValidator : PropertyValidator, IRegularExpressionValidator {
		readonly string expression;
		readonly Regex regex;

		public RegularExpressionValidator(string expression) : base(() => Messages.regex_error) {
			this.expression = expression;
			regex = new Regex(expression);

		}

		public RegularExpressionValidator(Regex regex) : base(() => Messages.regex_error) {
			this.expression = regex.ToString();
			this.regex = regex;
		}

		public RegularExpressionValidator(string expression, RegexOptions options) : base(() => Messages.regex_error) {
			this.expression = expression;
			this.regex = new Regex(expression, options);
		}

		protected override bool IsValid(PropertyValidatorContext context) {
			if (context.PropertyValue != null && !regex.IsMatch((string)context.PropertyValue)) {
				return false;
			}
			return true;
		}

		public string Expression {
			get { return expression; }
		}
	}

	public interface IRegularExpressionValidator : IPropertyValidator {
		string Expression { get; }
	}
}