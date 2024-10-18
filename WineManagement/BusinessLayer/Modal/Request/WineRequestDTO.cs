using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Request
{
    public class WineRequestDTO
    {
        public int RequestId { get; set; }
        public string? SupplierName { get; set; }
        public string? ManagerName { get; set; }
        public string? Wine { get; set; }
        public int SupplierId { get; set; }
        public int? ManagerId { get; set; }
        public int? WineId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
