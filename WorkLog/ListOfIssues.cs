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
    }

}
