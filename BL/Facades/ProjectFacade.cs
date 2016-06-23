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
    public class ProjectFacade
    {
        public void CreateProject(ProjectDTO project)
        {
            var newProject = Mapping.Mapper.Map<Project>(project);

            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Projects.Add(newProject);
                context.SaveChanges();
            }
        }
        public void CreateProject(ProjectDTO project, int id)
        {
            var newProject = Mapping.Mapper.Map<Project>(project);
            bool found = false;
            DAL.Entities.Customer customer;
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var oldCustomer = context.Customers.Find(id);
                if (oldCustomer != null)
                {
                    customer = oldCustomer;
                    found = true;
                }
                else
                {
                    customer = new DAL.Entities.Customer {Id = id};
                }
            }
            //customer.Projects.Add(newProject);
            newProject.Customer = customer;
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                if (found)
                {
                    context.Entry(customer).State = EntityState.Unchanged;
                }
                context.Projects.Add(newProject);
                context.SaveChanges();
            }
        }

        public void UpdateProject(ProjectDTO project)
        {
            var newProject = Mapping.Mapper.Map<Project>(project);

            using (var context = new DatabaseContext())
            {
                context.Entry(newProject).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProject(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var project = context.Projects.Find(id);
                context.Projects.Remove(project);
                context.SaveChanges();
            };
        }

        public ProjectDTO GetProjectById(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var project = context.Projects.Find(id);
                return Mapping.Mapper.Map<ProjectDTO>(project);
            }
        }

        public CustomerDTO GetProjectsCustomer(int id)
        {
            using (var context = new DatabaseContext())
            {
                var customer = new CustomerDTO();
                context.Database.Log = Console.WriteLine;
                var customers = context.Customers;
                foreach (var var in customers)
                {
                    var projectsOfCustomer = var.Projects;
                    foreach (var temp in projectsOfCustomer)
                    {
                        if (temp.Id == id)
                        {
                            return Mapping.Mapper.Map<CustomerDTO>(var);
                        }
                    }
                        
                }

                return Mapping.Mapper.Map<CustomerDTO>(customer);
            }
        }

        public List<ProjectDTO> GetAllProjects()
        {
            using (var context = new DatabaseContext())
            {
                var project = context.Projects.ToList();
                return project
                    .Select(element => Mapping.Mapper.Map<ProjectDTO>(element))
                    .ToList();
            }
        }


    }
}
