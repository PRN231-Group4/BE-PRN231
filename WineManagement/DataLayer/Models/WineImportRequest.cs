using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class WineImportRequest
    {
        public WineImportRequest()
        {
            WineBatches = new HashSet<WineBatch>();
            WineImportChecks = new HashSet<WineImportCheck>();
        }

        public int RequestId { get; set; }
        public int? SupplierId { get; set; }
        public int? ManagerId { get; set; }
        public int? WineId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? RequestData { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual Wine? Wine { get; set; }
        public virtual ICollection<WineBatch> WineBatches { get; set; }
        public virtual ICollection<WineImportCheck> WineImportChecks { get; set; }
    }
}
