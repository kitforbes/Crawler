using System;
using System.Collections.Generic;

namespace Crawler
{
    public interface IPage
    {
        Uri Url { get; set; }
        IList<string> Images { get; set; }
        IList<string> InternalLinks { get; set; }
        IList<string> ExternalLinks { get; set; }
    }
}
