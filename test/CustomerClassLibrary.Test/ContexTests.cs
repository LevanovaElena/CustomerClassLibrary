using CustomerClassLibrary.EFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerClassLibrary.Test
{
    public class ContextTests
    {
        [Fact]
        public void ShouldBeAbleDataContext()
        {
            var context = new CustomerLibraryContext();
            Assert.NotNull(context.Customers);
            Assert.NotNull(context.Addresses);
        }

    }
}
