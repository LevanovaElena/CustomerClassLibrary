using CustomerClassLibrary.Business;
using CustomerClassLibrary.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CustomerClassLibrary.WebMVC.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;
        
        public PaginationPageInfo PaginationPageInfo { get; set; }
        public List<Customer> CustomersPerPage { get; set; }

        public CustomerController()
        {
            _customerService = new EFCustomerService();
        }
        // GET: Customer
        public ActionResult Index(int page=1)
        {
            int pageSize = 5;
            int countOfAllCustomers = _customerService.GetCountCustomer();
            PaginationPageInfo =new PaginationPageInfo { PageNumber = page, PageSize = pageSize, TotalItems = countOfAllCustomers };
            int rowNotView =  PaginationPageInfo.PageSize * (PaginationPageInfo.PageNumber-1);
            CustomersPerPage = _customerService.GetAllCustomersFromNumber(rowNotView, PaginationPageInfo.PageSize);
            IndexViewModel indexView = new IndexViewModel { Customers = CustomersPerPage, paginationPage = PaginationPageInfo };



            return View(indexView);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            Customer customer = new Customer();
            customer.Notes.Add("");
            customer.AddressesList.Add(new Address());
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                _customerService.Create(customer);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
        }


        public ActionResult AddressesListEdit(Customer customer)
       {

            customer.AddressesList.Add(new Address(customer.IdCustomer));
            customer.AddressesList = ViewBag.AddressesList;
            return View(customer);
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
        public ActionResult Edit(Customer customer,int id)
        {
            try
            {
                if (customer.IdCustomer == 0) customer = _customerService.GetCustomer(id);
                customer = _customerService.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
        }


        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult AddAddress(Customer customer, int id, string viewName)
        {
            customer.AddressesList.Add(new Address(customer.IdCustomer));
            return View(viewName, customer);
        }
        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult DeleteAddress(Customer customer, int id, string viewName, int idDeleteAddress)
        {
            if (customer.AddressesList.Count > 1)
            {
                if (idDeleteAddress > 0)
                {
                    _customerService.DeleteAddress(idDeleteAddress);
                    customer.AddressesList = _customerService.GetCustomer(id).AddressesList;
                    return View(viewName, customer);
                }
            }
            else ViewBag.ErrorMessage = "Customer must have one address!";
            return View(viewName, customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult AddNote(Customer customer, int id,string viewName)
        {
            customer.Notes.Add("");
            return View(viewName, customer);
        }
        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult DeleteNote(Customer customer, int id, string viewName, int deleteNote)
        {
            if (deleteNote >= 0 && customer.Notes.Count > 1)
            {
                customer.Notes.RemoveAt(deleteNote);
            }
            else ViewBag.ErrorMessage = "Customer must have one note!";
            return View(viewName, customer);
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
