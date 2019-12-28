using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Components.API.Infrastructure.Repositories;
using Components.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Components.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComponentsController : ControllerBase
    {
        ILogger<ComponentsController> _logger;
        IWorkflowComponentDataRepository<WorkflowComponent> _workflowRepository;

        public ComponentsController(
            ILogger<ComponentsController> logger, 
            IWorkflowComponentDataRepository<WorkflowComponent> workflowRepository)
        {
            _logger = logger;
            _workflowRepository = workflowRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<WorkflowComponent>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<WorkflowComponent>>> GetAllComponents()
        {
            var componentsList = await _workflowRepository.GetAllItemsAsync();
            
            if(componentsList is null)
            {
                return Ok();
            }

            return componentsList.ToList();
        }
    }
}