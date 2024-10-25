using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            WineImportRequests = new HashSet<WineImportRequest>();
        }

        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<WineImportRequest> WineImportRequests { get; set; }
    }
}
