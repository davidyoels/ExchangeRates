using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DP;

namespace BL
{    
    public class Bl_imp : IBL
    {
        public async Task<List<DBCurrency>> getCurrencies()
        {
            var lst = await new DAL_imp().loadCurrenciesAsync();
            //------NEED--TO--CHANGE--THIS-------
            //foreach (DBCurrency c in lst)
            //{
            //    c.Difference = "8";
            //}
            // return Rounding(lst);//used to round the number until the two numbers after the dot.
            return lst;
        }

        //Here we use to calculate the moeny convert from X to Y with the amount by the formula.
        public double ConvertCalculation(DBCurrency From, DBCurrency To, string amount)
        {
            return Convert.ToDouble(amount) * Convert.ToDouble(To.Value) / Convert.ToDouble(From.Value);
        }
        //
        public async Task<Dictionary<DateTime, double>> getHistorialCurrencies(string countryInitial)
        {
            return await new DAL_imp().loadCurrenciesHistory(countryInitial);
        }

        public void pushHistorialCurrencies()
        {
             new DAL_imp().pushCurrenciesHistory();
        }
    }
}

