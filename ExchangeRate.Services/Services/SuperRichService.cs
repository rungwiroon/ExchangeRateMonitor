using ExchangeRate.Services.Models;
using ExchangeRate.Services.Models.SuperRich;
using LanguageExt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace ExchangeRate.Services.Services
{
    public class SuperRichService : IExchangeRateService
    {
        private HttpClient _httpClient;
        private string _getLatestUrl = "https://www.superrichthailand.com/api/v1/rates";
        private string _getAllCurrenciesUrl = "https://www.superrichthailand.com/api/v1/rates/history";
        private string _getSingleCurrencyHistoryUrl = "https://www.superrichthailand.com/api/v1/rates/chart";
        private string auturizationParamater = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "superrichTh", "hThcirrepus")));
        private string _successDescription = "Success";

        public SuperRichService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                auturizationParamater);
        }

        private static IEnumerable<ExchangeRateModel> MapModel(SuperRichResponse response)
        {
            return response.data.exchangeRate.SelectMany(r => r.rate.Select(r1 => new ExchangeRateModel()
            {
                CurrencyName = r.cUnit,
                Country = r.countryName,
                Denomination = r1.denom,
                BuyingRate = r1.cBuying,
                SellingRate = r1.cSelling,
                UpdateAt = r1.dateTime
            }));
        }

        //private static Either<string, IEnumerable<ExchangeRateModel>> MapToEither(SuperRichResponse response)
        //{
        //    if (response.descriptionEn != "Success")
        //    {
        //        return Left(response.descriptionEn);
        //    }

        //    return Right(MapModel(response));
        //}

        public async Task<Either<string, IEnumerable<ExchangeRateModel>>> Get()
        {
            var response = await _httpClient.GetAsync(_getLatestUrl);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var superRichResponseHeader = JsonConvert.DeserializeObject<SuperRichResponseHeader>(jsonResponse);

            if(superRichResponseHeader.descriptionEn != _successDescription)
            {
                return Left(superRichResponseHeader.descriptionEn);
            }

            var superRichResponse = JsonConvert.DeserializeObject<SuperRichResponse>(jsonResponse);

            return Right(MapModel(superRichResponse));
        }

        public async Task<Either<string, IEnumerable<ExchangeRateModel>>> Get(DateTime dateTime)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("date", dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
            });

            var response = await _httpClient.PostAsync(_getAllCurrenciesUrl, content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var superRichResponseHeader = JsonConvert.DeserializeObject<SuperRichResponseHeader>(jsonResponse);

            if (superRichResponseHeader.descriptionEn != _successDescription)
            {
                return Left(superRichResponseHeader.descriptionEn);
            }

            var superRichResponse = JsonConvert.DeserializeObject<SuperRichResponse>(jsonResponse);

            return Right(MapModel(superRichResponse));
        }

        public async Task<Either<string, IEnumerable<ExchangeRateModel>>> Get(Currency currency, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
