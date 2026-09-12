using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IOptions<AttachmentOptions> _attachmentOptions;
        private readonly IConfiguration _configuration;
        private readonly AttachmentOptions _attachmentOptions1;

        public ConfigController(IOptions<AttachmentOptions> attachmentOptions, IConfiguration configuration, AttachmentOptions attachmentOptions1)
        {

            _attachmentOptions = attachmentOptions;
            _configuration = configuration;
            _attachmentOptions1 = attachmentOptions1;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetConfig()
        {
            var config = new
            {
                AllowedHosts = _configuration["AllowedHosts"],
                _attachmentOptions1.AllowedExtensions,
                _attachmentOptions.Value.EnableCompression,
                _attachmentOptions.Value.MaxSizeInMegaBytes
            };

            return Ok(config);
        }
    }
}
