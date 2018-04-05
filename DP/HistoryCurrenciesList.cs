using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace DP
{
    class HistoryCurrenciesList
    {
        public virtual List<History> HistCurrList { set; get; }

        public DateTime date { get; set; }

        [Key]
        public int HC_id { set; get; }
    }
}
