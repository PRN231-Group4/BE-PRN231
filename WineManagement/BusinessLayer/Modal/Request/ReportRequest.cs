using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Request
{
    public class ReportRequest
    {
        public int? BatchId { get; set; }
        public int? AccountId { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
