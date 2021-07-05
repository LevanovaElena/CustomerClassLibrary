using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.EFData
{
    class EFAddressRepository : IAddressRepository
    {
        public int Create(Address address, int idCustomer)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllByCustomer(int idCustomer)
        {
            throw new NotImplementedException();
        }

        public void DeleteByIdAddress(int idAddress)
        {
            throw new NotImplementedException();
        }

        public Address ReadByIdAddress(int idAddress)
        {
            throw new NotImplementedException();
        }

        public List<Address> ReadByIdCustomer(int idCustomer)
        {
            throw new NotImplementedException();
        }

        public void Update(Address address, int idCustomer)
        {
            throw new NotImplementedException();
        }
    }
}
