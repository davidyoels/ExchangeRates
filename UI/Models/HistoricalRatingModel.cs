using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
namespace UI.Models
{
    public class HistoricalRatingModel
    {
        public async Task<Dictionary<DateTime, double>> GetHistorialCurrencies(string countryInitial)
        {
            return await new Bl_imp().getHistorialCurrencies(countryInitial);
        }
    }
}
