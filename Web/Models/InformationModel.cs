using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTO;

namespace Web.Models
{
    public class InformationModel
    {
        
        public int customerId { get; set; }
        public ICollection<ProjectDTO> InfoProjects { get; set; }
        public InformationModel()
        {
            InfoProjects = new HashSet<ProjectDTO>();
        }

        public ProjectDTO InfoProject { get; set; }
    }
}