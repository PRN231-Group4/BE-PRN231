using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Request
{
    public class WineCheckDTO
    {
        public int CheckId { get; set; }
        public int? RequestId { get; set; }
        public int? InspectorId { get; set; }
        public int? WineId { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }
        public DateTime? CheckDate { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
