using DAL.MyEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DAL.Entities
{
    public class Issue
    {

        /// <summary>
        /// Unique ID of Issue
        /// KEY
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DateTime when the Issue was created
        /// is required
        /// </summary>
        [Required]
        public System.DateTime CreatedDate { get; set; }

        /// <summary>
        /// DateTime when the Issue was solved
        /// can be NULL - when Issue has not been handled
        /// </summary>
        public Nullable<System.DateTime> SolvedDate { get; set; }

        /// <summary>
        /// Enum Type of issue - error = 1, request = 2
        /// is required
        /// </summary>
        [Required]
        public TypeOfIssue TypeOfIssue { get; set; }
        /// <summary>
        /// Issue status, can be newIssue = 1, inProgress = 2, solved = 3, declined = 4
        /// is required
        /// </summary>
        [Required]
        public IssueStatus IssueStatus { get; set; }

        /// <summary>
        /// Name of the person who requested Issue to be handled
        /// max 30 chars
        /// </summary>
        [MaxLength(30)]
        public string Requestor { get; set; }

        /// <summary>
        /// ShortMessage of the problem
        /// is required
        /// max 60 chars
        /// </summary>
        [Required]
        [MaxLength(60)]
        public string ShortMessage { get; set; }
        /// <summary>
        /// text of the issue
        /// max 500 characters
        /// </summary>
        [MaxLength(500)]
        public string Text { get; set; }
        /// <summary>
        /// comment to issue, every issue can have multiple comments
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
        public Issue()
        {
            this.Comments = new HashSet<Comment>();
        }

        /// <summary>
        /// every issue have to be managed by one Employee
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// Every issue need to be connected to project
        /// </summary>
        [Required]
        public virtual Project Project { get; set; }
    }
}
