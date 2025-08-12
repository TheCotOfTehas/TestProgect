using ManagementApplication.BaseEntity;
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
        public T Create(T baseEntity);
        public bool Delete(Guid id);
        public bool Edit(T baseEntity);
        public IEnumerable<T> GetAllByName();
        public IEnumerable<T> GetAllByName(string name);
        public IEnumerable<T> GetAllByStatus(StatusTD status);
        public void SaveChanges();
    }
}
