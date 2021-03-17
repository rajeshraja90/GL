using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;

namespace ProjectManagement.Api.Controllers
{
    public class BaseController<T> : ControllerBase where T : BaseEntity
    {
        private readonly IBaseRepository<T> _baseRepository;
        public BaseController(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _baseRepository.Get();
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var result = _baseRepository.Get(id);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpPost]
        public IActionResult Add(T value)
        {
            var result = _baseRepository.Add(value);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(T value)
        {
            var result = _baseRepository.Update(value);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            _baseRepository.Delete(id);
            return Ok();
        }

    }
}
