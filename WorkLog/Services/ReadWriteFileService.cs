using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog.Services
{
    class ReadWriteFileService
    {
        public void WriteFileToHtml(string fileContents)
        {
            try
            {
                var contents= fileContents.Replace("\r\n\r\n", "");
                //if (File.Exists(@"D:\New.txt"))
                //{
                //    File.Delete(@"D:\New.txt");
                //}
                if (File.Exists(@"D:\New.html"))
                {
                    File.Delete(@"D:\New.html");
                }
                //File.WriteAllText(@"D:\New.txt", contents);
                File.WriteAllText(@"D:\New.html", contents);
                Console.WriteLine("Verify the file in D:/New.html");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while writing contents to file");
            }
        }

        public string ReadFile()
        {
            string issuesList = String.Empty;
            if (File.Exists(@"D:\New.html"))
            {
                issuesList = File.ReadAllText(@"D:\New.html");
            }
            return issuesList;
        }
    }
}
