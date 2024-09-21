using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Category
    {
        public Category()
        {
            Wines = new HashSet<Wine>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Wine> Wines { get; set; }
    }
}
