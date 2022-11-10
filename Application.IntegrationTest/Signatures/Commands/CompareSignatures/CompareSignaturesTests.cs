using Application.Signatures.Commands.CompareSignatures;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTest.Signatures.Commands.CompareSignatures
{
    public class CompareSignaturesTests
    {
        private static IServiceScopeFactory _scopeFactory = null!;
        private static WebApplicationFactory<Program> _factory = null!;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = _factory.Services.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        [Test]
        public async Task ShouldReturnValidationExceptionForRolNotAllowed()
        {
            var command = new CompareSignaturesCommand() 
            { 
                Contract1 = "tes", 
                Contract2 = "tes" 
            };

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task ShouldCompareSignatures()
        {
            var command = new CompareSignaturesCommand()
            {
                Contract1 = "NKV",
                Contract2 = "KKN"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Should().Be("Contract2 wins!");
        }
    }
}
