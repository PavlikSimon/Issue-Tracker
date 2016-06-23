using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Comment
    {
        /// <summary>
        /// Unique ID of Comment
        /// KEY
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date when the comment was posted
        /// is required
        /// </summary>

        [Required]
        public System.DateTime PostedDate { get; set; }
        //Text of the comment, Is Required, Maximum length of text is 500 characters

        /// <summary>
        /// Text of the comment
        /// is required
        /// max 500 characters
        /// </summary>

        [MaxLength(500)]
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Comment must be related to just one issue
        /// </summary>
        [Required]
        public virtual Issue Issue { get; set; }

        [Required]
        public string CommentatorRole { get; set; }

        [Required]
        public int CommentatorId { get; set; }

        [Required]
        public string CommentatorName { get; set; }
    }
}
