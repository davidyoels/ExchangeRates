using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DP;

namespace ExchangeRates
{
    class Bl_imp : IBL
    {
        public Task<List<Currency>> getCurrencies()
        {
            return new DAL_imp().loadCurrencies();
        }
    }
}
