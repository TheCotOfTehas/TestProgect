using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Services
{
    internal class DocumentService : IDocumentService
    {
        public IDocument Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IDocument Edit()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResourceReceipt> GetByCustomer(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResourceShipment> GetByData(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResourceReceipt> GetByNumber(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResourceShipment> GetByResource(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResourceReceipt> GetByUnit(string name)
        {
            throw new NotImplementedException();
        }
    }
}
