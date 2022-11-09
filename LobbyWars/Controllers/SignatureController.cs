using Application.Signatures.Commands.CompareSignatures;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LobbyWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignatureController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SignatureController> _logger;


        public SignatureController(ILogger<SignatureController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("CompareSignatures")]
        public async Task<ActionResult<string>> CompareSignatures(CompareSignaturesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("SignatureRequired")]
        public async Task<ActionResult<string>> SignatureRequired(RequiredSigtnatureCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}