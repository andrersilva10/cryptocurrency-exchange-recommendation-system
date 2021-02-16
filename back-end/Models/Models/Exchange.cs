using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Domain.Models
{
    public class Exchange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("name_id")]
        public string NameId { get; set; }
        public string Url { get; set; }
        [JsonProperty("volume_usd")]
        public decimal VolumeUsd { get; set; }
        [JsonProperty("pairs")]
        public int ActivePairs { get; set; }
        public string Country { get; set; }
        public List<ExchangePair> Pairs { get; set; }

    }
}
