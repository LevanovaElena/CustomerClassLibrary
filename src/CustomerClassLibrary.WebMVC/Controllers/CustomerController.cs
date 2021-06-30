using CustomerClassLibrary.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerClassLibrary.WebMVC.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;

        public CustomerController()
        {
            _customerService = new CustomerService();
        }
        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>();
            customers = _customerService.GetAllCustomers();
            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                _customerService.Create(customer);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            try 
            {
                Customer customer = _customerService.GetCustomer(id);
                return View(customer);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
        }

   

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Customer customer = _customerService.GetCustomer(id);
                return View(customer);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _customerService.DeleteCustomer(id);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
        }
    }
}
