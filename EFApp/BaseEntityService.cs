using EFApp.EntityFrameworkCore;
using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFApp
{
    public class BaseEntityService<T> : IBaseEntityService<T>
        where T : class, IBaseEntity, new()
    {
        public ApplicationContext _context { get; set; }

        public BaseEntityService(ApplicationContext context)
        {
            _context = context;
        }

        public T Create(string name, StatusTD status)
        {
            var item = new T()
            {
                Id = Guid.NewGuid(), // Генерация нового Id
                Name = name,
                Status = status
            };

            _context.Set<T>().Add(item);
            _context.SaveChanges(); // Сохранение изменений в базе данных
            return item;
        }

        public void Delete(Guid id)
        {
            var item = _context.Set<T>().Find(id);
            if (item != null)
            {
                _context.Set<T>().Remove(item);
                _context.SaveChanges(); // Сохранение изменений
            }
            else
            {
                throw new KeyNotFoundException("Сущность не найдена для удаления.");
            }
        }

        public IEnumerable<T> GetAllByName(string name)
        {
            return _context.Set<T>().Where(entity => entity.Name == name).ToList();
        }

        public IEnumerable<T> GetAllByStatus(StatusTD status)
        {
            return _context.Set<T>().Where(entity => entity.Status == status).ToList();
        }
    }
}
