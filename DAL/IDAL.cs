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
        Task<List<DBCurrency>> loadCurrenciesAsync();//return real time value of coins.
        Task<Dictionary<DateTime, double>> loadCurrenciesHistory(string initials);//return real time history value of coins.
    }
}
