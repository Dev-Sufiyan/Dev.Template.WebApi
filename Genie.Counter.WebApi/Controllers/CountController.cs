using Genie.Counter.Model.Entity;
using Genie.Counter.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genie.Counter.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountController : BaseController<Count>
    {
        ICountHistoryRepository CountHistoryRepository;
        public CountController(IRepositoryBase<Count> repository, ICountHistoryRepository CountHistoryRepository) : base(repository)
        {
            this.CountHistoryRepository = CountHistoryRepository;
        }

        // GET api/count
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Example filter: Get Count entity with CounterId = 1
            var filters = new Dictionary<string, string> { { nameof(Count.CounterId), "1" } };
            var entities = await _repository.GetFilteredAsync(filters);

            if (entities == null || !entities.Any())
            {
                return NotFound(new { message = "Count entity not found." });
            }

            // Return the TotalCount of the first matching entity
            return Ok(new { totalCount = entities.First().TotalCount });
        }

        // POST api/count/add
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromQuery] int num)
        {
            // Example filter: Get Count entity with CounterId = 1
            var filters = new Dictionary<string, string> { { nameof(Count.CounterId), "1" } };
            var entities = await _repository.GetFilteredAsync(filters);

            if (entities == null || !entities.Any())
            {
                return NotFound(new { message = "Count entity not found." });
            }

            var entity = entities.First();
            entity.TotalCount += num;

            // Update the entity in the repository
            await _repository.UpdateAsync(entity);
            await CountHistoryRepository.AddAsync(new() { Count = num });
            // Return the updated entity
            return Ok(new { totalCount = entity.TotalCount });
        }
    }
}
