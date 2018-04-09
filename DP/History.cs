using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    public class History
    {
        private double _value;
        private string _fullName;
        private string _initials;
        private string _flag;

        public string Initials { get => _initials; set => _initials = value; }
        public double Value { get => _value; set => _value = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string Flag { get => _flag; set => _flag = value; }
        public int ID { get; set; }

    }
}
