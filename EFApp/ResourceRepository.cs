using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFApp
{
    internal class ResourceRepository : IDocumentRepository
    {
        public List<ResourceReceipt> resourceReceipts { get; set; }
        public List<ResourceShipment> resourceShipments { get; set; }
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
