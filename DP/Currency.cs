using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    public class Currency
    {
        public double Value { get; set; } // 1.0456
        public string Name { get; set; } // USILS
        public string Initials { get; set; }//ראשי תיבות
        public string Flag { get; set; } // בשביל תמונת דגל
        public DateTime TimeUpdate { get; set; }// זמן עדכון

        /*public Currency(double new_value, string new_name, string new_initials, string new_flag, string date)
        {
            Value = new_value;
            Name = new_name;
            Initials = new_initials;
            Flag = new_flag;
            date.Replace('-', '/');
            TimeUpdate = Convert.ToDateTime(date);
        }*/
    }
}
