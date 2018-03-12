using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadBalancer.Models
{
    public class SearchWordsWithLog
    {
        public List<SearchWords> SearchWords { get; set; }
        public MonitorLog MonitorLog { get; set; }
    }
}