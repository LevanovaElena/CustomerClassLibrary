using CustomerClassLibrary.Business;
using CustomerClassLibrary.WebForm;
using Moq;
using System;
using Xunit;

namespace CustomerClassLibrary.WebForms.Tests
{
    public class CustomerEditTests
    {
        [Fact]
        public void ShouldeBeGetIdCustomerAndObject()
        {
            var customerEdit = new CustomerEdit();

            var customerService = new Mock<ICustomerService>();

            customerEdit = new CustomerEdit(customerService.Object);
            //customerEdit.LoadCustomer("1");
            customerService
               .Setup(x => x.GetCustomer(1))
               .Returns(() => It.IsAny<Customer>());
        }
    }
}
