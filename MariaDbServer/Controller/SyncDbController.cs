using Microsoft.AspNetCore.Mvc;
using Dotmim.Sync.Web.Server;

namespace MariaDbServer.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncDbController : ControllerBase
    {
        private WebServerAgent webServerAgent;

        public SyncDbController(WebServerAgent webServerAgent)
        {
            this.webServerAgent = webServerAgent;
        }

        [HttpPost]
        public Task Post()
        {
            return webServerAgent.HandleRequestAsync(HttpContext);
        }
    }
}