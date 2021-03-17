﻿using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> Get();

        T Get(long id);

        T Add(T entity);

        T Update(T entity);

        Task<int> Delete(long id);

    }
}
