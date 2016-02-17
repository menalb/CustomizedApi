using CustomApi.CustomizedApi;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CustomApi.Server
{
    public class Program
    {
        private const string baseAddress = "http://localhost:3210/";

        static void Main()
        {
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Web Server is running. {baseAddress}");
                Console.WriteLine("Press any key to quit.");

                SendMessages();

                Console.ReadLine();
            }
        }

        private static void SendMessages()
        {
            new List<CustomizedApi.Models.CustomProductModel>
            {
                new CustomizedApi.Models.CustomProductModel
                {
                    Name = "Sweater",
                    Brand = "Benetton"
                },
                new CustomizedApi.Models.CustomProductModel
                {
                    Name = "Shirt",
                    Brand = "Boss"
                }
            }.ForEach(product => Send(product));
        }

        private static void Send(CustomizedApi.Models.CustomProductModel product)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);

            var ser = Newtonsoft.Json.JsonConvert.SerializeObject(product);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/products");
            request.Content = new StringContent(ser, System.Text.Encoding.UTF8, "application/custom-productmodel-type");

            client
                .SendAsync(request)
                .ContinueWith(responseTask =>
                {
                    Console.WriteLine("Response: {0}", responseTask.Result);
                });
        }
    }
}