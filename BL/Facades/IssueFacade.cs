using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using DAL;
using DAL.Entities;
using DAL.MyEnumerations;

namespace BL.Facades
{
    public class IssueFacade
    {
        public void CreateIssue(IssueDTO issue)
        {
            var newIssue = Mapping.Mapper.Map<Issue>(issue);

            using (var context = new DatabaseContext())
            {
                newIssue.IssueStatus=IssueStatus.newIssue;
                context.Database.Log = Console.WriteLine;
                context.Issues.Add(newIssue);
                context.SaveChanges();
            }
        }

        public void CreateIssue(IssueDTO issue, int id)
        {
            var newIssue = Mapping.Mapper.Map<Issue>(issue);
            newIssue.IssueStatus = IssueStatus.newIssue;
            bool found = false;
            DAL.Entities.Project project;
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var oldProject = context.Projects.Find(id);
                if (oldProject != null)
                {
                    project = oldProject;
                    found = true;
                }
                else
                {
                    project = new DAL.Entities.Project { Id = id };
                }
            }
            //project.Issues.Add(newIssue);
            newIssue.Project = project;
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                if (found)
                {
                    context.Entry(project).State = EntityState.Unchanged;
                }
                newIssue.CreatedDate=DateTime.Now;
                context.Issues.Add(newIssue);
                context.SaveChanges();
            }
        }

        public void UpdateIssue(IssueDTO issue)
        {
            var newIssue = Mapping.Mapper.Map<Issue>(issue);

            using (var context = new DatabaseContext())
            {
                context.Entry(newIssue).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteIssue(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var issue = context.Issues.Find(id);
                context.Issues.Remove(issue);
                context.SaveChanges();
            };
        }

        public void DeleteIssue(IssueDTO issue)
        {
            var newIssue = Mapping.Mapper.Map<Issue>(issue);

            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Entry(newIssue).State = EntityState.Deleted;
                context.SaveChanges();
            };
        }


        public List<IssueDTO> GetAllIssues()
        {
            using (var context = new DatabaseContext())
            {
                var issues = context.Issues.ToList();
                return issues
                    .Select(element => Mapping.Mapper.Map<IssueDTO>(element))
                    .ToList();
            }
        }

        public IssueDTO GetIssueById(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var issue = context.Issues.Find(id);
                return Mapping.Mapper.Map<IssueDTO>(issue);
            }
        }

        public ProjectDTO GetIssuesProject(int id)
        {
            using (var context = new DatabaseContext())
            {
                var project = new ProjectDTO();
                context.Database.Log = Console.WriteLine;
                var projects = context.Projects;
                foreach (var var in projects)
                {
                    var issuesOfProject = var.Issues;
                    foreach (var temp in issuesOfProject)
                    {
                        if (temp.Id == id)
                        {
                            return Mapping.Mapper.Map<ProjectDTO>(var);
                        }
                    }

                }

                return Mapping.Mapper.Map<ProjectDTO>(project);
            }
        }




    }
}