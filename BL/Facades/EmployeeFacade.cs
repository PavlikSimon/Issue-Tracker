using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace BL.Facades
{
    public class EmployeeFacade
    {
        public void CreateEmployee(EmployeeDTO employee)
        {
            var newEmployee = Mapping.Mapper.Map<Employee>(employee);

            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Employees.Add(newEmployee);
                context.SaveChanges();
            }
        }

        public void UpdateEmployee(EmployeeDTO employee)
        {
            var newEmployee = Mapping.Mapper.Map<Employee>(employee);

            using (var context = new DatabaseContext())
            {
                context.Entry(newEmployee).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteEmployee(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var employee = context.Employees.Find(id);
                context.Employees.Remove(employee);
                context.SaveChanges();
            };
        }

        public EmployeeDTO GetEmployeeById(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var employee = context.Employees.Find(id);
                return Mapping.Mapper.Map<EmployeeDTO>(employee);
            }
        }

        public List<EmployeeDTO> GetAllEmployees()
        {
            using (var context = new DatabaseContext())
            {
                var employee = context.Employees.ToList();
                return employee
                    .Select(element => Mapping.Mapper.Map<EmployeeDTO>(element))
                    .ToList();
            }
        }


    }
}
