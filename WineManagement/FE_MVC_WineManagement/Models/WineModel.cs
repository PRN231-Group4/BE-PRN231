namespace FE_MVC_WineManagement.Models
{
    public class WineModel
    {
        public int WineId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Origin { get; set; }
        public decimal? Volume { get; set; }
        public decimal? AlcContent { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
