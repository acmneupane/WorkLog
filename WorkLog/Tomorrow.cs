using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog
{
    class Tomorrow
    {
        public void IssuesForTomorrow(ref string fileContents)
        {
            string tomorrowquery = "http://www.jira.codebee.dk:8080/rest/api/latest/search?jql=status%20in%20%28%22To%20Do%22%2C%22In%20Progress%22%2C%22Backlog%22%29%26assignee%3Daseem";
            var issues = new Jira().JiraAccess(tomorrowquery);
            if(issues != null)
            {
                fileContents = AddTaskForTomorrow(issues, fileContents);
            }
        }

        public string AddTaskForTomorrow(ListOfIssues issues, string fileContents)
        {
            string tomorrow = "Tasks For Tomorrow - <br /> <ul>";
            foreach(var issue in issues.issues.ToList())
            {
                tomorrow += "<li> " + issue.key + "</li>";
            }
            tomorrow += "</ul>";
            return fileContents.Replace("Tasks For Tomorrow - ", tomorrow);
        }
    }
}
