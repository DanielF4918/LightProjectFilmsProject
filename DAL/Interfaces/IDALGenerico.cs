using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDALGenerico<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T GetById(int id);
        List<T> GetAll();
    }
}
