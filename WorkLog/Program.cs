using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Atlassian;
using Atlassian.Jira;
using RestSharp;

namespace WorkLog
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("List your issues?? (Type 'Y' or 'y' if YES or Any other Key if NO)");
            string execute = Console.ReadLine();
            if (execute != null && (execute[0] == 'Y' || execute[0] == 'y'))
            {
                Today todayIssues = new Today();
                var fileContents = todayIssues.IssuesForToday();
                Tomorrow tomorrow = new Tomorrow();
                tomorrow.IssuesForTomorrow(ref fileContents);
                fileContents = fileContents.Replace("\r\n\r\n", "");
                if (File.Exists(@"D:\New.txt"))
                {
                    File.Delete(@"D:\New.txt");
                }
                if (File.Exists(@"D:\New.html"))
                {
                    File.Delete(@"D:\New.html");
                }
                File.WriteAllText(@"D:\New.txt", fileContents);
                File.WriteAllText(@"D:\New.html", fileContents);
                Console.WriteLine("Verify the file in D:/New.txt");
            }
            SendEmail send = new SendEmail();
            send.SendMail();
        }
    }
}
