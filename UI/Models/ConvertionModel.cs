using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;
using BL;

namespace UI.Models
{
    public class ConvertionModel
    {
        public double GetConvertCalculation(DBCurrency From, DBCurrency To, string amount)
        {
            return new Bl_imp().ConvertCalculation(From, To, amount);
        }
    }
}

