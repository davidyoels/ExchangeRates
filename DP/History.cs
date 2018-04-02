using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    public class History
    {
        private double value;
        private string fullName;
        private string initials;
        private string state_flag;
        private DateTime date;
        private object _value;

        public double Value;

        public string Initials { get; }
        public DateTime TimeUpdate { get; }

        public History(string new_initials, double new_value, string new_TimeUpdate)
        {
            Value = new_value;
            Initials = new_initials;
            new_TimeUpdate.Replace('-', '/');
            TimeUpdate = Convert.ToDateTime(new_TimeUpdate);
        }
    }
}
