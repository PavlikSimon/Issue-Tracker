using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTO;

namespace Web.Models
{
    public class IssueModel
    {
        public int issueId { get; set; }
        public ICollection<CommentDTO> issueComments { get; set; }
        public IssueModel()
        {
            issueComments = new HashSet<CommentDTO>();
        }

        public CommentDTO issueComment { get; set; }
    }
}