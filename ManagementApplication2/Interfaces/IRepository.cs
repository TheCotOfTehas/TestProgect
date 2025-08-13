using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Interfaces
{
    public interface IRepository<T> where T : IBaseEntity
    {
        public T GetById(Guid id);
        public List<T> GetAll();
        public void Add(T entity);
        public void Update(T entity);
        public bool ExistsByName(string name, Guid? excludeId = null);
    }
}
