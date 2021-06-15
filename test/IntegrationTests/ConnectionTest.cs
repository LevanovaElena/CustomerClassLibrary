using CustomerClassLibrary.Repositories;
using System;
using Xunit;

namespace IntegrationTests
{
    public class ConnectionTest
    {
        class MockConnection : BaseRepository
        {

        }

        [Fact]
        public void ShouldOpenComnnection()
        {
            
            var mock = new MockConnection();
            var connect = mock.GetConnection();
            Assert.NotNull(connect);

            using (connect)
            {
                connect.Open();
                Assert.Equal<System.Data.ConnectionState>(System.Data.ConnectionState.Open, connect.State);
                connect.Close();
                Assert.Equal<System.Data.ConnectionState>(System.Data.ConnectionState.Closed, connect.State);
            }
        }
    }
}
