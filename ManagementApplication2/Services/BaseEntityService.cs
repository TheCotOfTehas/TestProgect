using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Services
{
    internal class BaseEntityService : IBaseEntityService
    {
        public IBaseEntity Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IBaseEntity Edit()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBaseEntity> GetAllByName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBaseEntity> GetAllByStatus()
        {
            throw new NotImplementedException();
        }
    }
}
