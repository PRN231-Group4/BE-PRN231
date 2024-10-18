namespace View_Wine.Models
{
    public class WineRequestModal
    {
        public int RequestId { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Wine { get; set; }
        public string ManagerName { get; set; }

        public int? StaffId { get; set; }
        public int? WineId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
