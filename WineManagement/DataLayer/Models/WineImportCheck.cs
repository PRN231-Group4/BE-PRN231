﻿using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class WineImportCheck
    {
        public int CheckId { get; set; }
        public int? RequestId { get; set; }
        public int? BatchId { get; set; }
        public int? InspectorId { get; set; }
        public int? WineId { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }
        public DateTime? CheckDate { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public virtual WineBatch? Batch { get; set; }
        public virtual WineImportRequest? Request { get; set; }
        public virtual Wine? Wine { get; set; }
    }
}
