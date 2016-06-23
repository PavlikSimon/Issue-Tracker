using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using BL.DTO;
using DAL;
using DAL.Entities;
using DAL.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace BL.Facades
{
    public class UserFacade
    {
        public void Register(UserDTO user)
        {
            var userManager = new AppUserManager(new AppUserStore(new DatabaseContext()));

            var appUser = Mapping.Mapper.Map<AppUser>(user);

            userManager.Create(appUser, user.Password);

            var ourUser = userManager.FindByName(appUser.UserName);


            if (user.SecretCode != null)
            {
                if (user.SecretCode.Equals("admin"))
                {
                    var roleManager = new AppRoleManager(new AppRoleStore(new DatabaseContext()));

                    if (!roleManager.RoleExists("Admin"))
                    {
                        roleManager.Create(new AppRole { Name = "Admin" });
                    }

                    userManager.AddToRole(ourUser.Id, "Admin");
                }

                if (user.SecretCode.Equals("customer"))
                {
                    var roleManager = new AppRoleManager(new AppRoleStore(new DatabaseContext()));

                    if (!roleManager.RoleExists("Customer"))
                    {
                        roleManager.Create(new AppRole { Name = "Customer" });
                    }

                    userManager.AddToRole(ourUser.Id, "Customer");
                }

                if (user.SecretCode.Equals("employee"))
                {
                    var roleManager = new AppRoleManager(new AppRoleStore(new DatabaseContext()));

                    if (!roleManager.RoleExists("Employee"))
                    {
                        roleManager.Create(new AppRole { Name = "Employee" });
                    }

                    userManager.AddToRole(ourUser.Id, "Employee");
                }
            }


        }

        public ClaimsIdentity Login(string email, string password)
        {
            var userManager = new AppUserManager(new AppUserStore(new DatabaseContext()));

            var wantedUser = userManager.FindByEmail(email);

            if (wantedUser == null)
            {
                return null;
            }

            var user = userManager.Find(wantedUser.UserName, password);

            if (user == null)
            {
                return null;
            }

            return userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
        }
        
        

        public void DeleteUser(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var user = context.Users.Find(id);
                context.Users.Remove(user);
                context.SaveChanges();
            };
        }

        public void UpdateUser(UserDTO user)
        {
            var newUser = Mapping.Mapper.Map<AppUser>(user);

            using (var context = new DatabaseContext())
            {
                context.Entry(newUser).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            using (var context = new DatabaseContext())
            {
                var users = context.Users.ToList();
                return users
                    .Select(element => Mapping.Mapper.Map<UserDTO>(element))
                    .ToList();
            }
        }

        public UserDTO GetUserById(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var user = context.Users.Find(id);
                return Mapping.Mapper.Map<UserDTO>(user);
            }
        }
    }
}
