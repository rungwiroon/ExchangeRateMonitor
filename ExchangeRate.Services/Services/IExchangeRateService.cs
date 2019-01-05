using ExchangeRate.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Services.Services
{
    public interface IExchangeRateService
    {
        Task<IEnumerable<ExchangeRateModel>> Get();
        Task<IEnumerable<ExchangeRateModel>> Get(DateTime dateTime);
        Task<IEnumerable<ExchangeRateModel>> Get(Currency currency, DateTime start, DateTime end);
    }
}
