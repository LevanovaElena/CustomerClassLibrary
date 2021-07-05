using CustomerClassLibrary;
using CustomerClassLibrary.Common;
using CustomerClassLibrary.EFData;
using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace IntegrationTests

{
    public class EFAddressRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateEFAddressRepository()
        {
            var addressRepository = new EFAddressRepository();
            Assert.NotNull(addressRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            var addressRepositoryFixture = new EFAddressRepositoryFixture();

            EFAddressRepository addressRepository = new EFAddressRepository();

            Address address = addressRepositoryFixture.MockAddress();
            Address address2 = addressRepositoryFixture.MockAddress();

            //addressRepository.Create(address, customerRepositoryFixture.IdCustomer);
            Assert.NotEqual(0, addressRepository.Create(address, addressRepositoryFixture.IdCustomer));
            addressRepository.Create(address2, addressRepositoryFixture.IdCustomer);
        }

        [Fact]
        public void ShouldBeAbleReadAddressByIdAddress()
        {
            var addressRepositoryFixture = new EFAddressRepositoryFixture();
            EFAddressRepository addressRepository = new EFAddressRepository();
            Address address = addressRepositoryFixture.Address;

            Address addressRead = addressRepository.ReadByIdAddress(address.IdAddress);

            Assert.Equal(address.AddressLine, addressRead.AddressLine);
            Assert.Equal(address.AddressLine2, addressRead.AddressLine2);
            Assert.Equal(address.TypeAddress, addressRead.TypeAddress);
            Assert.Equal(address.IdAddress, addressRead.IdAddress);
            Assert.Equal(address.Country, addressRead.Country);
            Assert.Equal(address.City, addressRead.City);
            Assert.Equal(address.PostalCode, addressRead.PostalCode);
            //Assert.Equal(address.State, addressRead.State);
        }

        [Fact]
        public void ShouldBeAbleReadAddressByIdCustomer()
        {
            var addressRepositoryFixture = new EFAddressRepositoryFixture();
            EFAddressRepository addressRepository = new EFAddressRepository();
            Address address = addressRepositoryFixture.Address;

            List<Address> addressRead = addressRepository.ReadByIdCustomer(addressRepositoryFixture.IdCustomer);
            Assert.NotEmpty(addressRead);

        }

        [Fact]
        public void ShoulddeleteAddressByIdAddress()
        {
            var addressRepositoryFixture = new EFAddressRepositoryFixture();
            EFAddressRepository addressRepository = new EFAddressRepository();
            Address address = addressRepositoryFixture.Address;

            
            Assert.NotNull(addressRepository.ReadByIdAddress(address.IdAddress));
            addressRepository.DeleteByIdAddress(address.IdAddress);
            Assert.Null(addressRepository.ReadByIdAddress(address.IdAddress));

        }

        [Fact]
        public void ShouldDeleteAllAddressesByIdCustomers()
        {
            var addressRepositoryFixture = new EFAddressRepositoryFixture();
            EFAddressRepository addressRepository = new EFAddressRepository();
            Address address = addressRepositoryFixture.Address;


            Assert.NotNull(addressRepository.ReadByIdAddress(address.IdAddress));
            addressRepository.DeleteAllByCustomer(address.IdCustomer);
            Assert.Null(addressRepository.ReadByIdAddress(address.IdAddress));

        }

        [Fact]
        public void ShouldUpdateAddressByIdAddress()
        {
            var addressRepositoryFixture = new EFAddressRepositoryFixture();
            EFAddressRepository addressRepository = new EFAddressRepository();
            Address address = addressRepositoryFixture.Address;
            address.AddressLine = "new address line";
            address.AddressLine2 = "new address line2";

            addressRepository.Update(address, addressRepositoryFixture.IdCustomer);
            Assert.Equal("new address line",addressRepository.ReadByIdAddress(address.IdAddress).AddressLine);
            Assert.Equal("new address line2", addressRepository.ReadByIdAddress(address.IdAddress).AddressLine2);
        }
    }


    public class EFAddressRepositoryFixture
    {
       public int IdCustomer { get; set; } = 0;
        public Address Address { get; set; }

        public EFAddressRepositoryFixture()
        {
            IdCustomer = CreateMockCustomer().IdCustomer;
        }
        public Customer CreateMockCustomer()
        {
            var customerRepository = new EFCustomerRepository();
            var customer = MockCustomer();

            customerRepository.DeleteAll();

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
            this.Address = address;
            return address;
        }
    }
}
