using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication
{
    internal class Balance
    {
        [Required]
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public Guid UnitId { get; set; }
        public double Amount { get; set; }

    }
}
