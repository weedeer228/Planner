using DbService.Db;
using DbService.Filler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Interfaces;

namespace Planner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DbController : ControllerBase
    {
        private PlannerContext _context;
        public DbController( PlannerContext context)
        {
            _context = context;
        }
        [HttpPost(Name = "FillDb")]
        public async Task<IActionResult> Get()
        {
            List<object> res = new List<object>();


            var filler = new DbFiller(_context);
            await filler.FillDb(5, true);
            res.AddRange(await _context.Sku.Include(x => x.SKUSubs).ThenInclude(x => x.PeriodData).ToListAsync());
            return Ok(res);
        }
    }
}
