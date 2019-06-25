using Apka2.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.Services.Validators
{
    public class CreateLawValidator : AbstractValidator<Law>
    {
        public CreateLawValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.LawText)
                .NotEmpty();

            RuleFor(x => x.DateAdd)
                .NotEmpty()
                .Must(BeAValidDate);

            RuleFor(x => x.DateEnd)
                 .NotEmpty()
                 .Must(BeAValidDate);



        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

    }
}
