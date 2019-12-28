using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Workflow.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        ILogger<ProjectController> _logger;

        public ProjectController(
            ILogger<ProjectController> logger)
        {

        }

        [HttpGet]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public ActionResult<List<string>> GetProject()
        {
            return new List<string>() { "Hellow, world!" };
        }
    }
}