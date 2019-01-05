using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Services.Models
{
    public class SuperRichResponse
    {
        public int code { get; set; }
        public string descriptionEn { get; set; }
        public string description { get; set; }
        public Data data { get; set; }
    }
}
