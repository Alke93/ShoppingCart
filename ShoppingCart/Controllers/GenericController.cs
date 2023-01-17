using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Entities.Generics;
using ShoppingCart.Entities.Requests;
using ShoppingCart.Generics;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [EnableCors(ConfigParams.CORSPOLICY)]
    [GenericControllerName]
    public class GenericController<T> : Controller where T : IRequest
    {
        private readonly IService _service;
        public GenericController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        [ActionName("Read")]
        public async Task<IActionResult> Read(T item)
        {
            var result = await _service.Execute(item, SystemAction.Read);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] T item)
        {
            var result = await _service.Execute(item, SystemAction.Create);
            return Ok(result);
        }

        [HttpPut]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody] T item)
        {
            var result = await _service.Execute(item, SystemAction.Update);
            return Ok(result);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(T item)
        {
            var result = await _service.Execute(item, SystemAction.Delete);
            return Ok(result);
        }
    }
}
