using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoadBalancer.Models;

namespace LoadBalancer.LoadHelper
{
    public class RoundRobinHelper
    {
        private static List<string> URLs = new List<string>();

        private static RoundRobinHelper instance;
        private RoundRobinHelper()
        {
            URLs.Add("http://searcherapi1.azurewebsites.net/");
            URLs.Add("http://searcherapi2.azurewebsites.net/");

        }

        public static RoundRobinHelper getInstance()
        {
            if (instance != null) return instance;

            instance = new RoundRobinHelper();
            return instance;

        }
        public string GetNextUrl()
        {
            URLs.Reverse();
            System.Diagnostics.Debug.WriteLine(URLs[0]);
            return URLs[0];
        }
    }
}