using Genie.Counter.Model.Entity;
using Genie.Counter.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genie.Counter.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountController : BaseController<Count>
    {
        public CountController(IRepositoryBase<Count> repository) : base(repository)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var filters = new Dictionary<string, string> { { nameof(Count.CounterId), "1" } };
            var entities = await _repository.GetFilteredAsync(filters);
            return Ok(entities.First().TotalCount);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(int num)
        {
            var filters = new Dictionary<string, string> { { nameof(Count.CounterId), "1" } };
            var entities = await _repository.GetFilteredAsync(filters);

            var entity = entities.First();
            entity.TotalCount += num;

            await _repository.UpdateAsync(entity);
            return Ok(entity);
        }
    }
}
