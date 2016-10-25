﻿using System;
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
            Today todayIssues = new Today();
            var fileContents = todayIssues.IssuesForToday();
            Tomorrow tomorrow = new Tomorrow();
            tomorrow.IssuesForTomorrow(ref fileContents);
            fileContents = fileContents.Replace("\r\n\r\n", "<br />");
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
            Console.ReadLine();
            SendEmail send = new SendEmail();
            send.SendMail();
        }
    }
}
