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
        public void Delete(Guid id);
        public IEnumerable<T> GetAllByName(string name);
        public IEnumerable<T> GetAllByStatus(StatusTD status);
    }
}
