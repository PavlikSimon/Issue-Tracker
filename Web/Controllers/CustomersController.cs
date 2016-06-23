using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        readonly CustomerFacade customerFacade = new CustomerFacade();

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
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Delete(int id)
        {
            customerFacade.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

    }
}