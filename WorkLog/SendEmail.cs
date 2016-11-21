using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog
{

    public class SendEmail
    {

        public int Worklog()
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
                if (!File.Exists(@"D:\New.txt"))
                {
                    return 2;
                }
                else
                {
                    message.Body = File.ReadAllText(@"D:\New.html");
                }

                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    SmtpClient client = new SmtpClient("smtp.codebee.dk", 587);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("ane@codebee.dk", "oopahQu2");
                    client.EnableSsl = false;
                    try
                    {
                        client.Send(message);
                        return 1;
                    }
                    catch (SmtpFailedRecipientException)
                    {
                        Console.WriteLine("Failed Receipient");
                        return 0;
                    }
                    catch (SmtpException ex)
                    {
                        Console.WriteLine("again" + ex);
                        return 0;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Server connection failed");
                        return 0;
                    }

                }
            }
            else
            {
                return 0;
            }
        }
        public void SendMail()
        {
            var returnValue = Worklog();
            switch(returnValue)
            {
                case 0:
                    {
                        Console.WriteLine("Worklog not sent.");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("Worklog Successfully sent.");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Issues not listed in D:/New.text.");
                        Console.WriteLine("Please, run the program again after listing the issues.");
                        break;
                    }       
            }
            Console.WriteLine("Press Enter to Exit.");
            Console.ReadLine();
        }
    }
}
