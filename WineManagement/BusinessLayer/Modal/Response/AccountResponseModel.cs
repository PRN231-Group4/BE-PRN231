using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Modal.Response
{
	public class AccountResponseModel
	{
		public int AccountId { get; set; }
		public string Username { get; set; } = null!;
		public int RoleId { get; set; }
		public string Status { get; set; }

	}
}
