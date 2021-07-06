using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerClassLibrary;

namespace CustomerClassLibrary.EFData
{
    public class CustomerLibraryContext:DbContext
    {
        public CustomerLibraryContext():base ("DbConnection")
        {

        }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Address> Addresses { get; set; }
    }
}
