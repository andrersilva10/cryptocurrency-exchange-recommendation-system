using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ExchangePair
    {
        public int Id { get; set; }
        public int IdExchange { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public decimal PriceUsd { get; set; }
        public int Time { get; set; }
    }
}
