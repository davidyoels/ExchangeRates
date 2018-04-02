using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;
using System.Data.Entity;


namespace DAL
{
    public class DAL_imp : IDAL
    {
        public async Task<List<Currency>> loadCurrencies()
        {
            List<Currency> currencies;
            using (DB_Context context = new DB_Context())
            {

                currencies = await context.currencies.ToListAsync();

            }
            if (currencies.Any() && IsUpdate(currencies))
            {
                return currencies;
            }
            else
            {
                var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                //CurrencyLayerDotNet refer to Model
                var CurrenciesList = await instance.Invoke<CurrencyLayerDotNet.Models.CurrencyListModel>("list");
                var CurrenciesValues = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("list");
                if (CurrenciesList.Success == true)
                {
                    currencies = CurrenciesList.quotes.Select(t => new Currency() { Initials = t.Key, Name = t.Value }).ToList();
                    currencies = UpdateValueToCurrenciesByNames(currencies, CurrenciesValues);

                    using (DB_Context context = new DB_Context())
                    {

                        context.currencies.AddRange(currencies);
                        await context.SaveChangesAsync();

                    }
                    return currencies;

                }
                else
                    return null;
            }
        }

        private List<Currency> UpdateValueToCurrenciesByNames(List<Currency> Currencies, CurrencyLayerDotNet.Models.LiveModel RTRates)
        {
            List<Currency> CurrenciesList = new List<Currency>();
            foreach (var qoute in RTRates.quotes)
            {
                string issuesCountryName = Currencies.Find(t => t.Initials == qoute.Key.Substring(3)).FullName;
                Currency newCurrency = new Currency();
                newCurrency.Value = double.Parse(qoute.Value);
                newCurrency.Initials = qoute.Key.Substring(3);
                newCurrency.Name = issuesCountryName;
                newCurrency.TimeUpdate = DateTime.Now;
                newCurrency.Flag = ("UserInterface/Flags/" + qoute.Key.Substring(3) + ".png");
                //asdasdasddasddasd
                CurrenciesList.Add(newCurrency);
            }
            return CurrenciesList;
        }

        #region Update Checking 
        private bool IsUpdate(List<Currency> currencies)
        {
            DB_Context context = new DB_Context();
            Currency tmp = currencies.First();
            DateTime time = DateTime.Now.ToLocalTime();
            if (tmp.TimeUpdate.AddHours(3) > DateTime.Now && IsItTheSameDay(tmp.Date))
                return true;

            context.Dispose();
            return false;
        }

        private bool IsItTheSameDay(DateTime date)
        {
            return (date.DayOfYear == DateTime.Now.DayOfYear && date.Year == DateTime.Now.Year);
        }
        #endregion

        public Task<List<Currency>> loadCurrenciesHistory(string initial)
        {
            return null;
        }
    }
}