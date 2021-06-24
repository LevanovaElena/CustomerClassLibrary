using CustomerClassLibrary;
using CustomerClassLibrary.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerClassLibrary.WebForm
{
    public partial class _Default : Page
    {
        private readonly ICustomerService _customerService;
        public List<Customer> CustomerList { get; set; }
         Customer ActiveCustomer { get; set; }
        int ActiveNumberCustomer { get; set; } = 0;
        public _Default()
        {
            _customerService = new CustomerService();
            CustomerList = _customerService.GetAllCustomers();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            
        }


        public void OnSaveClick(object sender, EventArgs e)
        {
            //save in db

            
        }
        public void OnDeleteClick(object sender, CommandEventArgs e)
        {
            int idDelete= Int32.Parse(e.CommandArgument.ToString());
            _customerService.DeleteCustomer(idDelete);
            this.CustomerList = _customerService.GetAllCustomers();
            this.OnLoad(new EventArgs());

        }
        public void OnPrevClick(object sender, CommandEventArgs e)
        {
            ActiveNumberCustomer = Int32.Parse(e.CommandArgument.ToString());
            if (ActiveNumberCustomer == 0) ActiveNumberCustomer = CustomerList.Count - 1;
            else ActiveNumberCustomer--;
            ActiveCustomer = CustomerList[ActiveNumberCustomer];
            btnPrev.CommandArgument = ActiveNumberCustomer.ToString();
            btnNext.CommandArgument = ActiveNumberCustomer.ToString();
            
        }
        public void OnNextClick(object sender, CommandEventArgs e)
        {
            ActiveNumberCustomer =Int32.Parse(e.CommandArgument.ToString());

            if (ActiveNumberCustomer == CustomerList.Count - 1) ActiveNumberCustomer = 0;
            else ActiveNumberCustomer++;
            ActiveCustomer = CustomerList[ActiveNumberCustomer];
            btnNext.CommandArgument = ActiveNumberCustomer.ToString();
            btnPrev.CommandArgument = ActiveNumberCustomer.ToString();
            
        }
    }
}