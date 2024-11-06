using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Report
    {
        public int? BatchId { get; set; }
        public int? AccountId { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int ReportId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual WineBatch? Batch { get; set; }
    }
}
