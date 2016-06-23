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
    public class EmployeesController : Controller
    {
        readonly EmployeeFacade employeeFacade = new EmployeeFacade();

        [Authorize]
        // GET: Issues
        public ActionResult Index()
        {
            var model = employeeFacade.GetAllEmployees();
            return View(model);
        }
        [Authorize(Roles = "Admin, Employee")]
        // GET: Issues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(EmployeeDTO employee)
        {
            if (ModelState.IsValid)
            {
                employeeFacade.CreateEmployee(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }
        [Authorize(Roles = "Admin, Employee")]
        // GET: Issues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = employeeFacade.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, EmployeeDTO employee)
        {
            if (ModelState.IsValid)
            {
                var originalEmployee = employeeFacade.GetEmployeeById(id);
                originalEmployee.Name = employee.Name;

                employeeFacade.UpdateEmployee(originalEmployee);

                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [Authorize(Roles = "Admin, Employee")]
        // GET: Issues/Delete/5
        public ActionResult Delete(int id)
        {
            employeeFacade.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        readonly IssueFacade issueFacade = new IssueFacade();
        
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult AddToIssue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = employeeFacade.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var issues = issueFacade.GetAllIssues();
            var freeIssues = new HashSet<IssueDTO>();
            foreach (var var in issues)
            {
                if (var.Employee != null)
                    freeIssues.Add(var);
            }
            
            var model = new EmployeeModel()
            {
                employeeId = id.Value,
                issues = issues
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult AddToIssue2(int? employeeId, int? issueId)
        {
            if (employeeId == null || issueId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = employeeFacade.GetEmployeeById(employeeId.Value);
            var issue = issueFacade.GetIssueById(issueId.Value);

            employee.Issues.Add(issue);
            employeeFacade.UpdateEmployee(employee);
            issue.Employee = employee;
            issueFacade.UpdateIssue(issue);
            return RedirectToAction("Index");
        }

    }
}
