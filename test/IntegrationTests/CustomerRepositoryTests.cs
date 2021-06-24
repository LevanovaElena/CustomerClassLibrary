using CustomerClassLibrary;
using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;

namespace IntegrationTests
{
    public class CustomerRepositoryTest 
    {
        public class CustomerRepositoryFixture
        {
            public Customer CreateMockCustomer()
            {
                var customerRepository = new CustomerRepository();
                var customer = MockCustomer();

                customerRepository.DeleteAll();

                customer.IdCustomer=customerRepository.Create(customer);

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
                customer.AddressesList.Add(new Address());
                customer.Notes = new List<string>();
                customer.Notes.Add("Note1");
                customer.Notes.Add("Note2");
                customer.Notes.Add("Note3");

                return customer;
            }
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomerRepository()
        {
            var customerRepository = new CustomerRepository();
            Assert.NotNull(customerRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var customerRepositoryFixture = new CustomerRepositoryFixture();
            CustomerRepository customerRepository = new CustomerRepository();
            Customer customer = customerRepositoryFixture.MockCustomer();

            customerRepository.DeleteAll();

            int id= customerRepository.Create(customer);
            Assert.Equal(1,id);

        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            var customerRepositoryFixture = new CustomerRepositoryFixture();
            CustomerRepository customerRepository = new CustomerRepository();
            Customer customer = customerRepositoryFixture.CreateMockCustomer();

            customerRepository.Delete(customer.IdCustomer);
        }
        [Fact]
        public void ShouldBeAbleToUpdateCustomer()
        {
            var customerRepositoryFixture = new CustomerRepositoryFixture();
            CustomerRepository customerRepository = new CustomerRepository();
            Customer customer = customerRepositoryFixture.CreateMockCustomer();

            customerRepository.Update(customer);
        }

        [Fact]
        public void ShouldBeAbleToSelectCustomer()
        {
            var customerRepositoryFixture = new CustomerRepositoryFixture();
            CustomerRepository customerRepository = new CustomerRepository();
            Customer customer = customerRepositoryFixture.CreateMockCustomer();

            customer=customerRepository.Read(customer.IdCustomer);
            Assert.NotEmpty(customer.Notes);

            Assert.Null(customerRepository.Read(20));
        }

        [Fact]
        public void ShouldBeAbleToSelectAllCustomer()
        {
            var customerRepositoryFixture = new CustomerRepositoryFixture();
            CustomerRepository customerRepository = new CustomerRepository();
            Customer customer = customerRepositoryFixture.CreateMockCustomer();

            var listCustomer = customerRepository.ReadAll();
            Assert.NotEmpty(listCustomer);

        }



    }
}
