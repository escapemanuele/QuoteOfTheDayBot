using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QuoteOfTheDay
{
    public class Qotd
    {
        public string Quote { get; set; }
        public string Author { get; set; }
    }
}
