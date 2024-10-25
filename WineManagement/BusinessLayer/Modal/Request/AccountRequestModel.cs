using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Request
{
	public class AccountRequestModel
	{
		public string UserName { get; set; } = null!;
		public string Password { get; set; } = null!;
		public int RoleId { get; set; }
		public string Status { get; set; }
	}

}
