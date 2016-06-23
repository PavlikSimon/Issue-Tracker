using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTO;

namespace Web.Models
{
    public class ShowIssueAttributesModel
    {
        public int errorNewIssue { get; set; }
        public int errorInProgress { get; set; }
        public int errorSolved { get; set; }
        public int errorDeclined { get; set; }
        public int otherNewIssue { get; set; }
        public int otherInProgress { get; set; }
        public int otherSolved { get; set; }
        public int otherDeclined { get; set; }

        public ProjectDTO project { get; set; }
    }
}