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

namespace BL.Facades
{
    public class CommentFacade
    {

        public void CreateComment(CommentDTO comment)
        {
            var newComment = Mapping.Mapper.Map<Comment>(comment);

            using (var context = new DatabaseContext())
            {
                newComment.PostedDate=DateTime.Now;
                context.Database.Log = Console.WriteLine;
                context.Comments.Add(newComment);
                context.SaveChanges();
            }
        }

        public void CreateComment(CommentDTO comment, int id)
        {
            var newComment = Mapping.Mapper.Map<Comment>(comment);
            bool found = false;
            DAL.Entities.Issue issue;
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var oldIssue = context.Issues.Find(id);
                if (oldIssue != null)
                {
                    issue = oldIssue;
                    found = true;
                }
                else
                {
                    issue = new DAL.Entities.Issue { Id = id };
                }
            }
            //project.Issues.Add(newIssue);
            newComment.Issue = issue;
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                if (found)
                {
                    context.Entry(issue).State = EntityState.Unchanged;
                }
                newComment.PostedDate = DateTime.Now;
                context.Comments.Add(newComment);
                context.SaveChanges();
            }
        }


        public void DeleteComment(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var comment = context.Comments.Find(id);
                context.Comments.Remove(comment);
                context.SaveChanges();
            };
        }


        public void UpdateComment(CommentDTO comment)
        {
            var newComment = Mapping.Mapper.Map<Comment>(comment);

            using (var context = new DatabaseContext())
            {
                context.Entry(newComment).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public CommentDTO GetCommentById(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.Log = Console.WriteLine;
                var comment = context.Comments.Find(id);
                return Mapping.Mapper.Map<CommentDTO>(comment);
            }
        }


        public List<CommentDTO> GetAllComments()
        {
            using (var context = new DatabaseContext())
            {
                var comment = context.Comments.ToList();
                return comment
                    .Select(element => Mapping.Mapper.Map<CommentDTO>(element))
                    .ToList();
            }
        }

        public IssueDTO GetCommentsIssue(int id)
        {
            using (var context = new DatabaseContext())
            {
                var issue = new IssueDTO();
                context.Database.Log = Console.WriteLine;
                var issues = context.Issues;
                foreach (var var in issues)
                {
                    var commentsOfIssue = var.Comments;
                    foreach (var temp in commentsOfIssue)
                    {
                        if (temp.Id == id)
                        {
                            return Mapping.Mapper.Map<IssueDTO>(var);
                        }
                    }

                }

                return Mapping.Mapper.Map<IssueDTO>(issue);
            }
        }

    }
}
