


namespace Hk.Infrastructures.Validator.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Metadata;
    using System.Web.Http.Validation;

    using Hk.Infrastructures.Validator.Attributes;
    using Hk.Infrastructures.Validator.Internal;
    using Hk.Infrastructures.Validator.Validators;

    public delegate ModelValidator CustomModelValidationFactory(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, PropertyRule rule, IPropertyValidator validator);


    public class CustomModelValidatorProvider: ModelValidatorProvider {
		public IValidatorFactory ValidatorFactory { get; set; }

        public CustomModelValidatorProvider(IValidatorFactory validatorFactory = null)
        {
			ValidatorFactory = validatorFactory ?? new AttributedValidatorFactory();
		}

		/// <summary>
		/// Initializes the FluentValidationModelValidatorProvider using the default options and adds it in to the ModelValidatorProviders collection.
		/// </summary>
		public static void Configure(HttpConfiguration configuration, Action<CustomModelValidatorProvider> configurationExpression = null) {
			configurationExpression = configurationExpression ?? delegate { };

			var provider = new CustomModelValidatorProvider();
			configurationExpression(provider);
		    configuration.Services.Replace(typeof(IBodyModelValidator), new CustomBodyModelValidator());
			configuration.Services.Add(typeof(ModelValidatorProvider), provider);
		}

		public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (IsValidatingProperty(metadata)) {
				yield break;
			}

			IValidator validator = ValidatorFactory.GetValidator(metadata.ModelType);
			
			if (validator == null) {
				yield break;
			}

			yield return new CustomModelValidator(validatorProviders, validator);
		}

		protected virtual bool IsValidatingProperty(ModelMetadata metadata) {
			return metadata.ContainerType != null && !string.IsNullOrEmpty(metadata.PropertyName);
		}
	}
}