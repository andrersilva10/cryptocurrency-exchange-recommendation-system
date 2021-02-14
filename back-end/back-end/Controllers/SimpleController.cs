using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleController : Controller
    {
        private ISimpleService _service;

        public SimpleController(ISimpleService service)
        {
            _service = service;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("currencies")]
        public async Task<ActionResult<string>> GetCurrencies()
        {
            var x = await _service.GetCurrencies();

            return Json(x);
        }


    }
}
