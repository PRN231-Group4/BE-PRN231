using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class WineTransaction
    {
        public int TransactionId { get; set; }
        public int? BatchId { get; set; }
        public int? WineId { get; set; }
        public int? InspectorId { get; set; }
        public int? LocationId { get; set; }
        public int? Quantity { get; set; }
        public string? TransType { get; set; }
        public DateTime? TransDate { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public virtual WineBatch? Batch { get; set; }
        public virtual WineStorageLocation? Location { get; set; }
        public virtual Wine? Wine { get; set; }
    }
}
