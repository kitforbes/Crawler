using System;
using System.Collections.Generic;
using System.Linq;

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

        public void OrderLists()
        {
            this.Images = OrderList(this.Images);
            this.InternalLinks = OrderList(this.InternalLinks);
            this.ExternalLinks = OrderList(this.ExternalLinks);
        }

        private IList<T> OrderList<T>(IList<T> list)
        {
            return list.Distinct().OrderBy(item => item).ToList();
        }
    }
}
