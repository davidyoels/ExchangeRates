using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;
namespace BL
{
    interface IBL
    {
        Task<List<DBCurrency>> getCurrencies();
        Task<Dictionary<DateTime, double>> getHistorialCurrencies(string countryInitial);
        void pushHistorialCurrencies();
    }
}
