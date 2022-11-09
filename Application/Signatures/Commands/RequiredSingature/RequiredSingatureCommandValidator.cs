using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Signatures.Commands.CompareSignatures
{
    public class RequiredSigtnatureCommandValidator : AbstractValidator<RequiredSigtnatureCommand>
    {
        public RequiredSigtnatureCommandValidator()
        {
            RuleFor(v => v.Contract1)
            .MaximumLength(3).WithMessage("Contract1 must not exceed 3 characters.")
            .NotEmpty().WithMessage("Contract1 is required.");

            RuleFor(v => v.Contract2)
            .MaximumLength(3).WithMessage("Contract2 must not exceed 3 characters.")
            .NotEmpty().WithMessage("Contract2 is required.");

            RuleFor(v => new { v.Contract1, v.Contract2 })
            .Must(x => ValidRoles(x.Contract1, x.Contract2)).WithMessage("Las dos firmas no pueden contener el caracter #");
        }

        private bool ValidRoles(string contract1, string contract2)
        {
            if(contract1.Contains("#") && contract2.Contains("#"))
            {
                return false;
            }

            return true;
        }
    }
}
