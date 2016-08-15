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
            Jira n = new Jira();
            n.JiraAccess().Wait();
            SendEmail send = new SendEmail();
            send.SendMail();
        }
    }
}
