using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MyEnumerations
{
    /// <summary>
    /// Type of every issue, is required
    /// </summary>
    public enum TypeOfIssue : int
        {
            error = 1,
            request = 2
        }

    /// <summary>
    /// Status of Issue, is required, after new issue is created, default state is 1
    /// </summary>
    public enum IssueStatus : int
        {
            newIssue = 1,
            inProgress = 2,
            solved = 3,
            declined = 4
        }
   
}
