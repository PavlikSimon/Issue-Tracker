using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.MyEnumerations;



namespace BL.DTO
{
    public class IssueDTO
    {
        public int Id { get; set; }
        
        [Required]
        public System.DateTime CreatedDate { get; set; }
        
        public Nullable<DateTime> SolvedDate { get; set; }

        [Required]
        [RegularExpression(@"1|2|error|request", ErrorMessage = "Typo of Issue can only be error = 1,request = 2")]
        public TypeOfIssue TypeOfIssue { get; set; }
        
        [Required]
        [RegularExpression(@"1|2|3|4|newissue|inprogress|inProgress|newIssue|solved|declined", ErrorMessage = "Issue status can only be newIssue = 1, inProgress = 2, solved = 3, declined = 4")]
        public IssueStatus IssueStatus { get; set; }


        [MaxLength(30)]
        public string Requestor { get; set; }

   
        [Required]
        [MaxLength(60)]
        public string ShortMessage { get; set; }
        
        [MaxLength(500)]
        public string Text { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
        public IssueDTO()
        {
            Comments = new HashSet<CommentDTO>();
        }

        public EmployeeDTO Employee { get; set; }


        [Required]
        public ProjectDTO Project { get; set; }
        


    }
}
