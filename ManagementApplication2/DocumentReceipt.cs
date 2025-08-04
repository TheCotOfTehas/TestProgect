using ManagementApplication.Interfaces;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication
{
    internal class DocumentReceipt : IDocument
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime DateTime { get; set; }
    }
}
