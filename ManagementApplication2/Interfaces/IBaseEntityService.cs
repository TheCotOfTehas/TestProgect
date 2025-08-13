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
        public Task<T> CreateAsync(string name, StatusTD status);
        public Task<T> CreateAsync(T baseEntity);
        public Task<bool> ArchiveAsync(Guid id);
        public Task<bool> EditAsync(T baseEntity);
        public Task<IEnumerable<T>> GetAllByNameAsync();
        public Task<IEnumerable<T>> GetAllByNameAsync(string name);
        public Task<IEnumerable<T>> GetAllByStatusAsync(StatusTD status);
        public Task SaveChangesAsync();
    }
}
