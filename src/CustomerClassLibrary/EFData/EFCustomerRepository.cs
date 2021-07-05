using CustomerClassLibrary.Common;
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

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM dbo.customers ; DBCC CHECKIDENT (customers, RESEED, 0)");

            _context.SaveChanges();
        }

        public int CountOfCustomers()
        {
            return _context.Customers.Count();
        }



        public void Delete(Customer customer)
        {
            try
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            catch
            {
                throw new NotFoundCustomerWithId("Not found customer for delete with id="+customer.IdCustomer);
            }
            
        }
        public void Delete(int idCustomer)
        {
            Customer customer = Read(idCustomer);
           if(customer==null)  throw new NotFoundCustomerWithId("Not found customer with id= "+idCustomer);
           else this.Delete(customer);

        }

        public Customer Read(int idCustomer)
        {
            var customer= _context.Customers.Find(idCustomer);
            return customer;
        }

        public List<Customer> ReadAll()
        {
            var list= _context.Customers.ToList();
            return list;
        }

        public List<Customer> ReadCustomerFromNumber(int numberOfRow, int numberFromRow)
        {
            List<Customer> result = _context.Customers.OrderBy(p=>p.IdCustomer).Skip(numberFromRow).Take(numberOfRow).ToList();
            return result;
        }

        public void Update(Customer customerNew)
        {
            Customer customerOld = _context.Customers
                .Include("AddressesList")
                .Where(c => c.IdCustomer == customerNew.IdCustomer)
                .FirstOrDefault();
            if (customerOld != null)
            {
                customerOld.FirstName = customerNew.FirstName;
                customerOld.LastName = customerNew.LastName;
                customerOld.Notes = customerNew.Notes;
                customerOld.Email = customerNew.Email;
                customerOld.PhoneNumber = customerNew.PhoneNumber;
                customerOld.IdCustomer = customerNew.IdCustomer;
            }
            
            _context.SaveChanges();
        }

        
    }
}
