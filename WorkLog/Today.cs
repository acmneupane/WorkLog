using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog
{
    class Today
    {
        public string IssuesForToday()
        {
            string todayquery = "http://www.jira.codebee.dk:8080/rest/api/latest/search?jql=lastViewed%3EstartOfDay%28%22-0d%22%29%26updated%3EstartOfDay%28%22-0d%22%29%26watcher%3Daseem%26assignee%21%3Dempty&fields=status,timetracking,summary,id,key%20ORDER%20BY%20lastViewed%20DESC";
            var issues = new Jira().JiraAccess(todayquery);
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "b.html";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string fileContents = file.ReadToEnd();
            if (issues != null)
            {
                fileContents = AddTodayTask(issues, fileContents);
            }
            return fileContents;
        }

        public string AddTodayTask(ListOfIssues listOfIssues, string fileContents)
        {
            string assigned = "My Assigned Tasks - <br /> <ul>";
            string progress = "In Progress Task - ";
            string done = "Done Task - ";

            foreach (var issue in listOfIssues.issues.ToList())
            {
                assigned += "<li> " + issue.key + "</li>";
                if (issue.fields.status.name == "In Progress")
                {
                    progress += issue.key;
                    ProgressTaskEstimation(ref fileContents, issue);
                }
                else if (issue.fields.status.name == "In Review" || issue.fields.status.name == "Ready for review" || issue.fields.status.name == "QA Pass" || issue.fields.status.name == "Done")
                {
                    done += issue.key;
                }
            }
            assigned += "</ul>";
            fileContents = fileContents.Replace("My Assigned Tasks - ", assigned).Replace("Done Task - ", done).Replace("In Progress Task - ", progress);
            return fileContents;
        }

        public void ProgressTaskEstimation(ref string fileContents, Issue issue)
        {
            var originalTime = issue.fields.timetracking.originalEstimateSeconds;
            var remainingTime = issue.fields.timetracking.remainingEstimateSeconds;
            var spentTime = issue.fields.timetracking.timeSpentSeconds;
            var completed = "";
            var remaining = "";
            if (spentTime <= originalTime)
            {
                completed = ((int)(remainingTime / originalTime) / 100).ToString();
                remaining = issue.fields.timetracking.remainingEstimate;
            }
            fileContents = fileContents.Replace("In Progress Task Complete % - ", "In Progress Task Complete % - " + completed);
            fileContents = fileContents.Replace("In Progress Task Remaining Estimation - ", "In Progress Task Remaining Estimation - " + remaining);
        }
    }
}
