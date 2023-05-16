using Api.CarPolicy.Business.Handlers;
using Api.CarPolicy.Model.DataBase;
using Api.CarPolicy.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.CarPolicy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PolicyController : ControllerBase
    {


        private readonly ILogger<PolicyController> _logger;
        private readonly IPolicyHandler<Policy> _policyHandler;


        public PolicyController(ILogger<PolicyController> logger,
            IPolicyHandler<Policy> policyHandler)
        {
            _logger = logger;
            _policyHandler = policyHandler;
        }

        [HttpPost(Name = "Insert")]
        public async Task<ActionResult> Insert([FromBody] Policy policy)
        {
            try
            {
                if (ModelState.IsValid)
                    return Ok(await _policyHandler.Insert(policy));
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet(Name = "Get")]
        public async Task<ActionResult> Get([FromQuery] PolicyFilter policyFilter)
        {
            try
            {
                if (!string.IsNullOrEmpty(policyFilter.Plate) || !string.IsNullOrEmpty(policyFilter.PolicyNumber))
                    return Ok(await _policyHandler.Get(
                        new Policy 
                        { 
                            PolicyNumber = policyFilter.PolicyNumber,
                            Car = new Car { Plate = policyFilter.Plate }
                        }));

                return BadRequest(ModelState.ErrorCount);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}