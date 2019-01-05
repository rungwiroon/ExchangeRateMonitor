using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Services.Models
{
    public class ExchangeRateModel
    {
        public string CurrencyName { get; set; }

        public string Country { get; set; }

        public string Denomination { get; set; }

        public decimal BuyingRate { get; set; }

        public decimal SellingRate { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
