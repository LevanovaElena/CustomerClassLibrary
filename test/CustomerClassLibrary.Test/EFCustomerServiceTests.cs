using CustomerClassLibrary.Business;
using CustomerClassLibrary.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerClassLibrary.Test
{
    public class EFCustomerServiceTests
    {
        [Fact]
        public void ShouldBeAbleCreateEFCustomerService()
        {
            Assert.NotNull( new EFCustomerService());
            var customerCustomerMock = new Mock<ICustomerRepository>();
            Assert.NotNull(new EFCustomerService(customerCustomerMock.Object));
            var addressAddressMock = new Mock<IAddressRepository>();
            Assert.NotNull(new EFCustomerService(customerCustomerMock.Object, addressAddressMock.Object));
        }

        [Fact]
        public void ShouldBeCreateCustomer()
        {
            CustomerServiceFixture customerServiceFixture = new CustomerServiceFixture();
            var customerExpected = new Customer();
            customerServiceFixture.customerCustomerMock
                .Setup(x => x.Read(1))
                .Returns(() => customerExpected);
        }

        [Fact]
        public void ShouldBeCreateAddress()
        {
            CustomerService customerService = new CustomerService();
            Assert.NotNull(customerService.CreateAddress());
        }

        [Fact]
        public void ShouldBeCreateAddressList()
        {
            CustomerService customerService = new CustomerService();
            Assert.NotEmpty(customerService.CreateAddressList());
        }


        [Fact]

        public void ShouldGetCustomerById()
        {
            CustomerServiceFixture customerServiceFixture = new CustomerServiceFixture();
            var customerExpected = new Customer();
            customerServiceFixture.customerCustomerMock
                .Setup(x => x.Read(1))
                .Returns(() => customerExpected);

            customerServiceFixture.CreateMockRepositories();
            var service = customerServiceFixture.Service;

            var customer = service.GetCustomer(1);

            Assert.Equal(customerExpected, customer);
            customerServiceFixture.customerCustomerMock
                .Verify(x => x.Read(1), Times.AtLeastOnce);
        }

        [Fact]
        public void ShouldGetAllCustomers()
        {
            CustomerServiceFixture customerServiceFixture = new CustomerServiceFixture();
            var customerExpected = new Customer();
            List<Customer> list = new List<Customer>();
            list.Add(customerExpected);
            customerServiceFixture.customerCustomerMock
                .Setup(x => x.ReadAll())
                .Returns(() => list);

            customerServiceFixture.CreateMockRepositories();
            var service = customerServiceFixture.Service;

            var customer = service.GetAllCustomers();


            customerServiceFixture.customerCustomerMock
                .Verify(x => x.ReadAll(), Times.AtLeastOnce);
        }


        [Fact]
        public void ShouldBeAbleSaveCustomerInBd()
        {
            CustomerServiceFixture customerServiceFixture = new CustomerServiceFixture();


            customerServiceFixture.CreateMockRepositories();
            var service = customerServiceFixture.Service;

            Customer customer = service.CreateCustomer();
            customer.AddressesList = service.CreateAddressList();

            customerServiceFixture.customerCustomerMock
                .Setup(x => x.Create(customer))
                .Returns(1);

            customerServiceFixture.addressAddressMock
                .Setup(x => x.Create(customer.AddressesList[0], 1))
                .Returns(1);

            var customerActual = service.InsertCustomer(customer);

            Assert.Equal(1, customerActual);

            customerServiceFixture.customerCustomerMock
                .Verify(x => x.Create(customer), Times.AtLeastOnce);
            customerServiceFixture.addressAddressMock
                .Verify(x => x.Create(customer.AddressesList[0], 1), Times.AtLeastOnce);
        }
  
    
    }

    public class EFCustomerServiceFixture
    {
        public EFCustomerService Service { get; set; }
        public Mock<ICustomerRepository> customerCustomerMock;
        public Mock<IAddressRepository> addressAddressMock;
        public EFCustomerServiceFixture()
        {
            customerCustomerMock = new Mock<ICustomerRepository>();
            addressAddressMock = new Mock<IAddressRepository>();
        }

        public void CreateMockRepositories()
        {
            Service = new EFCustomerService(customerCustomerMock.Object, addressAddressMock.Object);
        }
    }


}
