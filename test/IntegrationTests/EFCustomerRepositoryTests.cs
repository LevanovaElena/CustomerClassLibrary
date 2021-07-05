using CustomerClassLibrary;
using CustomerClassLibrary.Common;
using CustomerClassLibrary.EFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


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
            var customerRepositoryFixture = new EFCustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            Customer customer = customerRepositoryFixture.MockCustomer();



            //customerRepository.DeleteAll();

            int id = customerRepository.Create(customer);
            Assert.NotEqual(0, id);
        }

        [Fact]
        public void ShouldDeleteAllCustomer()
        {
            var customerRepositoryFixture = new EFCustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            Customer customer = customerRepositoryFixture.MockCustomer();

            customerRepository.DeleteAll();

            int id = customerRepository.CountOfCustomers();
            Assert.Equal(0, id);
        }

        [Fact]
        public void ShouldBeAbleCountOfCustomers()
        {
            var customerRepositoryFixture = new EFCustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            customerRepositoryFixture.CreateMockCustomer();
            int id = customerRepository.CountOfCustomers();
            Assert.Equal(1, id);
        }

        [Fact]
        public void ShouldDeleteCustomer()
        {
            var customerRepositoryFixture = new EFCustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            Customer customer= customerRepositoryFixture.CreateMockCustomer();
            customerRepository.Delete(customer.IdCustomer);

            int id = customerRepository.CountOfCustomers();
            Assert.Equal(0, id);

            Assert.Throws<NotFoundCustomerWithId>(()=> customerRepository.Delete(2000));
        }

        [Fact]
        public void ShouldBeAbleToSelectCustomer()
        {
            var customerRepositoryFixture = new EFCustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            Customer customer = customerRepositoryFixture.CreateMockCustomer();

            var customerRead = customerRepository.Read(customer.IdCustomer);
            Assert.NotNull(customerRead);

            Assert.Null(customerRepository.Read(2000));
        }

        [Fact]
        public void ShouldBeAbleToSelectAllCustomer()
        {
            var customerRepositoryFixture = new EFCustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            customerRepositoryFixture.CreateMockCustomer();
            customerRepositoryFixture.CreateMockCustomer();

            var listCustomer = customerRepository.ReadAll();
            Assert.NotEmpty(listCustomer);

            customerRepository.DeleteAll();
            Assert.Empty(customerRepository.ReadAll());
        }

        [Fact]
        public void ShouldBeAbleToSelectCustomerFromNumber()
        {
            var customerRepositoryFixture = new EFCustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            Customer customer = customerRepositoryFixture.CreateMockCustomer();
            customerRepositoryFixture.CreateMockCustomerWithoutDelete();
            customerRepositoryFixture.CreateMockCustomerWithoutDelete();
            customerRepositoryFixture.CreateMockCustomerWithoutDelete();

            List<Customer> listCustomer = customerRepository.ReadCustomerFromNumber(2, 2);
            Assert.NotEmpty(listCustomer);
            Assert.Equal(2,listCustomer.Count);
            Assert.Equal(3, listCustomer[0].IdCustomer);
        }
        [Fact]
        public void ShouldBeAbleToUpdateCustomer()
        {
            var customerRepositoryFixture = new EFCustomerRepositoryFixture();
            EFCustomerRepository customerRepository = new EFCustomerRepository();
            Customer customer = customerRepositoryFixture.CreateMockCustomer();
            customer.FirstName = "Ivan";
            customer.LastName = "Ivanov";

            customerRepository.Update(customer);
            Assert.Equal("Ivan", customerRepository.Read(customer.IdCustomer).FirstName);
        }


    }


    public class EFCustomerRepositoryFixture
    {
        public Customer CreateMockCustomer()
        {
            var customerRepository = new EFCustomerRepository();
            var customer = MockCustomer();

            customerRepository.DeleteAll();

            customer.IdCustomer = customerRepository.Create(customer);

            return customer;
        }
        public Customer CreateMockCustomerWithoutDelete()
        {
            var customerRepository = new EFCustomerRepository();
            var customer = MockCustomer();

            customer.IdCustomer = customerRepository.Create(customer);

            return customer;
        }

        public Customer MockCustomer()
        {
            var customer = new Customer();
            customer.FirstName = "Elena";
            customer.LastName = "Levanova";

            customer.PhoneNumber = "+7986452563";
            customer.Email = "ellevanova@mail.ru";
            customer.TotalPurchasesAmount = 5;

            customer.AddressesList = new List<Address>();

            customer.AddressesList.Add(MockAddress());
            customer.Notes = new List<string>();
            customer.Notes.Add("Note1");
            customer.Notes.Add("Note2");
            customer.Notes.Add("Note3");

            return customer;
        }

        public Address MockAddress()
        {
            var address = new Address();

            address.AddressLine = "Street 23";
            address.AddressLine2 = "Street 25";
            address.City = "Ottava";
            address.PostalCode = "234563";
            address.State = "Ontario";
            address.Country = "Canada";

            return address;
        }
    }
}
