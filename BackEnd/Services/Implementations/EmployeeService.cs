using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDAL _employeeDAL;

        public EmployeeService(IEmployeeDAL employeeDAL)
        {
            _employeeDAL = employeeDAL;
        }

        public void Add(Employee entity)
        {
            _employeeDAL.Add(entity);
        }

        public void Update(Employee entity)
        {
            _employeeDAL.Update(entity);
        }

        public void Delete(int id)
        {
            _employeeDAL.Delete(id);
        }

        public EmployeeDTO GetById(int id)
        {
            var employee = _employeeDAL.GetById(id);
            return new EmployeeDTO
            {
                IdEmployee = employee.IdEmployee,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Role = employee.Role,
                Phone = employee.Phone,
                Email = employee.Email
            };
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            return _employeeDAL.GetAll().Select(employee => new EmployeeDTO
            {
                IdEmployee = employee.IdEmployee,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Role = employee.Role,
                Phone = employee.Phone,
                Email = employee.Email
            }).ToList();
        }
    }
}
