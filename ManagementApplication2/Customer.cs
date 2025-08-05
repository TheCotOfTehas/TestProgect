using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication
{
    internal class Customer : IBaseEntity
    {
        [Required]
        public Guid Id { get; set; }
        public Address AddressCustomer { get; set; }
        
        public string Name { get; set; }
        public StatusTD Status { get; set; }
    }
}
