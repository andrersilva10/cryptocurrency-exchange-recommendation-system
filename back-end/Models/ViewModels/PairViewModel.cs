using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Models.ViewModels
{
    public class PairViewModel
    {
        public int IdExchange { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeNameId { get; set; }
        public string ExchangeUrl { get; set; }
        public decimal ExchangeVolumeUsd { get; set; }
        public int ExchangeActivePairs { get; set; }
        public string ExchangeCountry { get; set; }
        public int IdPair { get; set; }
        public string PairBase { get; set; }
        public string PairQuote { get; set; }
        public decimal PairVolume { get; set; }
        public decimal PairPrice { get; set; }
        public decimal PairPriceUsd { get; set; }
        public Int64 PairTime { get; set; }
        public bool BestRate { get; set; } = false;
        public PairViewModel()
        {

        }

        public PairViewModel(Exchange exchange)
        {
            IdExchange = exchange.Id;
            ExchangeName = exchange.Name;
            ExchangeNameId = exchange.NameId;
            ExchangeUrl = exchange.Url;
            ExchangeVolumeUsd = exchange.VolumeUsd;
            ExchangeActivePairs = exchange.ActivePairs;
            ExchangeCountry = exchange.Country;


            if(exchange.ExchangePairs != null && exchange.ExchangePairs.Count > 0)
            {

                var pair = exchange.ExchangePairs.FirstOrDefault();
                IdPair = pair.Id;
                PairBase = pair.Base;
                PairQuote = pair.Quote;
                PairVolume = pair.Volume;
                PairPrice = pair.Price;
                PairPriceUsd = pair.PriceUsd;
                PairTime = pair.Time;
            }

        }


    }
}
