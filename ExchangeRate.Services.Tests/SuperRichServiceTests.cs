using ExchangeRate.Services.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ExchangeRate.Services.Tests
{
    public class SuperRichServiceTests
    {
        private static HttpClient _httpClient;

        static SuperRichServiceTests()
        {
            _httpClient = new HttpClient();
        }

        [Fact]
        public async Task WhenGetLatest_ShouldReturnListOfCurrencyExhangeRates()
        {
            var service = new SuperRichService(_httpClient);
            var result = await service.Get();

            Assert.True(result.Any());
        }

        [Fact]
        public async Task WhenGetByDate_ShouldReturnListOfCurrencyExhangeRates()
        {
            var service = new SuperRichService(_httpClient);
            var result = await service.Get(DateTime.Now.Date.Subtract(TimeSpan.FromDays(1)));

            Assert.True(result.Any());
        }

        [Fact]
        public async Task WhenGetByDateOnNoData_ShouldReturnNoDataFoundText()
        {
            var service = new SuperRichService(_httpClient);
            var result = await service.Get(new DateTime(2018, 12, 31));

            Assert.True(result.IsLeft);
        }
    }
}
