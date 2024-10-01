using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Request
{
    public class WineBatchDTO
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
