using Apka2.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Apka2.Services.Validators
{
    public class CreateVoteValidator : AbstractValidator<Vote>
    {
        public CreateVoteValidator()
        {

            RuleFor(x => x.Law)
                .NotEmpty();
            RuleFor(x => x.User)
                .NotEmpty();
            RuleFor(x => x.VoteType)
                .IsInEnum()
                .NotEmpty();
                
                

        }
    }
}
