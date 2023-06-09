using Microsoft.AspNetCore.Mvc;

namespace Lakeshore.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RunAptosOpenPoController : Controller
    {
        private readonly Serilog.ILogger _logger;
        private readonly Service _service;

        public RunAptosOpenPoController(Serilog.ILogger logger, Service service)
        {
            _logger = logger;
            _service = service;
        }


        [HttpGet("")]
        public Task<ActionResult<bool>> HealthCheck(CancellationToken cancellationToken)
        {
            return Task.FromResult<ActionResult<bool>>(Ok(true));
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                _logger.Information("Post Request Called");
                await _service.ProcessRequest();
                _logger.Information("Post Request Finished");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to process Aptos Open PO Data records: ");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return Ok();
        }
    }
}
