using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Request
{
	public class AdminCreateAccountModel
	{
		public string Username { get; set; }
		//public string Password { get; set; }
		public int RoleId { get; set; }
		public bool Status { get; set; }

	}

	public class RegisterModel : AdminCreateAccountModel
	{
		public string Password { get; set; }


	}
}
