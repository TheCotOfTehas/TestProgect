using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Interfaces
{
    public interface IDocumentRepository
    {
        public IEnumerable<ResourceShipment> GetByData(string name);
        public IEnumerable<ResourceShipment> GetByResource(string name);
        public IEnumerable<ResourceReceipt> GetByNumber(string name);
        public IEnumerable<ResourceReceipt> GetByUnit(string name);
        public IEnumerable<ResourceReceipt> GetByCustomer(string name);
    }
}
