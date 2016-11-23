using System.Linq;
using System.Configuration;
using WorkLog.Models;
using WorkLog.Services;

namespace WorkLog.Components
{
    class Tomorrow
    {
        public ListOfIssues IssuesForTomorrow(ListOfIssues issues)
        {
            string tomorrowquery = ConfigurationManager.AppSettings["TomorrowIssueQuery"];
            var tomorrowIssues = new JiraAccessService().JiraAccess(tomorrowquery);
            if(tomorrowIssues != null && tomorrowIssues.issues.Count > 0)
            {
                issues = tomorrowIssues;
                issues.total = tomorrowIssues.issues.Count;
            }
            return issues;
        }
    }
}
