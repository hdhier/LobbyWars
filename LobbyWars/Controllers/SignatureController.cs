using Microsoft.AspNetCore.Mvc;

namespace LobbyWars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignatureController : ControllerBase
    {
        private readonly ILogger<SignatureController> _logger;

        public SignatureController(ILogger<SignatureController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CompareSignatures()
        {
            return Ok();
        }
    }
}