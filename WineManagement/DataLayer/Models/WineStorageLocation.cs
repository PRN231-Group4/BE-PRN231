using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class WineStorageLocation
    {
        public WineStorageLocation()
        {
            WineTransactions = new HashSet<WineTransaction>();
        }

        public int LocationId { get; set; }
        public int? FloorNumber { get; set; }
        public string? Zone { get; set; }
        public string? ShelfCode { get; set; }
        public int? Capacity { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<WineTransaction> WineTransactions { get; set; }
    }
}
