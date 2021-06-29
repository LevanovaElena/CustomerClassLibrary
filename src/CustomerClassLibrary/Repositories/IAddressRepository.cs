using System.Collections.Generic;

namespace CustomerClassLibrary.Repositories
{
    public interface IAddressRepository
    {
        public int Create(Address address, int idCustomer);
        public Address ReadByIdAddress(int idAddress);
        public void Update(Address address, int idCustomer);

        public void DeleteAllByCustomer(int idCustomer);
        List<Address> ReadByIdCustomer(int idCustomer);
        public void DeleteByIdAddress(int idAddress);
    }
}