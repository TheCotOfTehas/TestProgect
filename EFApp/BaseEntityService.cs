using EFApp.EntityFrameworkCore;
using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private ApplicationContext _context { get; set; }

        public BaseEntityService(ApplicationContext context)
        {
            _context = context;
        }


        public T Create(string name, StatusTD status)
        {
            var item = new T()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Status = status,
            };

            _context.Set<T>().Add(item);
            _context.SaveChanges();
            return item;
        }

        public T Create(T baseEntity)
        {
            var item = new T()
            {
                Id = Guid.NewGuid(),
                Name = baseEntity.Name,
                Status = baseEntity.Status
            };

            _context.Set<T>().Add(item);
            _context.SaveChanges();
            return item;
        }

        public bool Delete(Guid id)
        {
            var item = _context.Set<T>().Find(id);
            if (item != null)
            {
                _context.Set<T>().Remove(item);
                _context.SaveChanges();
                return true;
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

        public IEnumerable<T> GetAllByName()
        {
            return _context.Set<T>().ToList();
        }

        public bool Edit(T baseEntity)
        {
            var existingEntity = _context.Set<T>().Find(baseEntity.Id);
            if (existingEntity is null) return false;
            bool hasChanges = false;

            if (existingEntity.Name != baseEntity.Name || existingEntity.Status != baseEntity.Status)
            {
                existingEntity.Name = baseEntity.Name;
                existingEntity.Status = baseEntity.Status;
                hasChanges = true;
            }

            if (hasChanges)
                try
                {
                    _context.Set<T>().Update(existingEntity);
                    _context.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists(baseEntity.Id)) return false;
                    else throw;
                }

            return true;
        }

        public bool EntityExists(Guid id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }

        bool IBaseEntityService<T>.Edit(T baseEntity)
        {
            return Edit(baseEntity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
