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

        const int NumberCustomersOnPage= 5;
        int ActivePagePagination { get; set; } = 1;
        public int CountOfPaginationButton { get; set; } = 1;
        public _Default()
        {
            _customerService = new CustomerService();
            

        }

        protected override void OnPreLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string idDelete = Request["idCustomerDelete"];
                int idCustomer;
                if (!String.IsNullOrEmpty(idDelete) && int.TryParse(idDelete, out idCustomer))
                {
                    _customerService.DeleteCustomer(idCustomer);
                }
                
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

                int countCustomer= _customerService.GetCountCustomer();
                int result;
            CountOfPaginationButton = Math.DivRem(countCustomer, NumberCustomersOnPage, out result);
            CountOfPaginationButton = result > 0 ? CountOfPaginationButton++ : CountOfPaginationButton;

            string paginationPage = Request["pagePagination"];
            int pagePagination;
            if (!String.IsNullOrEmpty(paginationPage) && int.TryParse(paginationPage, out pagePagination))
            {
                CustomerList = _customerService.GetAllCustomersFromNumber(NumberCustomersOnPage, pagePagination * NumberCustomersOnPage);
                ActivePagePagination = pagePagination;
            }
            else
            {
                CustomerList = _customerService.GetAllCustomersFromNumber(NumberCustomersOnPage, ActivePagePagination * NumberCustomersOnPage);
            }


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
            ActivePagePagination -= 1;
            ActivePagePagination = ActivePagePagination <=0 ? CountOfPaginationButton : ActivePagePagination;
            Response.Redirect("Default?pagePagination=" + ActivePagePagination);

        }
        public void OnNextClick(object sender, CommandEventArgs e)
        {
            ActivePagePagination += 1;
            ActivePagePagination = ActivePagePagination > CountOfPaginationButton ? 1 : ActivePagePagination;
            Response.Redirect("Default?pagePagination=" + ActivePagePagination);

        }
    }
}