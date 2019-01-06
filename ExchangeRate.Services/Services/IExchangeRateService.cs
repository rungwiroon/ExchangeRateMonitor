using ExchangeRate.Services.Models;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Services.Services
{
    public interface IExchangeRateService
    {
        Task<Either<string, IEnumerable<ExchangeRateModel>>> Get();
        Task<Either<string, IEnumerable<ExchangeRateModel>>> Get(DateTime dateTime);
        Task<Either<string, IEnumerable<ExchangeRateModel>>> Get(Currency currency, DateTime start, DateTime end);
    }
}
