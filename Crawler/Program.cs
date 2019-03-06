using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace Crawler
{
    class Program
    {
        private static string _url = "https://chris-forbes.com";
        private static IList<string> _pages;
        private static IList<string> _visitedPages;
        private static IDictionary<string, IPage> _dictionary;

        static int Main(string[] args)
        {
            try
            {
                _pages = new List<string>();
                _visitedPages = new List<string>();
                _dictionary = new Dictionary<string, IPage>();

                _pages.Add(_url);
                StartCrawler(_url);

                string json = JsonConvert.SerializeObject(_dictionary.Values, Formatting.Indented);
                Console.WriteLine(json);

                var output = $@"Crawler Summary:
- Internal Pages : {_pages.Count}
- Visited Pages  : {_visitedPages.Count}
";

                Console.WriteLine(output);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        public static void StartCrawler(string url)
        {
            if (_visitedPages.Contains(url))
            {
                return;
            }

            _visitedPages.Add(url);
            var page = new Page
            {
                Url = new Uri(url)
            };

            var source = GetPageSource(url);
            foreach (var value in GetAttributeValue(source, "a", "href"))
            {
                if (value == url || value == _url || value.StartsWith('#') || string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }
                else if (value.StartsWith(_url))
                {
                    if (!page.InternalLinks.Contains(value))
                    {
                        page.InternalLinks.Add(value);
                    }
                }
                else if (value.StartsWith('/'))
                {
                    var absoluteUrl = _url + value;
                    if (absoluteUrl != url || !page.InternalLinks.Contains(value))
                    {
                        page.InternalLinks.Add(absoluteUrl);
                    }
                }
                else
                {
                    if (value.StartsWith("http") && !page.ExternalLinks.Contains(value))
                    {
                        page.ExternalLinks.Add(value);
                    }
                }
            }

            _dictionary.Add(url, page);
            foreach (var link in page.InternalLinks)
            {
                if (!_visitedPages.Contains(link))
                {
                    _pages.Add(link);
                    StartCrawler(link);
                }
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
