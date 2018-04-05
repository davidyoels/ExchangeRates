using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    public class HistoryCurrenciesL
    {
        public virtual List<CurrencyHistory> HistCurrList { set; get; }

        public DateTime date { get; set; }

        [Key]
        public int HC_id { set; get; }

    }
}
