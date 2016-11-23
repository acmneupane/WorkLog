using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog.Models
{
    public class TimeTracking
    {
        public string originalEstimate { get; set; }
        public string remainingEstimate { get; set; }
        public string timeSpent { get; set; }
        public long originalEstimateSeconds { get; set; }
        public long remainingEstimateSeconds { get; set; }
        public long timeSpentSeconds { get; set; }
    }
}
