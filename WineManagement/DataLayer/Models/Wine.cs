﻿using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Wine
    {
        public Wine()
        {
            WineBatches = new HashSet<WineBatch>();
            WineImportChecks = new HashSet<WineImportCheck>();
            WineImportRequests = new HashSet<WineImportRequest>();
            WineTransactions = new HashSet<WineTransaction>();
        }

        public int WineId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Origin { get; set; }
        public decimal? Volume { get; set; }
        public decimal? AlcContent { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<WineBatch> WineBatches { get; set; }
        public virtual ICollection<WineImportCheck> WineImportChecks { get; set; }
        public virtual ICollection<WineImportRequest> WineImportRequests { get; set; }
        public virtual ICollection<WineTransaction> WineTransactions { get; set; }
    }
}
