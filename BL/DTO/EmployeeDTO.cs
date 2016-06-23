using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BL.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<IssueDTO> Issues { get; set; }
        public EmployeeDTO()
        {
            Issues = new HashSet<IssueDTO>();
        }
    }
}
