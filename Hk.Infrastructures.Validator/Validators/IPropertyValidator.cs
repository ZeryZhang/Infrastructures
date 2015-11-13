

namespace Hk.Infrastructures.Validator.Validators {
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Resources;
	using Results;

	/// <summary>
	/// A custom property validator.
	/// This interface should not be implemented directly in your code as it is subject to change.
	/// Please inherit from <see cref="PropertyValidator">PropertyValidator</see> instead.
	/// </summary>
	public interface IPropertyValidator {
		bool IsAsync { get; }

		IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context);

		Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context);

		/// <summary>
		/// Custom message arguments. 
		/// Arg 1: Instance being validated
		/// Arg 2: Property value
		/// </summary>
		ICollection<Func<object, object, object>> CustomMessageFormatArguments { get; }
		Func<object, object> CustomStateProvider { get; set; }
		IStringSource ErrorMessageSource { get; set; }
	}
}