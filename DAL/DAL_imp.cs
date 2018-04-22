using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using DP;
using System.Data.Entity;



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

        /*    #region get dictionary of historical currencies' values by dates

            public async Task<Dictionary<DateTime, double>> LoadCurrenciesHistory(string initials)
            {
                List<double> historyRates;
                Dictionary<DateTime, double> dictionaryHCurrencies = new Dictionary<DateTime, double>();
                HistoryCurrenciesL lastHistory;
                HistoryCurrenciesL firstHistory;

                DateTime lastHistoryDate;
                DateTime firstHistoryDate;


                using (DB_Context context = new DB_Context())
                {
                    //file the gap from now to the first date we have
                    firstHistory = await context.historicalCurrencies.OrderByDescending(t => t.date).FirstOrDefaultAsync().ConfigureAwait(false);
                    //firstHistoryDate = DateTime.Now.AddYears(-1);
                    firstHistoryDate = firstHistory.date;

                    if (firstHistory != null && !IsItYesterday(firstHistory.date))
                    {
                        List<CurrencyHistory> Curr = await LoadCountriesToHistory().ConfigureAwait(false);
                        var instance = new CurrencyLayerDotNet.CurrencyLayerApi();

                        for (DateTime start = firstHistoryDate.AddDays(1); DateTime.Now > start; start = start.AddDays(1))
                        {
                            var HRate = await instance.Invoke<CurrencyLayerDotNet.Models.HistoryModel>("historical", new Dictionary<string, string> { { "date", start.ToString("yyyy-MM-dd") } }).ConfigureAwait(false);
                            HistoryCurrenciesL Hcurrencies = new HistoryCurrenciesL();
                            Hcurrencies.date = start;
                            Hcurrencies.HistCurrList = ConvertCurrencyToHistoricalCurrency(Curr, HRate);
                            firstHistory = Hcurrencies;
                            context.historicalCurrencies.Add(Hcurrencies);
                        }
                        //await context.SaveChangesAsync();
                    }

                    ////gets the last date from DB
                    //lastHistory = await context.historicalCurrencies.OrderBy(t => t.date).FirstOrDefaultAsync().ConfigureAwait(false);

                    ////if no currecies in the DB so make the first on to be the real time one
                    //if (lastHistory == null)
                    //{
                    //    List<CurrencyHistory> Curr = await LoadCountriesToHistory();
                    //    lastHistory.HistCurrList = await GetForNowCurrencyToHistoricalCurrency(Curr);
                    //    lastHistory.date = DateTime.Now;
                    //}


                    //lastHistoryDate = lastHistory.date;

                    ////fil the gap from the last date we have to a year
                    //if (lastHistoryDate.AddYears(1) > DateTime.Now.AddDays(1))
                    //{
                    //    var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                    //    List<CurrencyHistory> Curr = await LoadCountriesToHistory();

                    //    for (DateTime start = lastHistoryDate; start.AddYears(1) > DateTime.Now; start = start.AddDays(-1))
                    //    {
                    //        var HRate = await instance.Invoke<CurrencyLayerDotNet.Models.HistoryModel>("historical", new Dictionary<string, string> { { "date", start.ToString("yyyy-MM-dd") } }).ConfigureAwait(false);
                    //        HistoryCurrenciesL currencies = new HistoryCurrenciesL();
                    //        currencies.date = start;
                    //        currencies.HistCurrList = ConvertCurrencyToHistoricalCurrency(Curr, HRate);
                    //        lastHistory = currencies;
                    //        context.historicalCurrencies.Add(currencies);
                    //    }

                    //}
                    await context.SaveChangesAsync();

                    //get the historical dates
                    historyRates = (from c in context.historicalCurrencies
                                    let selected = (from h in c.HistCurrList
                                                    where h.Initials == initials
                                                    select h).FirstOrDefault()
                                    orderby c.date
                                    select selected.Value).ToList();


                    //historyRates = await context.historicalCurrencies.OrderBy(t => t.date).Select(t => new CurrencyHistory() { Value = t.HistCurrList.FirstOrDefault(x => x.Initials == initials).Value, Initials = initials, FullName = t.HistCurrList.FirstOrDefault(x => x.Initials == initials).FullName }).ToListAsync().ConfigureAwait(false);
                    List<DateTime> dates = await context.historicalCurrencies.OrderBy(t => t.date).Select(t => t.date).ToListAsync().ConfigureAwait(false);
                    //dictionaryHCurrencies = historyRates.ToDictionary(key => DateTime.Now, value => value);
                    try
                    {
                        for (int i = 1; i <= 365 && i <= dates.Count; i++)
                        {
                            dictionaryHCurrencies.Add(dates[dates.Count - i], historyRates[historyRates.Count - i]);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                return dictionaryHCurrencies;
            }

            private async Task<List<CurrencyHistory>> GetForNowCurrencyToHistoricalCurrency(List<CurrencyHistory> curr)
            {
                var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                var CurrenciesValues = await instance.Invoke<CurrencyLayerDotNet.Models.LiveModel>("list").ConfigureAwait(false);
                List<CurrencyHistory> CurrenciesHList = new List<CurrencyHistory>();
                foreach (var qoute in CurrenciesValues.quotes)
                {
                    string issuesCountryName = curr.Find(t => t.Initials == qoute.Key.Substring(3)).FullName;
                    CurrencyHistory newCurrency = new CurrencyHistory()
                    {
                        Value = double.Parse(qoute.Value),
                        Initials = qoute.Key.Substring(3),
                        FullName = issuesCountryName,
                        Flag = ("PL/Flags/" + qoute.Key.Substring(3) + ".png")
                    };
                    CurrenciesHList.Add(newCurrency);
                }
                return CurrenciesHList;
            }

            private async Task<List<CurrencyHistory>> LoadCountriesToHistory()
            {
                List<CurrencyHistory> Currencies;
                var instance = new CurrencyLayerDotNet.CurrencyLayerApi();
                var CurrenciesList = await instance.Invoke<CurrencyLayerDotNet.Models.CurrencyListModel>("list").ConfigureAwait(false);
                if (CurrenciesList.Success == true)
                {
                    Currencies = CurrenciesList.quotes.Select(t => new CurrencyHistory() { Initials = t.Key, FullName = t.Value }).ToList();
                }
                else
                    throw new Exception("Can't load countires");
                return Currencies;
            }

            private List<CurrencyHistory> ConvertCurrencyToHistoricalCurrency(List<CurrencyHistory> curr, CurrencyLayerDotNet.Models.HistoryModel HRates)
            {
                List<CurrencyHistory> CurrenciesHList = new List<CurrencyHistory>();
                foreach (var qoute in HRates.quotes)
                {
                    string issuesCountryName = curr.Find(t => t.Initials == qoute.Key.Substring(3)).FullName;
                    CurrencyHistory newCurrency = new CurrencyHistory()
                    {
                        Value = float.Parse(qoute.Value),
                        Initials = qoute.Key.Substring(3),
                        FullName = issuesCountryName,
                        Flag = ("PL/Flags/" + qoute.Key.Substring(3) + ".png")
                    };
                    CurrenciesHList.Add(newCurrency);
                }
                return CurrenciesHList;
            }
            #endregion



            #endregion
        }
        */

        
        public async Task<Dictionary<DateTime, double>> loadCurrenciesHistory(string initial)
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

        //history information save for last X weeks , for all kinds of coins.
        public async void pushCurrenciesHistory()
        {
            DB_Context context = new DB_Context();
            List<History> Currencies = new List<History>();
            if (context.historicalCurrencies == null)
                return;
            Currencies = await context.historicalCurrencies.ToListAsync();

            Dictionary<string, string> _date = new Dictionary<string, string>();
            DateTime dt = DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (Currencies.Any())  //The table isn't empty.
                return;            //Don't need to download again the hostori...

            var instance = new CurrencyLayerDotNet.CurrencyLayerApi();  //The url request for the history.
            var init_fullName = await instance.Invoke<CurrencyLayerDotNet.Models.CurrencyListModel>("list").ConfigureAwait(false);
            Dictionary<string, string> converter = init_fullName.quotes;
            for (int i = 0; i < 10; i++)
            {
                _date.Add("date", dt.ToString());// "YYYY-MM-DD"));
                var CurrenciesList = await instance.Invoke<CurrencyLayerDotNet.Models.HistoryModel>("historical", _date).ConfigureAwait(false);
                DateTime shareDate = dt;
                dt.AddDays(-7); //check the values for all the weeks.
                Dictionary<string, string> items = CurrenciesList.quotes;
               
                foreach (KeyValuePair<string, string> entry in items)
                {
                    // do something with entry.Value or entry.Key
                    double value = Double.Parse(entry.Value);
                    string inital = entry.Key.Substring(3);
                    string fullName = converter[inital];
                    string flag = "UI/Images/" + inital.Substring(3) + ".png";
                    string date = dt.Ticks.ToString(); 
                    Currencies.Add(new History() { Initials = inital, Date = date, Flag = flag, Value = value, FullName = fullName });
                }
                context.historicalCurrencies.AddRange(Currencies);
                await context.SaveChangesAsync();
            }
        }


    }
}