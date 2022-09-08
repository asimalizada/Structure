using Core.Business.Abstract;
using Core.Entities.Abstract;
using Core.Features.Results.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TService, TEntity, TModelAdd, TModelUpdate, TModelDelete> : ControllerBase
        where TEntity : class, IEntity, new()
        where TModelAdd : class, IAddModel, new()
        where TModelUpdate : class, IUpdateModel, new()
        where TModelDelete : class, IDeleteModel, new()
        where TService : class, IServiceRepository<TEntity>
    {
        private readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        public virtual IActionResult GetAll()
        {
            return CreateResponse(_service.GetAll());
        }

        [HttpGet("get")]
        public virtual IActionResult Get(int id)
        {
            return CreateResponse(_service.Get(id));
        }

        [HttpPost("add")]
        public virtual IActionResult Add(TEntity entity)
        {
            return CreateResponse(_service.Add(entity));
        }

        [HttpPost("update")]
        public virtual IActionResult Update(TEntity entity)
        {
            return CreateResponse(_service.Update(entity));
        }

        [HttpPost("delete")]
        public virtual IActionResult Delete(TEntity entity)
        {
            return CreateResponse(_service.Delete(entity));
        }

        [HttpPost("deleteall")]
        public virtual IActionResult DeleteAll()
        {
            return CreateResponse(_service.DeleteAll());
        }

        protected IActionResult CreateResponse(IResult result)
        {
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
