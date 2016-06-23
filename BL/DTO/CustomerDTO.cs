using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{

    public class CustomerDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is in wrong format")]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Address { get; set; }

        public ICollection<ProjectDTO> Projects { get; set; }
        public CustomerDTO()
        {
            Projects = new HashSet<ProjectDTO>();
        }


    }
}