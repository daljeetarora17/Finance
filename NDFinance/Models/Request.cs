using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NDFinance.Models
{
    public static class Request
    {
        public static void HttpMethodPost(string postUrl, dynamic postData)
        {
            using (var client = new HttpClient())
            {               
                client.BaseAddress = new Uri("http://localhost:52667/");
                var response = client.PostAsync(postUrl, postData).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }

        }
        
    }
}
