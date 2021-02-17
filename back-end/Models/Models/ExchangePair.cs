using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class ExchangePair
    {
        public int Id { get; set; }
        public int IdExchange { get; set; }
        public Exchange Exchange { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        [JsonProperty("price_usd")]
        public decimal PriceUsd { get; set; }
        public Int64 Time { get; set; }
    }
}
