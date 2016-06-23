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
    public class CustomerFacade
    {
        public void CreateCustomer(CustomerDTO customer)
        {
            var newCustomer = Mapping.Mapper.Map<Customer>(customer);

            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Customers.Add(newCustomer);
                context.SaveChanges();
            }
        }

        public void UpdateCustomer(CustomerDTO customer)
        {
            var newCustomer = Mapping.Mapper.Map<Customer>(customer);

            using (var context = new DatabaseContext())
            {
                context.Entry(newCustomer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCustomer(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var customer = context.Customers.Find(id);
                context.Customers.Remove(customer);
                context.SaveChanges();
            };
        }

        public void DeleteCustomer(CustomerDTO customer)
        {
            var newCustomer = Mapping.Mapper.Map<Customer>(customer);

            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Entry(newCustomer).State = EntityState.Deleted;
                context.SaveChanges();
            };
        }

        /*
        public void DeleteCustomerViaSql(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand("exec delete_customer {0}", id);
                context.SaveChanges();
            };
        }
        */

        public List<CustomerDTO> GetAllCustomers()
        {
            using (var context = new DatabaseContext())
            {
              //  var customers = context.Customers.ToList();
              //  return (Mapper.Map<List< CustomerDTO >>(customers));

               var customers = context.Customers.ToList();
                 var ret = new List<CustomerDTO>();
                 foreach (var cus in customers)
                 {
                     var temp = Mapping.Mapper.Map<CustomerDTO>(cus);
                  ret.Add(temp);
                 }

                 return ret;

            }
        }

        public CustomerDTO GetCustomerById(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var customer = context.Customers.Find(id);
                return Mapping.Mapper.Map<CustomerDTO>(customer);
            }
        }

        public CustomerDTO GetCustomerByEmail(int email)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var customer = context.Customers.Find(email);
                return Mapping.Mapper.Map<CustomerDTO>(customer);
            }
        }

    }
}