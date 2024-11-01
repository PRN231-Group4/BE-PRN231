
namespace View_Wine.Models
{
    public class WineCheckModal
    {
        public int CheckId { get; set; }
        public int? RequestId { get; set; }
        public int? InspectorId { get; set; }
        public string? wineName { get; set; }

        public int? WineId { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }
        public DateTime? CheckDate { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
