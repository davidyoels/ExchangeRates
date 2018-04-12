using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    //meant to be the coins on the Data Base
    public class DBCurrency
    {
        private string _value;
        private string _fullName;
        private string _initials;
        private string _flag;
        private DateTime _date;
        private string _difference;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        [Key]
        public string Initials
        {
            get => _initials; 
            set => _initials = value; 
        }
        public string Flag
        {
            get => _flag;
            set => _flag = value;
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        //public string Difference
        //{
        //    get { return _difference; }
        //    set { _difference = value; }
        //}
        //public int ID {get;set;}
    }
}
