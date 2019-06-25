using Apka2.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.Services.Validators
{
    public class CreateUserValidator : AbstractValidator<User>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5);
            
        }

        



    }
}
