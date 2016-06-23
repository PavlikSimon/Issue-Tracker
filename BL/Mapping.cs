using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using DAL.Entities;
using DAL.IdentityEntities;

namespace BL
{
    public static class Mapping
    {
        public static IMapper Mapper { get; }
        static Mapping()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Customer, CustomerDTO>().ReverseMap();
                c.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                c.CreateMap<Comment, CommentDTO>().ReverseMap();
                c.CreateMap<ProjectDTO, Project>().ReverseMap();
                c.CreateMap<IssueDTO, Issue>().ReverseMap();
                c.CreateMap<UserDTO, AppUser>().ReverseMap();
            });
            Mapper = config.CreateMapper();
        }
    }
}
