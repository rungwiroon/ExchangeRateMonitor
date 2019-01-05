using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Services.Models
{
    public class ExchangeRate
    {
        public string countryName { get; set; }
        public string cUnit { get; set; }
        public string imgUrl { get; set; }
        public List<Rate> rate { get; set; }
    }
}
