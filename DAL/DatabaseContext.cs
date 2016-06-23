using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    /// <summary>
    /// Database Context
    /// </summary>
    /// 
    /// publi 

    public class DatabaseContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public DatabaseContext() : base("IssueTrackerDatabase") { }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
    /*
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>()
            .MapToStoredProcedures(s => s
                .Update(u => u.HasName("modify_customer")
                    .Parameter(b => b.DateOfBirth, "BirthDay"))
                .Delete(d => d.HasName("delete_customer"))
                .Insert(i => i.HasName("insert_customer"))
            );
    }*/
    
}
