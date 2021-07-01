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
            Customer customer = new Customer();
            customer.AddressesList.Add(new Address());
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer,string action)
        {
            try
            {
                if (action == "AddAddress")
                {
                    customer.AddressesList.Add(new Address());
                    return View(customer);
                }
                // TODO: Add insert logic here
                else
                {
                    _customerService.Create(customer);

                    return RedirectToAction("Index");
                }
                
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
        public ActionResult Edit(Customer customer, string action,string idDelete,int id)
        {
            try
            {
                if (customer.IdCustomer == 0) customer = _customerService.GetCustomer(id);
                if (action == "AddAddress")
                {

                    customer.AddressesList.Add(new Address(customer.IdCustomer));
                    return View(customer);
                }
                else if (action == "Save")
                {
                    _customerService.UpdateCustomer(customer);
                    return RedirectToAction("Index");
                }
               else
                {

                    int k = -1;
                    int idAddress = int.Parse(idDelete);
                    foreach (Address address in customer.AddressesList)
                    {
                        if (address.IdAddress == idAddress)
                        {
                            k = customer.AddressesList.FindIndex( address);
                            break;
                        }
                    }
                    customer.AddressesList.RemoveAt(k);
                    _customerService.UpdateAddressList(customer);
                }
                return View(customer);
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
