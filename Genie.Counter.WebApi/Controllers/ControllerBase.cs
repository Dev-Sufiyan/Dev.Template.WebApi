using Genie.Counter.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Genie.Counter.WebApi;
[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<T> : ControllerBase where T : class
{
    protected readonly IRepositoryBase<T> _repository;

    protected BaseController(IRepositoryBase<T> repository)
    {
        _repository = repository;
    }


    [HttpGet(nameof(GetFilteredAsync))]
    public async Task<IActionResult> GetFilteredAsync([FromQuery] Dictionary<string, object> filters)
    {
        var entities = await _repository.GetFilteredAsync(filters);
        return Ok(entities);
    }

    [HttpPost(nameof(AddAsync))]
    public async Task<IActionResult> AddAsync([FromBody] T entity)
    {
        await _repository.AddAsync(entity);
        return NoContent();
    }

    [HttpPut(nameof(UpdateAsync))]
    public async Task<IActionResult> UpdateAsync([FromBody] T entity)
    {
        if (!await EntityExistsAsync(_repository.GetPrimaryKeyValue(entity)))
        {
            return NotFound();
        }

        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete(nameof(DeleteByPKAsync))]
    public async Task<IActionResult> DeleteByPKAsync(object primaryKey)
    {
        if (!await EntityExistsAsync(primaryKey))
        {
            return NotFound();
        }

        await _repository.DeleteAsync(primaryKey);
        return NoContent();
    }

    private async Task<bool> EntityExistsAsync(object pk)
    {
        var entity = await _repository.GetByPrimaryKeyAsync(pk);
        return entity != null;
    }
}

