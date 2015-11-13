using System;
using Hk.Infrastructures.Validator;

namespace Hk.Infrastructures.Core.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {
        protected BaseValidator()
        {
            PostInitialize();
        }
        protected virtual void PostInitialize()
        {
            
        }
    }
}
