using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Migrations;

namespace Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());


            //drop if changed, pridane 1.5.16 odkomentuj ak chces vymazat
            //Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseIfModelChanges<DatabaseContext>());


            //Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseAlways<DatabaseContext>());

            using (var db = new DatabaseContext())
            {
                //NEW CUSTOMER
                //manual addition
                /*
                Console.WriteLine("Enter new Customer in FORMAT:\nNAME EMAIL ADDRESS");
                string input = Console.ReadLine();
                string[] array = input.Split(' ');

                if (array.Length != 3)
                    throw new ArgumentException("There are 3 arguments needed!");

                var customer = new Customer { Name = array[0], Email = array[1], Address = array[2] };


                // automatic part, it will add TEST customer
                // var customer = new Customer { Name = "testperson", Email = "testmail", Address = "testaddress" };

                //end of autopart, can be exchanged with manual part testing


                db.Customers.Add(customer);
                db.SaveChanges();
                */

                /*
                foreach (var item in customers)
                    Console.WriteLine("ID: " + item.Id + " Name: " + item.Name + " E-Mail: " + item.Email + " Address: " +
                                      item.Address);
                */


                //NEW PROJECT
                /*
                string con = "a";
                while (con.Equals("a"))
                {

                    Console.WriteLine("Enter new Project in FORMAT:\nNAME IDofCustomer");
                string input = Console.ReadLine();
                string[] array = input.Split(' ');

                if (array.Length != 2)
                    throw new ArgumentException("There are 2 arguments needed!");

                var customer = db.Customers.Find(Int32.Parse(array[1]));
                var project = new Project { Name = array[0], Customer = customer, Issue = null};

                db.Projects.Add(project);
                db.SaveChanges();

                Console.WriteLine("\n another? a/n");
                    con = Console.ReadLine();
                }


                var projects = db.Projects;

                foreach (var item in projects)
                    Console.WriteLine("ID: " + item.Id + " Name: " + item.Name + " Customer: " + item.Customer.Id);

            */
            /*
                foreach (var var in db.Projects)
                {
                    Console.WriteLine(var.Id + var.Name + var.Customer.Id + var.Issue.Count);
                }
                */
                foreach (var var in db.Customers)
                {
                    Console.WriteLine(var.Id + var.Name + var.Email + var.Projects.Count + "\n");
                }

                var customer = db.Projects.Find(8);
                Console.WriteLine(customer.Name);

            }
            Console.WriteLine("Enter to exit app");
            string ind = Console.ReadLine();
        }
    }
}
