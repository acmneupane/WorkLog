using System.Collections.Generic;


namespace WorkLog.Models
{
    public class ListOfIssues
    {
        public List<Issue> issues { get; set; }

        public int total { get; set; }
    }
}
