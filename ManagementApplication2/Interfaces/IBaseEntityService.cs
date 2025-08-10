using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Interfaces
{
    public interface IBaseEntityService
    {
        public IEnumerable<IBaseEntity> Create();
        public IEnumerable<IBaseEntity> Delete();
        public IEnumerable<IBaseEntity> Edit();
        public IEnumerable<IBaseEntity> GetAllByName();
        public IEnumerable<IBaseEntity> GetAllByStatus();
    }
}
