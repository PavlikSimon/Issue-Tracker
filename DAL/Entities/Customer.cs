using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Customer
    {
        /// <summary>
        /// Unique ID of Customer
        /// KEY
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the customer
        /// Is Required
        /// Max 30 characters
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        /// <summary>
        /// Email of the customer
        /// Is Required
        /// Max characters
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        /// <summary>
        /// Address of customer
        /// Max 30 characters
        /// </summary>
        [MaxLength(30)]
        public string Address { get; set; }
        /// <summary>
        /// CUstomer can have multiple projects
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }
        public Customer()
        {
            this.Projects = new HashSet<Project>();
        }
    }
}
