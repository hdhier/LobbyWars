using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Signatures.Commands.CompareSignatures
{
    public class CompareSignaturesCommandValidator : AbstractValidator<CompareSignaturesCommand>
    {
        public CompareSignaturesCommandValidator()
        {
            RuleFor(v => v.Contract1)
            .MaximumLength(3).WithMessage("Contract1 must not exceed 3 characters.")
            .NotEmpty().WithMessage("Contract1 is required.");

            RuleFor(v => v.Contract2)
            .MaximumLength(3).WithMessage("Contract2 must not exceed 3 characters.")
            .NotEmpty().WithMessage("Contract2 is required.");
        }
    }
}
