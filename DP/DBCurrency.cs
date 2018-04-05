using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    //meant to be the coins on the Data Base
    public class DBCurrency
    {
        public double Value { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public string Flag { get; set; }
        public DateTime Date { get; set; }
        public int ID { get; set; }
        public string Difference { get; set; }

        public DBCurrency(Currency c)
        {
            this.Date = c.Date;
            this.Flag = c.Flag;
            this.FullName = c.FullName;
            this.ID = c.ID;
            this.Initials = c.Initials;
            //לעגל בשביל התצוגה לכמה ספרות אחרי הנקודה
           /* if (c.Value > 1000 && c.Value < 10000)
                this.Value = string.Format("{0:F3}", c.Value);
            else if (c.Value > 100 && c.Value < 1000)
                this.Value = string.Format("{0:F4}", c.Value);
            else if (c.Value > 10 && c.Value < 100)
                this.Value = string.Format("{0:F5}", c.Value);
            else
                this.Value = string.Format("{0:F6}", c.Value);*/
        }
    }
}
