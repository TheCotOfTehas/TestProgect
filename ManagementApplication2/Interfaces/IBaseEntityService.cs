using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Interfaces
{
    public interface IBaseEntityService
    {
        public IBaseEntity Create();
        public void Delete();
        public IBaseEntity Edit();
        public IEnumerable<IBaseEntity> GetAllByName();
        public IEnumerable<IBaseEntity> GetAllByStatus();
    }
}
