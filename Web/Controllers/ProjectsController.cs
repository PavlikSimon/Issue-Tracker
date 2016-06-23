using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;
using DAL.MyEnumerations;

namespace Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        readonly ProjectFacade projectFacade = new ProjectFacade();

        [Authorize]
        public ActionResult Index()
        {
            var model = projectFacade.GetAllProjects();
            return View(model);
        }

        // GET: Projects/Details/5
        [Authorize]
        public ActionResult Details(int id)
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

        [Authorize(Roles = "Admin, Employee")]
        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, ProjectDTO project)
        {
            var originalProject = projectFacade.GetProjectById(id);
            originalProject.Name = project.Name;
            originalProject.Description = project.Description;

            if (!ModelState.IsValid && originalProject.Customer != null)
            {
                if (ModelState.ContainsKey("Customer"))
                    ModelState["Customer"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                projectFacade.UpdateProject(originalProject);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
        projectFacade.DeleteProject(id);
            return RedirectToAction("Index");
        }

    }
}
