

namespace Hk.Infrastructures.Validator {
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Validators;

	
	/// <summary>
	/// Provides metadata about a validator.
	/// </summary>
	public interface IValidatorDescriptor {
		/// <summary>
		/// Gets the name display name for a property. 
		/// </summary>
		string GetName(string property);
		
		/// <summary>
		/// Gets a collection of validators grouped by property.
		/// </summary>
		ILookup<string, IPropertyValidator> GetMembersWithValidators();
		
		/// <summary>
		/// Gets validators for a particular property.
		/// </summary>
		IEnumerable<IPropertyValidator> GetValidatorsForMember(string name);

		/// <summary>
		/// Gets rules for a property.
		/// </summary>
		IEnumerable<IValidationRule> GetRulesForMember(string name);
	}
}