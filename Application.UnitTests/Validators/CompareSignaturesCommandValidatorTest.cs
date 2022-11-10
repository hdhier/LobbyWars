using Application.Signatures.Commands.CompareSignatures;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Validators
{
    public class CompareSignaturesCommandValidatorTest
    {

        private CompareSignaturesCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CompareSignaturesCommandValidator();
        }


        [Test]
        public async Task ShouldRequireMaximumLengthTo3()
        {
            var command = new CompareSignaturesCommand()
            {
                Contract1 = "VVVV",
                Contract2 = "NNNN"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Contract1);
            result.ShouldHaveValidationErrorFor(x => x.Contract2);
        }

        [Test]
        public async Task ShouldRequireNotEmpty()
        {
            var command = new CompareSignaturesCommand();

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Contract1);
            result.ShouldHaveValidationErrorFor(x => x.Contract2);
        }
    }
}
