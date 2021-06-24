using System.Collections.Generic;

namespace CustomerClassLibrary.Repositories
{
    public interface ICustomerRepository
    {
        public int CustomerId { get; set; }
        public int Create(Customer customer);
        public Customer Read(int idCustomer);

        public void Delete(int idCustomer);

        public void Update(Customer customer);
        public List<Customer> ReadAll();
    }
}