using DbService.Db;
using Microsoft.AspNetCore.Mvc;
using Model.Interfaces;
using Model.Models;

namespace Planner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlannerController : ControllerBase
    {
        private IPlannerService _service;
        private IDataService _dataService;

        public PlannerController(PlannerContext context, IPlannerService service,IDataService dataService)
        {
            _service = service;
            _dataService = dataService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterModel filterModel)
        {
            var data = await _service.GetReport(filterModel);
            return Ok(new { Data = data.Result, data.MetaData });
        }
        [HttpPatch]
        public async Task<IActionResult> Patch([FromQuery]Guid id, BasePeriodInfo value)
        {
            try
            {
                var responce  =await _dataService.UpdateY1ById(id, value);
                return responce ? Ok() : NotFound();

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
