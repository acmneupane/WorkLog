using System.Configuration;
using System.Linq;
using WorkLog.Models;
using WorkLog.Services;

namespace WorkLog.Components
{
    class Today
    {
        public ListOfIssues IssuesForToday(ListOfIssues issues)
        {
            string todayquery = ConfigurationManager.AppSettings["TodayIssueQuery"];
            var todayIssues = new JiraAccessService().JiraAccess(todayquery);
            if (todayIssues != null && todayIssues.issues.Count > 0)
            {
                issues = todayIssues;
                issues.total = todayIssues.issues.Count;
            }
            return issues;
        }

        
    }
}
