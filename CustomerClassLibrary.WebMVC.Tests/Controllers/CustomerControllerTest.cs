using CustomerClassLibrary.WebMVC.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace CustomerClassLibrary.WebMVC.Tests.Controllers
{
    [TestClass]
    public class CustomersControllerTest
    {
        [TestMethod]
        public void SholuldBeCreateCustomer()
        {
            // Arrange
            CustomerController controller = new CustomerController();
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void SholuldReturnListOfCustomers()
        {
            // Arrange
            CustomerController controller = new CustomerController();
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
