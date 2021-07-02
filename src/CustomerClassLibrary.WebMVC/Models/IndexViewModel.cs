using CustomerClassLibrary.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerClassLibrary.WebMVC.Models
{
    public class IndexViewModel
    {
        public List<Customer> Customers { get; set; }
        public PaginationPageInfo paginationPage { get; set; }
    }
}