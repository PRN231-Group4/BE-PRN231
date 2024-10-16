using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Account
    {
        public Account()
        {
            WineChecks = new HashSet<WineCheck>();
            WineRequests = new HashSet<WineRequest>();
            WineTransactions = new HashSet<WineTransaction>();
        }

        public int AccountId { get; set; }
        public int? RoleId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Status { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<WineCheck> WineChecks { get; set; }
        public virtual ICollection<WineRequest> WineRequests { get; set; }
        public virtual ICollection<WineTransaction> WineTransactions { get; set; }
    }
}
