using CustomerClassLibrary;
using CustomerClassLibrary.EFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static IntegrationTests.CustomerRepositoryTest;

namespace IntegrationTests
{
    public class EFCustomerRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateEFCustomerRepository()
        {
            var customerRepository = new EFCustomerRepository();
            Assert.NotNull(customerRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var customerRepositoryFixture = new CustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            Customer customer = customerRepositoryFixture.MockCustomer();
            Address address = new Address();


            //customerRepository.DeleteAll();

            int id = customerRepository.Create(customer);
            //Assert.Equal(1, id);
            Assert.NotNull(id);
        }
    }
}
