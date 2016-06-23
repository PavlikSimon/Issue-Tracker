using System.Collections.Generic;
using BL.DTO;

namespace Web.Models
{
    public class EmployeeModel
    {
        public int employeeId { get; set; }
        public ICollection<IssueDTO> issues { get; set; }
        public EmployeeModel()
        {
            issues = new HashSet<IssueDTO>();
        }
    }
}