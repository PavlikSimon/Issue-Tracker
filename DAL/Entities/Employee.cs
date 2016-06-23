using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee
    {
        /// <summary>
        /// ID of the employee, must be unique
        /// KEY
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of Employee
        /// is required
        /// max 30 characters
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        /// <summary>
        /// One employee can handle multiple issues (error/requirement)
        /// </summary>
        public virtual ICollection<Issue> Issues { get; set; }
        public Employee()
        {
            this.Issues = new HashSet<Issue>();
        }

    }
}
