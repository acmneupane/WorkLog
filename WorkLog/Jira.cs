using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WorkLog
{
    public class Jira
    {

        public async Task JiraAccess()
        {
            int counter = 0, linenum = 0;
            string line;
            Console.WriteLine("");
            Console.WriteLine("Execute Jira List of Issues?? (Type 'Y' or 'y' if YES or Any other Key if NO)");
            string execute = Console.ReadLine();
            if (execute[0] == 'Y' || execute[0] == 'y') 
            {
                try
                {
                    string jql = "http://www.jira.codebee.dk:8080/rest/api/latest/search?jql=lastViewed%3EstartOfDay%28%22-0d%22%29%26updated%3EstartOfDay%28%22-0d%22%29%26watcher%3Dane%26assignee%21%3Dempty&fields=summary,id,key%20ORDER%20BY%20lastViewed%20DESC";
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + "a.html";
                    HttpClient client = new HttpClient();
                    string url = "http://jira.codebee.dk:8080/rest/api/latest";
                    Uri myUri = new Uri(url, UriKind.Absolute);
                    client.BaseAddress = myUri;
                    var byteArray = Encoding.ASCII.GetBytes("username:password");
                    var header = new AuthenticationHeaderValue(
                               "Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(jql);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        ListOfIssues issues = JsonConvert.DeserializeObject<ListOfIssues>(responseBody);
                        System.IO.StreamReader file = new System.IO.StreamReader(path);
                        var numlines = File.ReadLines(path).ToList();
                        while ((line = file.ReadLine()) != null)
                        {
                            if (linenum < numlines.Count)
                            {
                                if (numlines[linenum] == "\t</ul>")
                                {
                                    int linetoAdd = linenum;
                                    for (counter = 0; counter < issues.issues.Count; counter++)
                                    {
                                        string newline = "\t\t<li> " + @issues.issues[counter].fields.summary + " (" + @issues.issues[counter].key + ") </li>\t";
                                        AddToWorklog(numlines, linetoAdd, newline);
                                        linetoAdd++;
                                    }
                                    linenum = linetoAdd;
                                }
                                AddToWorklog(numlines, linenum, line);
                                linenum++;
                            }
                            else
                            {
                                numlines.Add(line);
                                linenum++;
                            }
                        }
                        if(File.Exists(@"C:\WorkLog.txt"))
                        {
                            File.Delete(@"C:\WorkLog.txt");
                        }
                        file.Close();
                        File.WriteAllLines(@"C:\WorkLog.txt", numlines);
                        Console.WriteLine("Succesfully listed in C:Worklog.txt.");
                    }
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("Connection Error");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
            else
            {
                Console.WriteLine("Jira Issues not listed.");
            }
        }

        public void AddToWorklog(List<String> numlines, int linetoAdd, string newline)
        {
            if (linetoAdd < numlines.Count)
            {
                numlines[linetoAdd] = newline;
            }
            else
            {
                numlines.Add(newline);
            } 
        }
    }
}
