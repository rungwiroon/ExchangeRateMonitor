using ExchangeRate.Services.Models;
using ExchangeRate.Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExchangeRate
{
    public partial class Form1 : Form
    {
        private IExchangeRateService _exchangeRateService;
        private List<ExchangeRateModel> _exchangeRates = new List<ExchangeRateModel>(0);

        public Form1()
        {
            InitializeComponent();

            GetRateButton.Click += async (s, e) => await GetRate_Click(s, e);

            _exchangeRateService = new SuperRichService(HttpClientSingleton.Instance);
        }

        private async Task GetRate_Click(object sender, EventArgs e)
        {
            GetRateButton.Text = "Loading";
            GetRateButton.Enabled = false;

            try
            {
                IEnumerable<ExchangeRateModel> result;
                if(dateTimePicker1.Value.Date >= DateTime.Now.Date)
                {
                    result = await _exchangeRateService.Get();
                }
                else
                {
                    result = await _exchangeRateService.Get(dateTimePicker1.Value.Date);
                }

                _exchangeRates = result.ToList();
            }
            finally
            {
                GetRateButton.Enabled = true;
                GetRateButton.Text = "Get rate";
            }

            UpdateList();

            var currencies = _exchangeRates.GroupBy(r => r.CurrencyName)
                    .Select(g => g.Key)
                    .OrderBy(c => c)
                    .ToList();
            currencies.Insert(0, "ALL");

            currencyComboBox.DataSource = currencies;
        }

        private void UpdateList()
        {
            if (currencyComboBox.SelectedIndex <= 0)
            {
                bindingSource1.DataSource = _exchangeRates;
            }
            else
            {
                bindingSource1.DataSource = _exchangeRates
                    .Where(er => er.CurrencyName == currencyComboBox.SelectedValue.ToString());
            }
        }

        private void currencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateList();
        }
    }
}
