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
using WorkLog.Models;

namespace WorkLog.Services
{
    public class JiraAccessService
    {
        public ListOfIssues JiraAccess(string jql)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = "http://jira.codebee.dk:8080/rest/api/latest";
                Uri myUri = new Uri(url, UriKind.Absolute);
                client.BaseAddress = myUri;
                var byteArray = Encoding.ASCII.GetBytes("aseem:k@ngar00");
                var header = new AuthenticationHeaderValue(
                           "Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Authorization = header;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(jql).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    ListOfIssues issues = JsonConvert.DeserializeObject<ListOfIssues>(responseBody);
                    return issues;
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
            return null;
        }
    }
}
