using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Request
{
    public class WineRequestItemDTO
    {
        public int WineId { get; set; }
        public int Quantity { get; set; } // Số lượng cho từng loại rượu
    }
    public class WineRequestCRUDDTO
    {
        public int? SupplierId { get; set; }

        public int RequestId { get; set; }
        public int? ManagerId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        public List<WineRequestItemDTO>? WineItems { get; set; } // Danh sách các loại rượu và số lượng

    }
}
