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
using DAL.MyEnumerations;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class IssuesController : Controller
    {
        readonly IssueFacade issueFacade = new IssueFacade();

        [Authorize]
        // GET: Issues
        public ActionResult Index()
        {
            var model = issueFacade.GetAllIssues();
            return View(model);
        }
        [Authorize]
        // GET: Issues/Details/5
        public ActionResult Details(int id)
        {
            var issue = issueFacade.GetIssueById(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Issues/Edit/5
        public ActionResult Edit(int? id)
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
            var i = issue.CreatedDate;
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, IssueDTO issueDTO)
        {
            var originalIssue = issueFacade.GetIssueById(id);
            originalIssue.ShortMessage = issueDTO.ShortMessage;
            originalIssue.Text = issueDTO.Text;
            originalIssue.Requestor = issueDTO.Requestor;
            originalIssue.IssueStatus = issueDTO.IssueStatus;
            if ((issueDTO.IssueStatus == (IssueStatus.solved)) && originalIssue.SolvedDate == null)
            {
                originalIssue.SolvedDate = DateTime.Now;
            }

            if (!ModelState.IsValid && originalIssue.Project !=null)
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
                return RedirectToAction("Index");
            }
            //var i = originalIssue.CreatedDate;
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(issueDTO);
        }
        [Authorize(Roles = "Admin")]
        // GET: Issues/Delete/5
        public ActionResult Delete(int id)
        {
            issueFacade.DeleteIssue(id);
            return RedirectToAction("Index");
        }


    }
}
