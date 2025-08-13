using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFApp
{
    internal class MemoryRepository<T> : IRepository<T> where T : IBaseEntity
    {
        private readonly List<T> _items = new();

        public void Add(T entity)
        {
            _items.Add(entity);
        }

        public bool ExistsByName(string name, Guid? excludeId = null)
        {
            var exists = _items.Any(i =>
                string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase)
                && (!excludeId.HasValue || i.Id != excludeId.Value));
            return exists;
        }

        public List<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(Guid id)
        {
            var entity = _items.FirstOrDefault(i => i.Id == id);
            return entity;
        }

        public void Update(T entity)
        {
            var existing = _items.FirstOrDefault(i => i.Id == entity.Id);
            if (existing != null)
            {
                _items.Remove(existing);
                _items.Add(entity);
            }
        }
    }
}
