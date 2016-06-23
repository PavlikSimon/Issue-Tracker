using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        
        [Required]
        public System.DateTime PostedDate { get; set; }

        [MaxLength(500)]
        public string Text { get; set; }

        [Required]
        public IssueDTO Issue { get; set; }

        [Required]
        public string CommentatorRole { get; set; }

        [Required]
        public int CommentatorId { get; set; }

        [Required]
        public string CommentatorName { get; set; }
    }
}
