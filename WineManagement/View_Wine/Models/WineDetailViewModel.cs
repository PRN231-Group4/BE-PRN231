using BusinessLayer.Modal.Request;

namespace View_Wine.Models
{
    public class WineDetailViewModel
    {
        public WineModal Wine { get; set; }
        public List<WineCheckDTO> WineChecks { get; set; }
    }
}
