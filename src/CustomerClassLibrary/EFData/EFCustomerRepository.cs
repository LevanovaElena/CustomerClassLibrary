using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.EFData
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private CustomerLibraryContext _context;
        public int CustomerId { get; set ; }

        public EFCustomerRepository()
        {
            _context = new CustomerLibraryContext();

        }

        public int Create(Customer customer)
        {
            _context.Customers.Add(customer);
            Console.WriteLine(_context.Database.Connection.Database.ToString());
            //Console.WriteLine(_context.Database.Log..ToString());
            _context.Database.Log = s => Console.WriteLine(s);
            try
            {

                _context.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Object: " + validationError.Entry.Entity.ToString());
                    Console.WriteLine("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        Console.WriteLine(err.ErrorMessage + "");
                        }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customer.IdCustomer;
        }


        public int CountOfCustomers()
        {
            throw new NotImplementedException();
        }



        public void Delete(int idCustomer)
        {
            throw new NotImplementedException();
        }

        public Customer Read(int idCustomer)
        {
            throw new NotImplementedException();
        }

        public List<Customer> ReadAll()
        {
            throw new NotImplementedException();
        }

        public List<Customer> ReadCustomerFromNumber(int numberOfRow, int numberFromRow)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
