using Microsoft.AspNetCore.Mvc.Rendering;

namespace View_Wine.Models
{

    public class WineRequestItemModal
    {
        public int WineId { get; set; }
        public int Quantity { get; set; } // Số lượng cho từng loại rượu
    }
    public class WineRequestModal
    {
        public int? SupplierId { get; set; }

        public int RequestId { get; set; }
        public int? ManagerId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string SupplierName { get; set; }
        public string Wine { get; set; }
        public string ManagerName { get; set; }

        public List<WineRequestItemModal> WineItems { get; set; } // Danh sách các loại rượu và số lượng
        public SelectList SupplierList { get; set; }
        public SelectList WineList { get; set; }
        public SelectList StaffList { get; set; }


    }
   
}
