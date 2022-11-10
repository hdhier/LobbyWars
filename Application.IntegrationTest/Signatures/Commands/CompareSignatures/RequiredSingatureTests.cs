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
    public class RequiredSingatureTests
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
            var command = new RequiredSigtnatureCommand() 
            { 
                Contract1 = "tes", 
                Contract2 = "te#" 
            };

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task ShouldGetKROL()
        {
            var command = new RequiredSigtnatureCommand()
            {
                Contract1 = "VKV",
                Contract2 = "NV#"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Should().Be("El rol necesario será K");
        }

        [Test]
        public async Task ShouldGetNROL()
        {
            var command = new RequiredSigtnatureCommand()
            {
                Contract1 = "VVV",
                Contract2 = "V#V"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Should().Be("El rol necesario será N");
        }

        [Test]
        public async Task ShouldGetVROL()
        {
            var command = new RequiredSigtnatureCommand()
            {
                Contract1 = "#NN",
                Contract2 = "NVV"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Should().Be("El rol necesario será V");
        }

        [Test]
        public async Task ShouldGetContractWithHashTagIsBigger()
        {
            var command = new RequiredSigtnatureCommand()
            {
                Contract1 = "#KK",
                Contract2 = "NVV"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Should().Be("El contrato con # ya es el de mayor puntuación");
        }

        [Test]
        public async Task ShouldGetIsNotPossibleToExceedTheValueWithAnyRol()
        {
            var command = new RequiredSigtnatureCommand()
            {
                Contract1 = "#VV",
                Contract2 = "KKK"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Should().Be("No es posible superar el valor de la firma con ningun ROL");
        }
    }
}
