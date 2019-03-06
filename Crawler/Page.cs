using System;
using System.Collections.Generic;

namespace Crawler
{
    public class Page : IPage
    {
        public Uri Url { get; set; }
        public IList<string> Images { get; set; }
        public IList<string> InternalLinks { get; set; }
        public IList<string> ExternalLinks { get; set; }

        public Page()
        {
            Images = new List<string>();
            InternalLinks = new List<string>();
            ExternalLinks = new List<string>();
        }
    }
}
