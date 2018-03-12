using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadBalancer.Models
{
    public class MonitorLog
    {
        public int Id { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string URL { get; set; }
    }
}