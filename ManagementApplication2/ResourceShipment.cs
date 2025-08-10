using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication
{
    public class ResourceShipment
    {
        [Required]
        public Guid Id { get; set; }

        public int Number { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public StatusTD Status { get; set; }
    }
}
