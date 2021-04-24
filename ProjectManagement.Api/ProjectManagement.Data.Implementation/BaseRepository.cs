using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ProjectManagementContext _projectManagementContext;

        public BaseRepository(ProjectManagementContext projectManagementContext)
        {
            this._projectManagementContext = projectManagementContext;
           // _projectManagementContext.AddInitialData();
        }
        public async Task<T> Add(T entity)
        {
            _projectManagementContext.Add<T>(entity);
            await _projectManagementContext.SaveChangesAsync();
            return entity;
        }

        public async Task<int> Delete(long id)
        {
            _projectManagementContext.Remove<T>(Get(id));
           return await _projectManagementContext.SaveChangesAsync();
        }

        public IQueryable<T> Get()
        {
            return _projectManagementContext.Set<T>();
        }

        public T Get(long id)
        {
            return _projectManagementContext.Set<T>().Where(i => i.ID == id).FirstOrDefault();
        }

        public async Task<T> Update(T entity)
        {
            _projectManagementContext.Update<T>(entity);
            await _projectManagementContext.SaveChangesAsync();
            return entity;
        }
    }
}
