using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;
using DAL.MyEnumerations;
using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin, Customer, Employee")]
    public class InformationController : Controller
    {
        readonly CustomerFacade customerFacade = new CustomerFacade();
        readonly ProjectFacade projectFacade = new ProjectFacade();
        readonly CommentFacade commentFacade = new CommentFacade();
        readonly IssueFacade issueFacade = new IssueFacade();

        [Authorize]
        public ActionResult Index()
        {
            var model = customerFacade.GetAllCustomers();
            return View(model);
        }

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                customerFacade.CreateCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);

        }

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Delete(int id)
        {
            customerFacade.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = customerFacade.GetCustomerById(id.Value);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(int id, CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                var originalCustomer = customerFacade.GetCustomerById(id);
                originalCustomer.Email = customer.Email;
                originalCustomer.Name = customer.Name;
                originalCustomer.Address = customer.Address;

                customerFacade.UpdateCustomer(originalCustomer);

                return RedirectToAction("Index");
            }
            return View(customer);
        }


        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult Projects(int id)
        {
            var customer = customerFacade.GetCustomerById(id);
            var model = new InformationModel {customerId = id, InfoProjects = customer.Projects};
            return View(model);
        }

        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult Issues(int id)
        {
            var project = projectFacade.GetProjectById(id);
            var model = new ProjectModel {projectId = id, projectIssues = project.Issues};
            return View(model);
        }


        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult Comments(int id)
        {
            var issue = issueFacade.GetIssueById(id);
            var model = new IssueModel() {issueId = id, issueComments = issue.Comments};
            return View(model);
        }

        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult ProjectsCreate(int id)
        {
            var customer = customerFacade.GetCustomerById(id);
            var model = new InformationModel
            {
                customerId = id,
                InfoProjects = customer.Projects,
                InfoProject = new ProjectDTO()
            };

            return View(model);
        }


        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult IssuesCreate(int id)
        {
            var project = projectFacade.GetProjectById(id);
            var model = new ProjectModel()
            {
                projectId = id,
                projectIssues = project.Issues,
                projectIssue = new IssueDTO()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult CommentsCreate(int id)
        {
            var issue = issueFacade.GetIssueById(id);
            var model = new IssueModel()
            {
                issueId = id,
                issueComments = issue.Comments,
                issueComment = new CommentDTO()
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult ProjectsCreate(InformationModel info)
        {
            var originalCustomer = customerFacade.GetCustomerById(info.customerId);
            info.InfoProject.Customer = originalCustomer;
            info.InfoProjects = info.InfoProject.Customer.Projects;
            if (!ModelState.IsValid && info.InfoProject.Customer != null)
            {

                if (ModelState.ContainsKey("InfoProject.Customer"))
                    ModelState["InfoProject.Customer"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                projectFacade.CreateProject(info.InfoProject, info.customerId);
                return RedirectToAction("Projects", new {id = info.customerId});
            }


            return View(info);
        }

        [HttpPost]
        public ActionResult IssuesCreate(ProjectModel info)
        {
            var originalProject = projectFacade.GetProjectById(info.projectId);
            info.projectIssue.Project = originalProject;
            info.projectIssues = info.projectIssue.Project.Issues;

            if (!ModelState.IsValid && info.projectIssue.Project != null)
            {
                if (ModelState.ContainsKey("projectIssue.Project"))
                    ModelState["projectIssue.Project"].Errors.Clear();
            }
            if (ModelState.ContainsKey("projectIssue.IssueStatus"))
                ModelState["projectIssue.IssueStatus"].Errors.Clear();

            //var temp = ModelState.Values;
            //var tmp = ModelState.Keys;
            if (ModelState.IsValid)
            {
                issueFacade.CreateIssue(info.projectIssue, info.projectId);
                return RedirectToAction("Issues", new {id = info.projectId});
            }
            return View(info);
        }

        [HttpPost]
        public ActionResult CommentsCreate(IssueModel info)
        {
            var originalIssue = issueFacade.GetIssueById(info.issueId);
            info.issueComment.Issue = originalIssue;
            info.issueComments = info.issueComment.Issue.Comments;
            info.issueComment.CommentatorId = int.Parse(User.Identity.GetUserId());
            info.issueComment.CommentatorName = User.Identity.GetUserName();
            if (User.IsInRole("Admin"))
                info.issueComment.CommentatorRole = "Admin";
            if (User.IsInRole("Customer"))
                info.issueComment.CommentatorRole = "Customer";
            if (User.IsInRole("Employee"))
                info.issueComment.CommentatorRole = "Employee";

            if (!ModelState.IsValid && info.issueComment.Issue != null)
            {
                if (ModelState.ContainsKey("issueComment.Issue"))
                    ModelState["issueComment.Issue"].Errors.Clear();
            }
            if (!ModelState.IsValid && info.issueComment.CommentatorName != null)
            {
                if (ModelState.ContainsKey("issueComment.CommentatorName"))
                    ModelState["issueComment.CommentatorName"].Errors.Clear();
            }
            if (!ModelState.IsValid && info.issueComment.CommentatorId != null)
            {
                if (ModelState.ContainsKey("issueComment.CommentatorId"))
                    ModelState["issueComment.CommentatorId"].Errors.Clear();
            }
            if (!ModelState.IsValid && info.issueComment.CommentatorRole != null)
            {
                if (ModelState.ContainsKey("issueComment.CommentatorRole"))
                    ModelState["issueComment.CommentatorRole"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                commentFacade.CreateComment(info.issueComment, info.issueId);
                return RedirectToAction("Comments", new {id = info.issueId});
            }
            return View(info);
        }


        [Authorize(Roles = "Admin, Employee")]
        public ActionResult ProjectsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = projectFacade.GetProjectById(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult IssuesEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var issue = issueFacade.GetIssueById(id.Value);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CommentsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = commentFacade.GetCommentById(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }


        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult ProjectsEdit(int id, ProjectDTO project)
        {
            var originalProject = projectFacade.GetProjectById(id);
            originalProject.Name = project.Name;
            originalProject.Description = project.Description;
            int backId = originalProject.Customer.Id;
            if (!ModelState.IsValid && originalProject.Customer != null)
            {
                if (ModelState.ContainsKey("Customer"))
                    ModelState["Customer"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                projectFacade.UpdateProject(originalProject);
                return RedirectToAction("Projects", new {id = backId});
            }
            return View(project);
        }

        [HttpPost]
        public ActionResult IssuesEdit(int id, IssueDTO issue)
        {
            var originalIssue = issueFacade.GetIssueById(id);
            originalIssue.ShortMessage = issue.ShortMessage;
            originalIssue.Text = issue.Text;
            originalIssue.Requestor = issue.Requestor;
            originalIssue.IssueStatus = issue.IssueStatus;
            if ((issue.IssueStatus == (IssueStatus.solved)) && originalIssue.SolvedDate == null)
            {
                originalIssue.SolvedDate = DateTime.Now;
            }
            //var allErrorsz = ModelState.Values;
            //var allErrors = ModelState.Keys;
            int backId = originalIssue.Project.Id;
            if (!ModelState.IsValid && originalIssue.Project != null)
            {
                if (ModelState.ContainsKey("Project"))
                    ModelState["Project"].Errors.Clear();
            }
            if (!ModelState.IsValid && originalIssue.TypeOfIssue != null)
            {
                if (ModelState.ContainsKey("TypeOfIssue"))
                    ModelState["TypeOfIssue"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                issueFacade.UpdateIssue(originalIssue);
                return RedirectToAction("Issues", new {id = backId});
            }
            return View(issue);
        }

        [HttpPost]
        public ActionResult CommentsEdit(int id, CommentDTO comment)
        {
            var originalComment = commentFacade.GetCommentById(id);
            originalComment.Text = comment.Text;

            int backId = originalComment.Issue.Id;
            if (!ModelState.IsValid && originalComment.Issue != null)
            {
                if (ModelState.ContainsKey("Issue"))
                    ModelState["Issue"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                commentFacade.UpdateComment(originalComment);
                return RedirectToAction("Comments", new {id = backId});
            }
            return View(comment);
        }

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult ProjectDelete(int id)
        {
            var customer = projectFacade.GetProjectsCustomer(id);
            projectFacade.DeleteProject(id);
            return RedirectToAction("Projects", new {id = customer.Id});
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IssuesDelete(int id)
        {
            var project = issueFacade.GetIssuesProject(id);
            issueFacade.DeleteIssue(id);
            return RedirectToAction("Issues", new {id = project.Id});
        }

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult CommentsDelete(int id)
        {
            var issue = commentFacade.GetCommentsIssue(id);
            commentFacade.DeleteComment(id);
            return RedirectToAction("Comments", new {id = issue.Id});
        }


        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult ProjectsDetails(int id)
        {
            var project = projectFacade.GetProjectById(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var issues = project.Issues;
            int newIssues = 0;
            int inProgress = 0;
            int solved = 0;
            int declined = 0;
            int errornewIssues = 0;
            int errorinProgress = 0;
            int errorsolved = 0;
            int errordeclined = 0;
            foreach (var issue in issues)
            {
                if (issue.TypeOfIssue == TypeOfIssue.request)
                {
                    if (issue.IssueStatus == IssueStatus.declined)
                    {
                        declined++;
                    }
                    if (issue.IssueStatus == IssueStatus.inProgress)
                    {
                        inProgress++;
                    }
                    if (issue.IssueStatus == IssueStatus.solved)
                    {
                        solved++;
                    }
                    if (issue.IssueStatus == IssueStatus.newIssue)
                    {
                        newIssues++;
                    }
                }
                if (issue.TypeOfIssue == TypeOfIssue.error)
                {
                    if (issue.IssueStatus == IssueStatus.declined)
                    {
                        errordeclined++;
                    }
                    if (issue.IssueStatus == IssueStatus.inProgress)
                    {
                        errorinProgress++;
                    }
                    if (issue.IssueStatus == IssueStatus.solved)
                    {
                        errorsolved++;
                    }
                    if (issue.IssueStatus == IssueStatus.newIssue)
                    {
                        errornewIssues++;
                    }
                }
            }
            var model = new ShowIssueAttributesModel()
            {
                project = project,
                errorDeclined = errordeclined,
                errorInProgress = errorinProgress,
                errorNewIssue = errornewIssues,
                errorSolved = errorsolved,
                otherDeclined = declined,
                otherInProgress = inProgress,
                otherNewIssue = newIssues,
                otherSolved = solved
            };
            return View(model);
        }



        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult IssuesDetails(int id)
        {
            var issue = issueFacade.GetIssueById(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        public ActionResult RedirectToProjectsFromIssues(int id)
        {
            var customer = projectFacade.GetProjectsCustomer(id);
            //All we want to do is redirect to the class selection page
            return RedirectToAction("Projects", new {customer.Id});
        }

        public ActionResult RedirectToIssuesFromComments(int id)
        {
            var issue = issueFacade.GetIssueById(id);
            //All we want to do is redirect to the class selection page
            return RedirectToAction("Issues", new {issue.Project.Id});
        }
    }
}