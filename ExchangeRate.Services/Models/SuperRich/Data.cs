using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Services.Models
{
    public class Data
    {
        public List<ExchangeRate> exchangeRate { get; set; }
        public DateTime dateTime { get; set; }
    }
}
