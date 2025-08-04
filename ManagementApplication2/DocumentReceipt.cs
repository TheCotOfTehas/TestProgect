using ManagementApplication.Interfaces;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication
{
    internal class DocumentReceipt : IDocument
    {
        [Required]
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime DateTime { get; set; }
    }
}
