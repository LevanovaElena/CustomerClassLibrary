using CustomerClassLibrary;
using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static IntegrationTests.CustomerRepositoryTest;

namespace IntegrationTests
{
    public class AddressRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleNoteRepository()
        {
            AddressRepository noteRepository = new AddressRepository();

            Assert.NotNull(noteRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            var customerRepositoryFixture = new CustomerRepositoryFixture();
            CustomerRepository customerRepository = new CustomerRepository();
            Customer customer = customerRepositoryFixture.CreateMockCustomer();

            AddressRepository addressRepository = new AddressRepository();

            Address address = new AddressRepositoryFixture().MockAddress();
            Address address2 = new AddressRepositoryFixture().MockAddress();

            addressRepository.Create(address,customer.IdCustomer);
            addressRepository.Create(address2, customer.IdCustomer);
        }

        [Fact]
        public void ShouldBeAbleToDeleteAllNoteByCustomer()
        {
            var repositoryFixture = new AddressRepositoryFixture();
            AddressRepository noteRep = new AddressRepository();
            repositoryFixture.CreateMockAddress();

            noteRep.DeleteAllByCustomer(repositoryFixture.IdCustomer);

        }
        [Fact]
        public void ShouldBeAbleToUpdateAddress()
        {
            var repositoryFixture = new AddressRepositoryFixture();
            AddressRepository noteRep = new AddressRepository();
            Address address = repositoryFixture.CreateMockAddress();

            address.City = "Moscow";

            noteRep.Update(address, repositoryFixture.IdCustomer);
        }
        [Fact]
        public void ShouldBeAbleToReadAddressByIdAddress()
        {
            var repositoryFixture = new AddressRepositoryFixture();
            AddressRepository noteRep = new AddressRepository();
            Address address = repositoryFixture.CreateMockAddress();

            Address addressRead = noteRep.ReadByIdAddress(address.IdAddress);
            Assert.Equal(address.AddressLine, addressRead.AddressLine);
            Assert.Equal(address.AddressLine2, addressRead.AddressLine2);
            Assert.Equal(address.TypeAddress, addressRead.TypeAddress);
            Assert.Equal(address.IdAddress, addressRead.IdAddress);
            Assert.Equal(address.Country, addressRead.Country);
            Assert.Equal(address.City, addressRead.City);
            Assert.Equal(address.PostalCode, addressRead.PostalCode);
            //Assert.Equal(address.State, addressRead.State);
        }
    }

    public class AddressRepositoryFixture : CustomerRepositoryFixture
    {
        public int IdCustomer { get; set; } = 0;
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

        public Address CreateMockAddress()
        {
            Customer customer = CreateMockCustomer();
            Address address = MockAddress();

            var addressRepository = new AddressRepository();
            addressRepository.DeleteAllByCustomer(customer.IdCustomer);

            address.IdAddress= addressRepository.Create(address, customer.IdCustomer);
            IdCustomer = customer.IdCustomer;
            return address;
        }
    }
}
