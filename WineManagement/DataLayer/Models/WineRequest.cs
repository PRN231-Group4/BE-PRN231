using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class WineRequest
    {
        public WineRequest()
        {
            WineBatches = new HashSet<WineBatch>();
            WineChecks = new HashSet<WineCheck>();
        }

        public int RequestId { get; set; }
        public int? SupplierId { get; set; }
        public int? ManagerId { get; set; }
        public int? WineId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual Account? Manager { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<WineBatch> WineBatches { get; set; }
        public virtual ICollection<WineCheck> WineChecks { get; set; }
    }
}
