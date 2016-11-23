using System;
using System.Linq;
using WorkLog.Models;

namespace WorkLog.Components
{
    public class AddTaskToFile
    {
        public string AddAllTasks(AllIssues allIssues)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "b.html";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string fileContents = file.ReadToEnd();

            fileContents = AddTodayTask(allIssues.TodayIssues, fileContents);
            fileContents = AddTaskForTomorrow(allIssues.TomorrowIssues, fileContents);
            return fileContents;
        }

        public string AddTodayTask(ListOfIssues listOfIssues, string fileContents)
        {
            if (listOfIssues.total > 0)
            {
                string assigned = "My Assigned Tasks - <ul>";
                string progress = "In Progress Task - ";
                string done = "Done Task - <ul>";

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
                        done += "<li>" + issue.key + "</li>";
                    }
                }
                assigned += "</ul>";
                done += "</ul>";
                fileContents = fileContents.Replace("My Assigned Tasks - ", assigned).Replace("Done Task - ", done).Replace("In Progress Task - ", progress);
            }
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
                var perce = (spentTime * 100) / originalTime;
                completed = perce.ToString();
            }
            if (remainingTime != 0 && remainingTime <= originalTime)
            {
                remaining = issue.fields.timetracking.remainingEstimate;
            }
            fileContents = fileContents.Replace("In Progress Task Complete % - ", "In Progress Task Complete % - " + completed);
            fileContents = fileContents.Replace("In Progress Task Remaining Estimation - ", "In Progress Task Remaining Estimation - " + remaining);
        }

        public string AddTaskForTomorrow(ListOfIssues listOfIssues, string fileContents)
        {
            if (listOfIssues.total > 0)
            {
                string tomorrow = "Tasks For Tomorrow - <br /> <ul>";
                foreach (var issue in listOfIssues.issues.ToList())
                {
                    tomorrow += "<li> " + issue.key + "</li>";
                }
                tomorrow += "</ul>";
                fileContents = fileContents.Replace("Tasks For Tomorrow - ", tomorrow);
            }
            return fileContents;
        }
    }
}
