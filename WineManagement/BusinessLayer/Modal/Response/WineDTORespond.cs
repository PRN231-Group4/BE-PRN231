using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Response
{
    public class WineDTORespond
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Origin { get; set; }
        public decimal? Volume { get; set; }
        public decimal? AlcContent { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
