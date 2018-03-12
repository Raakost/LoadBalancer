using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadBalancer.Models
{
    public class TextsWithLog
    {
        public List<Texts> Texts { get; set; }
        public MonitorLog MonitorLog { get; set; }
    }
}