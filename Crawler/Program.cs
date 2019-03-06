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
        private static IList<string> _pages;
        private static IList<string> _visitedPages;
        private static IList<string> _externalPages;

        static int Main(string[] args)
        {
            try
            {
                _pages = new List<string>();
                _visitedPages = new List<string>();
                _externalPages = new List<string>();

                _pages.Add(_url);
                StartCrawler(_url);

                var output = $@"Crawler Summary:
- Internal Pages : {_pages.Count}
- Visited Pages  : {_visitedPages.Count}
- External Pages : {_externalPages.Count}
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

            var internalLinks = new List<string>();
            var source = GetPageSource(url);
            foreach (var value in GetAttributeValue(source, "a", "href"))
            {
                if (value == url || value == _url || value.StartsWith('#') || string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }
                else if (value.StartsWith(_url))
                {
                    if (!internalLinks.Contains(value))
                    {
                        internalLinks.Add(value);
                    }
                }
                else if (value.StartsWith('/'))
                {
                    var absoluteUrl = _url + value;
                    if (absoluteUrl != url || !internalLinks.Contains(value))
                    {
                        internalLinks.Add(absoluteUrl);
                    }
                }
                else
                {
                    if (value.StartsWith("http") && !_externalPages.Contains(value))
                    {
                        _externalPages.Add(value);
                    }
                }
            }

            foreach (var link in internalLinks)
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
