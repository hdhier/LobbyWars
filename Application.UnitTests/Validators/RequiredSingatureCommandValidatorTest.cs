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
    public class RequiredSingatureCommandValidatorTest
    {

        private RequiredSigtnatureCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new RequiredSigtnatureCommandValidator();
        }


        [Test]
        public async Task ShouldRequireMaximumLengthTo3()
        {
            var command = new RequiredSigtnatureCommand()
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
            var command = new RequiredSigtnatureCommand();

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Contract1);
            result.ShouldHaveValidationErrorFor(x => x.Contract2);
        }

        [Test]
        public async Task ShouldRequireJustOneHashtag()
        {
            var command = new RequiredSigtnatureCommand()
            {
                Contract1 = "VV#",
                Contract2 = "NN#"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => new { x.Contract1, x.Contract2 });
        }

        [Test]
        public async Task ShouldRequireHashtag()
        {
            var command = new RequiredSigtnatureCommand()
            {
                Contract1 = "VVV",
                Contract2 = "NNN"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => new { x.Contract1, x.Contract2 });
        }
    }
}
