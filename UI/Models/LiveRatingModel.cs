using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;
using BL;
namespace UI.Models
{
    public class LiveRatingModel
    {
        public async Task<List<DBCurrency>> GetCurrencies()
        {
            return await new Bl_imp().getCurrencies();
        }
    }
}
