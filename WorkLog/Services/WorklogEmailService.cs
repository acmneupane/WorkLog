using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog.Services
{
    public class WorklogEmailService
    {
        public bool SendWorklog()
        {
            Console.WriteLine("");
            Console.WriteLine("Send WorkLog?? (Type 'Y' or 'y' if YES or Any other Key if NO)");
            string execute = Console.ReadLine();
            if (execute != null && (execute[0] == 'Y' || execute[0] == 'y'))
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress("developer@codebee.dk"));
                message.CC.Add(new MailAddress("sas@codebee.dk"));
                //message.To.Add(new MailAddress("ane@codebee.dk"));
                message.From = new MailAddress("ane@codebee.dk", "Aseem");
                var date = String.Format("{0:MMM d, yyyy}", DateTime.Now);
                message.Subject = "Work Log for: " + date;

                string messageBody = new ReadWriteFileService().ReadFile();
                if (String.IsNullOrEmpty(messageBody))
                {
                    Console.WriteLine("Issues not listed in D:/New.html.");
                    Console.WriteLine("Please, run the program again after listing the issues.");
                    return false;
                }
                else
                {
                    message.Body = messageBody;
                }

                message.IsBodyHtml = true;
                var sucessfull = SendMail(message);
                return sucessfull;
            }
            return false;
        }

        public bool SendMail(MailMessage message)
        {
            using (var smtp = new SmtpClient())
            {
                var client = new SmtpClient("smtp.codebee.dk", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("ane@codebee.dk", "oopahQu2");
                client.EnableSsl = false;
                try
                {
                    client.Send(message);
                    return true;
                }
                catch (SmtpFailedRecipientException)
                {
                    Console.WriteLine("Failed Receipient");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("again" + ex);
                }
                catch (Exception)
                {
                    Console.WriteLine("Server connection failed");
                }
                return false;
            }
        }
    }
}
