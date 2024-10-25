using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class WineBatch
    {
        public WineBatch()
        {
            WineImportChecks = new HashSet<WineImportCheck>();
            WineTransactions = new HashSet<WineTransaction>();
        }

        public int BatchId { get; set; }
        public int? WineId { get; set; }
        public int? RequestId { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime? ImportDate { get; set; }
        public int? Quantity { get; set; }
        public int? ProductionYear { get; set; }
        public string? Status { get; set; }

        public virtual WineImportRequest? Request { get; set; }
        public virtual Wine? Wine { get; set; }
        public virtual ICollection<WineImportCheck> WineImportChecks { get; set; }
        public virtual ICollection<WineTransaction> WineTransactions { get; set; }
    }
}
