using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;
namespace DAL
{
    interface IDAL
    {
        Task<List<Currency>> loadCurrencies();//return real time value of coins.
        Task<List<Currency>> loadCurrenciesHistory(string initial);//return real time history value of coins.
    }
}
