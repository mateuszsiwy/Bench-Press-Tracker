using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bench_Press_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Bench_Press_Tracker.Repositories
{
    public class Repository<T>: IRepository<T> where T: class
    {
        private readonly BenchPressContext _context;

        public Repository(BenchPressContext context)
        {
            _context = context;
        }   

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete(int id)
        {
            if (_context.Set<T>().Find(id) != null)
            {
                _context.Set<T>().Remove(_context.Set<T>().Find(id));
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
