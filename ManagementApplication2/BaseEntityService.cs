using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication
{
    internal class BaseEntityService<T> : IBaseEntityService<T>
        where T : IBaseEntity, new()
    {
        private List<T> Reposetore {  get; set; }

        public BaseEntityService(IEnumerable<T> baseEntities) 
        { 
            Reposetore = baseEntities.ToList();
        }

        public T Create(string name, StatusTD status)
        {
            var item = new T() 
            { 
                Id = new Guid(name),
                Name = name,
                Status = status
            };

            Reposetore.Add(item);
            return item;
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllByName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllByStatus()
        {
            throw new NotImplementedException();
        }
    }
}
