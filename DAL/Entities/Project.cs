using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Entities
{
    public class Project
    {

        /// <summary>
        /// unique ID of the project
        /// KEY
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// name of the product
        /// max 30 characters
        /// </summary>
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// every project have its owner
        /// is required
        /// </summary>
        [Required]
        public virtual Customer Customer { get; set; }
        /// <summary>
        /// one project can have many issues-errors or requests
        /// </summary>
        public virtual ICollection<Issue> Issues { get; set; }
        public Project()
        {
            this.Issues = new HashSet<Issue>();
        }
    }
}
