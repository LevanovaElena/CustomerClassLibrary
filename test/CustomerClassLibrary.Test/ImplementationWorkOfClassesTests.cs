using CustomerClassLibrary;
using CustomerClassLibrary.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class ImplementationWorkOfClassesTests
    {
        [Fact]
        public void ShouldBeAbleCreateClass() 
        {
            ImplementationWorkOfClasses implementationWork = new ImplementationWorkOfClasses();
        }

        [Fact]
        public void ShouldBeCreateCustomer()
        {
            ImplementationWorkOfClasses implementationWork = new ImplementationWorkOfClasses();
            Assert.NotNull(implementationWork.CreateCustomer());
        }

        [Fact]
        public void ShouldBeCreateAddress()
        {
            ImplementationWorkOfClasses implementationWork = new ImplementationWorkOfClasses();
            Assert.NotNull(implementationWork.CreateAddress());
        }

        [Fact]
        public void ShouldBeCreateAddressList()
        {
            ImplementationWorkOfClasses implementationWork = new ImplementationWorkOfClasses();
            Assert.NotEmpty(implementationWork.CreateAddressList());
        }

        [Fact]

        public void ShouldGetCustomerById()
        {
            var customerCustomerMock = new Mock<ICustomerRepository>();
            var customerExpected = new Customer();

            customerCustomerMock
                .Setup(x => x.Read(1))
                .Returns(() => customerExpected);

            var servise = new ImplementationWorkOfClasses(customerCustomerMock.Object);
            var customer = servise.GetCustomer(1);

            Assert.Equal(customerExpected, customer);
        }

        [Fact]
        public void ShouldBeAbleSaveCustomerInBd()
        {
            var customerCustomerMock = new Mock<ICustomerRepository>();
            ImplementationWorkOfClasses implementationWork = new ImplementationWorkOfClasses();
            Customer customer = implementationWork.CreateCustomer();
            int expectedIdCustomer = 2;

            customerCustomerMock
                .Setup(x => x.Create(customer))
                .Returns(() => expectedIdCustomer);

            var servise = new ImplementationWorkOfClasses(customerCustomerMock.Object);
            var idCustomer = servise.SaveCustomerInBd(customer);
            Assert.Equal(expectedIdCustomer, idCustomer);
        }

        [Fact]
        public void ShouldBeAbleSaveAddressInBd()
        {
            var customerCustomerMock = new Mock<ICustomerRepository>();
            var addressAddressMock = new Mock<IAddressRepository>();
            ImplementationWorkOfClasses implementationWork = new ImplementationWorkOfClasses();
            Address address = implementationWork.CreateAddress();
            int expectedIdAddress = 1;
            int idCustomer = 1;

            addressAddressMock
                .Setup(x => x.Create(address,idCustomer))
                .Returns(() => expectedIdAddress);

            var servise = new ImplementationWorkOfClasses(customerCustomerMock.Object,addressAddressMock.Object);
            var idAddress = servise.SaveAddressInBd(address,idCustomer);
            Assert.Equal(expectedIdAddress, idAddress);
            addressAddressMock
                .Verify(x => x.Create(address, idCustomer), Times.AtLeastOnce);
        }

        [Fact]
        public void ShouldBeAbleDeleteCustomer()
        {
            var customerCustomerMock = new Mock<ICustomerRepository>();

            int expectedIdCustomer = 1;

            customerCustomerMock
                .Setup(x => x.Delete(expectedIdCustomer));

           var implementationWork = new ImplementationWorkOfClasses(customerCustomerMock.Object);
            Customer customer = implementationWork.CreateCustomer();
            implementationWork.Customers.Add(customer);
            implementationWork.DeleteCustomer();
           customerCustomerMock
                .Verify(x => x.Delete(expectedIdCustomer), Times.Never);

        }

        [Fact]
        public void ShouldBeAbleUpdateCustomer()
        {
            ImplementationWorkOfClasses implementationWork = new ImplementationWorkOfClasses();
            implementationWork.AddedCustomer();

            implementationWork.UpdateCustomer();

        }
    }

    
}
