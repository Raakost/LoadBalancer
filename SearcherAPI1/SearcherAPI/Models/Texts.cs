using Newtonsoft.Json;

namespace SearcherAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Texts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Texts()
        {
            SearchWords = new HashSet<SearchWords>();
        }

        public int id { get; set; }

        [Required]
        public string text { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<SearchWords> SearchWords { get; set; }
    }
}
