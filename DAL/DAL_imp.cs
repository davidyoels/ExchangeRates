using DP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DAL
{
    public class DAL_imp : IDAL
    {
        #region get list of updated currencies
        public async Task<List<DBCurrency>> loadCurrenciesAsync()
        {
            try
            {
                DB_Context context = new DB_Context();
                List<DBCurrency> Currencies = new List<DBCurrency>();
                //ToListAsync-convert from the DbSet<Currency> to List<Currency>
                Currencies = await context.currencies.ToListAsync();

                //check if it's not empty otherwise we need to charge the list from the webSite using the Api.
                if (Currencies.Any() && CheckIfUpdate(Currencies))
                    return Currencies;
                else
                {
                    //using the instance we get the access to the webSite by the api.
                    var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                    //await - says that i will we give the cpu to anyone who wants and i will wait.
                    //here we invoke the function to get the list of initilais with their full name and resotore them using the Models.
                    //Models.CurrencyListModel - have "terms" "privacy" "currencies".
                    var CurrenciesList = await instance.Invoke<CurrencyLayerDotNet.Models.CurrencyListModel>("list").ConfigureAwait(false);
                    var CurrenciesValues = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("live").ConfigureAwait(false);
                    if (CurrenciesList.Success == true)
                    {
                        Currencies = CurrenciesList.quotes.Select(t => new DBCurrency() { Initials = t.Key, FullName = t.Value }).ToList();// "BND": "Brunei Dollar",
                        Currencies = UpdateValueToCurrenciesByNames(Currencies, CurrenciesValues);

                        //this used to remove all the currencies form the data base and to insert the new currencies.
                        foreach (var t in context.currencies)
                        {
                            context.currencies.Remove(t);
                        }
                        context.currencies.AddRange(Currencies);
                        await context.SaveChangesAsync();
                        return Currencies;
                    }
                    else
                        return null;
                }
            }

            catch (Exception e)
            {

                throw;
            }

        }

        private List<DBCurrency> UpdateValueToCurrenciesByNames(List<DBCurrency> Currencies, CurrencyLayerDotNet.Models.LiveModel RTRates)
        {
            List<DBCurrency> CurrenciesList = new List<DBCurrency>();
            foreach (var qoute in RTRates.quotes)
            {
                //here we get the full name of the country currency."ILS"="UDS_ILS_" and return the full name of the country.
                string issuesCountryName = Currencies.Find(t => t.Initials == qoute.Key.Substring(3)).FullName;
                DBCurrency currency = new DBCurrency()
                {
                    Value = qoute.Value,
                    Initials = qoute.Key.Substring(3),
                    FullName = issuesCountryName,
                    Date = DateTime.Now,
                    Flag = ("UI/Images/" + qoute.Key.Substring(3) + ".png")
                };
                CurrenciesList.Add(currency);
            }
            return CurrenciesList;
        }


        #region check if update
        private bool CheckIfUpdate(List<DBCurrency> currencies)
        {
            DB_Context context = new DB_Context();
            DBCurrency tmp = currencies.First();
            DateTime time = DateTime.Now.ToLocalTime();
            //NEED TO CHANGE
            if (tmp.Date.AddHours(3) > DateTime.Now && (IsItTheSameDay(tmp.Date)))
                return true;

            context.Dispose();
            return false;
        }

        private bool IsItTheSameDay(DateTime date)
        {
            return (date.DayOfYear == DateTime.Now.DayOfYear && date.Year == DateTime.Now.Year);
        }

        private bool IsItYesterday(DateTime date)
        {
            return (date.DayOfYear == (DateTime.Now.DayOfYear - 1) && date.Year == DateTime.Now.Year);
        }
        #endregion
        #endregion

  

        
        public async Task<Dictionary<DateTime, double>> loadCurrenciesHistory(string initial)
        {
            try
            {
                pushCurrenciesHistory();
                DB_Context context = new DB_Context();
                List<History> Currencies = new List<History>();
                Currencies = await context.historicalCurrencies.ToListAsync();
                Dictionary<DateTime, double> d = new Dictionary<DateTime, double>();
                foreach (History element in Currencies)
                {
                    if (element.Initials.Equals(initial))
                        d.Add(new DateTime(long.Parse(element.Date)), element.Value);
                }
                return d;
            }
             catch(Exception ex)
            {
                throw;
            }
        }

        //history information save for last X weeks , for all kinds of coins.
        public async void pushCurrenciesHistory()
        {
            DB_Context context = new DB_Context();
            List<History> Currencies = new List<History>();
            //if (context.historicalCurrencies == null)
            //    return;
            Currencies = await context.historicalCurrencies.ToListAsync();

            Dictionary<string, string> _date = new Dictionary<string, string>();
            DateTime dt = DateTime.Now;//DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (Currencies.Any())  //The table isn't empty.
                return;            //Don't need to download again the hostori...

            var instance = new CurrencyLayerDotNet.CurrencyLayerApi();  //The url request for the history.
            var init_fullName = await instance.Invoke<CurrencyLayerDotNet.Models.CurrencyListModel>("list").ConfigureAwait(false);
            Dictionary<string, string> converter = init_fullName.quotes;
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                _date.Add("date", dt.ToString("yyyy-MM-dd"));// "YYYY-MM-DD"));               
                var CurrenciesList = await instance.Invoke<CurrencyLayerDotNet.Models.HistoryModel>("historical", _date).ConfigureAwait(false);
                _date.Clear();
                DateTime shareDate = dt;
                dt.AddDays(-7); //check the values for all the weeks.
                Dictionary<string, string> items = CurrenciesList.quotes;
               
                foreach (KeyValuePair<string, string> entry in items)
                {
                    // do something with entry.Value or entry.Key
                    double value = Double.Parse(entry.Value);
                    string inital = entry.Key.Substring(3);
                    string fullName = converter[inital];
                    string flag = "UI/Images/" + entry.Key.Substring(3) + ".png";
                    string date = dt.Ticks.ToString(); 
                    Currencies.Add(new History() {Id=count++, Initials = inital, Date = date, Flag = flag, Value = value, FullName = fullName });
                }
                context.historicalCurrencies.AddRange(Currencies);
                await context.SaveChangesAsync();
            }
        }


    }
}