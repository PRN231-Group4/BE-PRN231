namespace View_Wine.Models
{
    public class ReportModal
    {
        public int? BatchId { get; set; }
        public int? AccountId { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int ReportId { get; set; }
    }
}
