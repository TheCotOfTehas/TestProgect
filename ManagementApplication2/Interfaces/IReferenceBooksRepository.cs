using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Interfaces
{
    public interface IReferenceBooksRepository
    {
        public IBaseEntity Create { get; set; }
        public IBaseEntity Delete { get; set; }
        public IBaseEntity SendToArchive { get; set; }      
        public IEnumerable<IBaseEntity> GetAllByName { get; set; }
        public IEnumerable<IBaseEntity> GetAllByStatus { get; set; }
    }
}
