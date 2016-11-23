using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog.Models
{
    public class AllIssues
    {
        public  ListOfIssues TodayIssues { get; set; }
        public ListOfIssues TomorrowIssues { get; set; }
        public string FileContents { get; set; }
    }
}
