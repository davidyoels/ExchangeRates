﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    public class History
    {
        private double _value;
        private string _fullName;
        [Key]
        private string _initials;
        private string _flag;

        public string Initials {
            get { return _initials; }
            set { _initials = value; }
        }
        public double Value {
            get { return _value; }
            set { _value = value; }
        }
        public string FullName {
            get { return _fullName; }
            set { _fullName = value; }
            }
        public string Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        public int ID { get; set; }

    }
}
