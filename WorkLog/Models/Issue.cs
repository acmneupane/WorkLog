﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog.Models
{
    public class Issue
    {
        public int id { get; set; }

        public string key { get; set; }

        public Fields fields { get; set; }
    }
}
