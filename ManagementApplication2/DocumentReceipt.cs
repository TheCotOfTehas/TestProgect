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
        public int Id => throw new NotImplementedException();

        public int Number { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
