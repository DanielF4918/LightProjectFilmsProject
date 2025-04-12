using Domain.Domain;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Implementations
{
    public class DALGenericoImpl<T> : IDALGenerico<T> where T : class
    {
        private readonly RentalSystem _context;
        private readonly DbSet<T> _dbSet;

        public DALGenericoImpl(RentalSystem context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            Console.WriteLine($"Attempting to delete {typeof(T).Name} with ID: {id}");

            var entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
                Console.WriteLine($"Successfully deleted {typeof(T).Name} with ID: {id}");
            }
            else
            {
                Console.WriteLine($"No {typeof(T).Name} found with ID: {id} for deletion");
                throw new InvalidOperationException($"Entity {typeof(T).Name} with ID {id} not found");
            }
        }

        public virtual T GetById(int id)
        {
            Console.WriteLine($"Looking for {typeof(T).Name} with ID: {id}");

            var entity = _context.Set<T>().Find(id);

            if (entity == null)
                Console.WriteLine($"No {typeof(T).Name} found with ID: {id}");
            else
                Console.WriteLine($"Found {typeof(T).Name} with ID: {id}");

            return entity!;
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
