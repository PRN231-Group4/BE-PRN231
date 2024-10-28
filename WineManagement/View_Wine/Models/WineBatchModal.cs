namespace View_Wine.Models
{

    public class WineBatchModal
    {
        public int BatchId { get; set; }
        public int? WineId { get; set; }
        public int? RequestId { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime? ImportDate { get; set; }
        public int? Quantity { get; set; }
        public int? ProductionYear { get; set; }
        public string? Status { get; set; }


    }
}
