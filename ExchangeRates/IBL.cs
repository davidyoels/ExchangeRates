using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;
namespace ExchangeRates
{
    interface IBL
    {
        /*
       List<Currency> FindCurrency(string lastName);
      List<Currency> GetCurrency();
      bool setHistoricalData(CurrencyHistory Hcurrency);
      */
        Task<List<Currency>> getCurrencies();
    }
}
