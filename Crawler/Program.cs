using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;

namespace Crawler
{
    class Program
    {
        private static string _url = "https://chris-forbes.com";

        static void Main(string[] args)
        {
            var source = GetPageSource(_url);
            foreach (var value in GetAttributeValue(source, "a", "href"))
            {
                Console.WriteLine(value);
            }
        }

        public static string GetPageSource(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }

                return string.Empty;
            }
        }

        public static IList<string> GetAttributeValue(string htmlSource, string element, string attribute)
        {
            try
            {
                var document = new HtmlDocument();
                document.LoadHtml(htmlSource);
                return document.DocumentNode.SelectNodes($"//{element}[@{attribute}]").Select(node => node.Attributes[attribute].Value).ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
