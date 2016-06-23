using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTO;

namespace Web.Models
{
    public class ProjectModel
    {
        public int projectId { get; set; }
        public ICollection<IssueDTO> projectIssues { get; set; }
        public ProjectModel()
        {
            projectIssues = new HashSet<IssueDTO>();
        }

        public IssueDTO projectIssue { get; set; }
    }
}