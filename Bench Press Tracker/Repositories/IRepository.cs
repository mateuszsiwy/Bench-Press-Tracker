﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bench_Press_Tracker.Repositories
{
    public interface IRepository<T> where T: class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
        void SaveChanges();
    }
}
