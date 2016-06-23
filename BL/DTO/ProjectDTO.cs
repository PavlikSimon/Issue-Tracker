using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public CustomerDTO Customer { get; set; }

        public string Description { get; set; }

        public ICollection<IssueDTO> Issues { get; set; }
        public ProjectDTO()
        {
            Issues = new HashSet<IssueDTO>();
        }


    }
}