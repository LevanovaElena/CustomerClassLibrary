using CustomerClassLibrary;
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
        public void ShouldBeAbleAddedCustomer()
        {
            ImplementationWorkOfClasses implementationWork = new ImplementationWorkOfClasses();
            implementationWork.AddedCustomer();

        }
    }
}
