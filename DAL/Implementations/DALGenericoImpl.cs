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

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Add debug logging
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

        public T GetById(int id)
        {
            // Add debug logging
            Console.WriteLine($"Looking for {typeof(T).Name} with ID: {id}");

            // For Client entity specifically
            if (typeof(T) == typeof(Client))
            {
                // This handles the specific case for Client with the Id_Client column
                return _context.Set<T>().Find(id);
            }

            // Generic implementation for other entities
            var entity = _context.Set<T>().Find(id);

            if (entity == null)
            {
                Console.WriteLine($"No {typeof(T).Name} found with ID: {id}");
            }
            else
            {
                Console.WriteLine($"Found {typeof(T).Name} with ID: {id}");
            }

            return entity;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
