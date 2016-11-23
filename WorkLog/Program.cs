using System;
using WorkLog.Components;
using WorkLog.Services;

namespace WorkLog
{
    class Program
    {
        public static void Main(string[] args)
        {
            var allIssues = new JiraIssues().ListIssues();

            var readWriteFileService = new ReadWriteFileService();
            readWriteFileService.WriteFileToHtml(allIssues.FileContents); //Write contents to File

            var send = new WorklogEmailService();
            var isSuccess = send.SendWorklog(); //Send Worklog
            if (isSuccess)
            {
                Console.WriteLine("SendWorklog Successfully sent.");
                Console.WriteLine("Press Enter to Exit.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Worklog not sent");
            }
            
        }
    }
}
