using Domain.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Infrastructure
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly ExchangeContext _context;

        public ExchangeRepository(ExchangeContext context)
        {
            _context = context;
        }
        public void AddExchanges(List<Exchange> exchanges)
        {
            clearDatabase();
            _context.AddRange(exchanges);
            _context.SaveChanges();
        }

        public List<Exchange> GetExchangesByTwoCurrencies(string currencySymbol1, string currencySymbol2)
        {
            var exchanges = _context.ExchangePairs.Where(x => x.Base == currencySymbol1 && x.Quote == currencySymbol2).Select(x => x.Exchange).Distinct().ToList();
            exchanges.ForEach(x => {
                x.ExchangePairs = _context.ExchangePairs.Where(y => y.IdExchange == x.Id && y.Base == currencySymbol1 && y.Quote == currencySymbol2).ToList();
                x.ExchangePairs = x.ExchangePairs.Where(y => y.Time == x.ExchangePairs.Max(z => z.Time)).ToList();
            });
            return exchanges;
        }

        private void clearDatabase()
        {
            var allRows = _context.Exchanges.ToList();
            _context.Exchanges.RemoveRange(allRows);
        }
    }
}
