using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Interfaces
{
    public interface IBaseEntityService<T> where T : IBaseEntity
    {
        public T Create(string name, StatusTD status);
        public void Delete();
        public IEnumerable<T> GetAllByName();
        public IEnumerable<T> GetAllByStatus();
    }
}
