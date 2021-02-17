using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;

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


        [HttpGet("currencies")]
        public async Task<ActionResult<string>> GetCurrencies()
        {
            var currencies = await _service.GetCurrencies();

            return Json(currencies);
        }

        [HttpGet("exchanges")]
        public async Task<ActionResult<string>> GetExchanges()
        {
            var exchanges = await _service.GetExchanges();

            return Json(exchanges);
        }

        [HttpGet("exchange/{id}")]
        public async Task<ActionResult<string>> GetPairsByExchangeId(int id)
        {
            var pairs = await _service.GetPairsByExchangeId(id);


            return Json(pairs);
        }

        [HttpGet("exchanges-with-pairs")]
        public async Task<ActionResult<string>> GetExchangesWithPairs()
        {
            var pairs = await _service.GetExchangeWithPairs();


            return Json(pairs);
        }

        [HttpPost("exchanges")]
        public async Task<ActionResult<string>> AddExchanges()
        {
            await _service.AddExchanges();

            return Json("{}");
        }
        [HttpGet("exchanges/trade/{currencySymbol1}/to/{currencySymbol2}")]
        public ActionResult<string> GetExchangesByTwoCurrencies(string currencySymbol1, string currencySymbol2)
        {
            var exchanges = _service.GetExchangesByTwoCurrencies(currencySymbol1, currencySymbol2);
            return Json(exchanges);
        }
    }
}
