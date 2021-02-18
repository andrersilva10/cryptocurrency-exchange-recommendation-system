using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Models.Interfaces;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleController : Controller
    {
        private ISimpleService _service;
        private IExternalApiService externalApiService;

        public SimpleController(ISimpleService service, IExternalApiService externalApiService)
        {
            _service = service;
            this.externalApiService = externalApiService;
        }

        [HttpPost("exchanges")]
        public async Task<ActionResult<string>> Exchanges()
        {
            await externalApiService.AddExchanges();

            return Json(new { });
        }

        [HttpGet("currencies")]
        public ActionResult<string> GetCurrencies()
        {
            var currencies =  _service.GetCurrencies();

            return Json(currencies);
        }


        [HttpGet("exchanges/trade/{currencySymbol1}/to/{currencySymbol2}")]
        public ActionResult<string> GetExchangesByTwoCurrencies(string currencySymbol1, string currencySymbol2)
        {
            var exchanges = _service.GetExchangesByTwoCurrencies(currencySymbol1, currencySymbol2);
            return Json(exchanges);
        }
    }
}
