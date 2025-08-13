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


        public async Task<T> CreateAsync(string name, StatusTD status)
        {
            var item = new T()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Status = status,
            };

            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> CreateAsync(T baseEntity)
        {
            var item = new T()
            {
                Id = Guid.NewGuid(),
                Name = baseEntity.Name,
                Status = baseEntity.Status
            };

            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> ArchiveAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Сущность не найдена.");

            if (IsEntityUsed(id)) 
                return false;

            entity.Status = StatusTD.Archived;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAllByNameAsync(string name)
        {
            return await _context.Set<T>().Where(entity => entity.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByStatusAsync(StatusTD status)
        {
            return await _context.Set<T>().Where(entity => entity.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByNameAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> EditAsync(T baseEntity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(baseEntity.Id);
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
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists(baseEntity.Id).Result) return false;
                    else throw;
                }

            return true;
        }

        public async Task<bool> EntityExists(Guid id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }

        //public async Task<bool> EditAsync(T baseEntity)
        //{
        //    return await EditAsync(baseEntity);
        //}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        //Не реализован(заглушка)
        private bool IsEntityUsed(Guid id)
        {
            return false;
        }
    }
}
