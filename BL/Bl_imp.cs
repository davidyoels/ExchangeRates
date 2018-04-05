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
            return Rounding(lst);//used to round the number until the two numbers after the dot.
        }

        private List<DBCurrency> Rounding(List<DBCurrency> lst)
        {
            foreach (DBCurrency c in lst)
            {
                bool isParsed = double.TryParse(c.Difference, out double temp);
                if (isParsed)
                {
                    if (temp > 100 && temp < 1000)
                        c.Difference = string.Format("{0:F2}", temp);
                    else if (temp > 10 && temp < 100)
                        c.Difference = string.Format("{0:F3}", temp);
                    else
                        c.Difference = string.Format("{0:F4}", temp);

                    if (temp > 0)
                        c.Difference = " " + c.Difference;
                }
                else
                    throw new Exception("ERROR; can't parse");
            }
            return lst;
        }

        //Here we use to calculate the moeny convert from * to * with the amount by the formula.
        public double ConvertCalculation(DBCurrency From, DBCurrency To, string amount)
        {
            return Convert.ToDouble(amount) * Convert.ToDouble(To.Value) / Convert.ToDouble(From.Value);
        }
        //
        public async Task<Dictionary<DateTime, double>> getHistorialCurrencies(string countryInitial)
        {
            return await new DAL_imp().loadCurrenciesHistory(countryInitial);
        }
    }
}

