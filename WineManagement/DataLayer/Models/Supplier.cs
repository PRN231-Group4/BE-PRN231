using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            WineRequests = new HashSet<WineRequest>();
        }

        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<WineRequest> WineRequests { get; set; }
    }
}
