using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkLog.Models;

namespace WorkLog.Components
{
    class JiraIssues
    {
        public AllIssues ListIssues()
        {
            Console.WriteLine("");
            Console.WriteLine("List your issues?? (Type 'Y' or 'y' if YES or Any other Key if NO)");
            var allIssues = new AllIssues();
            allIssues.FileContents = String.Empty;
            string execute = Console.ReadLine();
            if (execute != null && (execute[0] == 'Y' || execute[0] == 'y'))
            {
                var todayIssues = new Today();
                allIssues.TodayIssues = todayIssues.IssuesForToday(new ListOfIssues()); //Issues for Today

                var tomorrow = new Tomorrow();
                allIssues.TomorrowIssues = tomorrow.IssuesForTomorrow(new ListOfIssues()); //Issues for Tomorrow

                allIssues.FileContents = new AddTaskToFile().AddAllTasks(allIssues);
            }
            return allIssues;
        }
    }
}
