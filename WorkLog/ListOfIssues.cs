using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog
{
    public class ListOfIssues
    {
        public List<Issue> issues { get; set; }

        public int total { get; set; }
    }

    public class Issue
    {

        public int id { get; set; }

        public string key { get; set; }

        public Fields fields { get; set; }
    }

    public class Fields
    {
        public string summary { get; set; }
        public Timetracking timetracking { get; set; }
        public Status status { get; set; }
    }
    public class Timetracking
    {
        public string originalEstimate { get; set; }
        public string remainingEstimate { get; set; }
        public string timeSpent { get; set; }
        public long originalEstimateSeconds { get; set; }
        public long remainingEstimateSeconds { get; set; }
        public long timeSpentSeconds { get; set; }
    }
    public class Status
    {
        public string name { get; set; }
    }
}
