

namespace Hk.Infrastructures.Validator.Validators {
	using System;
	using System.Linq.Expressions;
	using Attributes;
	using Resources;

	public class LengthValidator : PropertyValidator, ILengthValidator {
		public int Min { get; private set; }
		public int Max { get; private set; }

		public LengthValidator(int min, int max) : this(min, max, () => Messages.length_error) {
		}

		public LengthValidator(int min, int max, Expression<Func<string>> errorMessageResourceSelector) : base(errorMessageResourceSelector) {
			Max = max;
			Min = min;

			if (max != -1 && max < min) {
				throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
			}
		}

		protected override bool IsValid(PropertyValidatorContext context) {
			if (context.PropertyValue == null) return true;

			int length = context.PropertyValue.ToString().Length;

			if (length < Min || (length > Max && Max != -1)) {
				context.MessageFormatter
					.AppendArgument("MinLength", Min)
					.AppendArgument("MaxLength", Max)
					.AppendArgument("TotalLength", length);

				return false;
			}

			return true;
		}
	}

	public class ExactLengthValidator : LengthValidator {
		public ExactLengthValidator(int length) : base(length,length, () => Messages.exact_length_error) {
			
		}
	}

    public class MaximumLengthValidator : LengthValidator {
        public MaximumLengthValidator(int max) : this(max, () => Messages.length_error) {

        }

        public MaximumLengthValidator(int max, Expression<Func<string>> errorMessageResourceSelector)
            : base(0, max, errorMessageResourceSelector) {

        }
    }

    public class MinimumLengthValidator : LengthValidator {
        public MinimumLengthValidator(int min) : this(min, () => Messages.length_error) {

        }

        public MinimumLengthValidator(int min, Expression<Func<string>> errorMessageResourceSelector) 
            : base(min, -1, errorMessageResourceSelector) {

        }
    }

	public interface ILengthValidator : IPropertyValidator {
		int Min { get; }
		int Max { get; }
	}
}