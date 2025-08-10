using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Document
{
    public class DocumentShipment : IDocument
    {
        [Required]
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime DateTime { get; set; }
        public Guid CustomerID { get; set; }

        public StatusTD Status { get; set; }
    }
}
