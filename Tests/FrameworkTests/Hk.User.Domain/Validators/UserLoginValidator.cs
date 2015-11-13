using System;
using Hk.Infrastructures.Core.Validators;
using Hk.Infrastructures.Validator;
using Hk.User.Domain.Entities;

namespace Hk.User.Domain.Validators
{
    public class UserLoginValidator : BaseValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.UserId)
               .NotNull()
               .WithMessage("weeeeeeeeeeeeeeee");
            RuleFor(x => x.LoginName)
                .NotEqual("admin")
                .WithMessage("wwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        }
    }
}
