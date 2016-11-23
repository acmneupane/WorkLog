using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog.Models
{
    public class Fields
    {
        public string summary { get; set; }
        public TimeTracking timetracking { get; set; }
        public Status status { get; set; }
    }
}
