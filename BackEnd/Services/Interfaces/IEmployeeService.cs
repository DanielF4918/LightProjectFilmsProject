using BackEnd.DTO;
using System.Collections.Generic;
using Domain.Domain;

namespace BackEnd.Services.Interfaces
{
    public interface IEmployeeService
    {
        void Add(Employee entity);
        void Update(Employee entity);
        void Delete(int id);
        EmployeeDTO GetById(int id);
        IEnumerable<EmployeeDTO> GetAll();
    }
}
