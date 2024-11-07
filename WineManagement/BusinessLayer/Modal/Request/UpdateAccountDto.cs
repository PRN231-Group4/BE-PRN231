using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Request
{
    public class UpdateAccountDto
    {
        public int RoleId { get; set; }
        public string Username { get; set; } = null!;
        public string Status { get; set; } = null;

    }
}
