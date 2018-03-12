using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadBalancer.Models
{
    public class SearchWords
    {
        public SearchWords()
        {
            Texts = new HashSet<Texts>();
        }

        public int id { get; set; }

        public string SearchWord { get; set; }

        public virtual ICollection<Texts> Texts { get; set; }
        
    }
}
