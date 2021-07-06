using CustomerClassLibrary.Common;
using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.EFData
{
    public class EFAddressRepository : IAddressRepository
    {
        private EFData.CustomerLibraryContext _context;

        public EFAddressRepository()
        {
            _context = new CustomerLibraryContext();
        }
        public int Create(Address address, int idCustomer)
        {
            address.IdCustomer = idCustomer;
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return address.IdAddress;
        }

        public Address ReadByIdAddress(int idAddress)
        {
            var address = _context.Addresses.Find(idAddress);
            return address;
        }

        public List<Address> ReadByIdCustomer(int idCustomer)
        {
            var addresses = _context.Addresses
                .Where(adr=>adr.IdCustomer==idCustomer)
                .ToList();
            return addresses;
        }

        public void DeleteAllByCustomer(int idCustomer)
        {

                var listDelete= _context.Addresses.RemoveRange(_context.Addresses.Where(x => x.IdCustomer == idCustomer));
                _context.SaveChanges();
           
               if(listDelete==null)throw new NotFoundAddressWithId("Not found addresses for customer id= " + idCustomer); 
            }

        public void DeleteByIdAddress(int idAddress)
        {
            Address address = ReadByIdAddress(idAddress);
            if (address == null) throw new NotFoundAddressWithId("Not found address with id= " + idAddress);
            else this.Delete(address);
        }

        private void Delete(Address address)
        {
            try
            {
                _context.Addresses.Remove(address);
                _context.SaveChanges();
            }
            catch
            {
                throw new NotFoundAddressWithId("Not found address for delete with id=" + address.IdAddress);
            }
        }

        public void Update(Address addressNew, int idCustomer)
        {
            Address addressOld = _context.Addresses
                .Where(c => c.IdAddress == addressNew.IdAddress)
                .FirstOrDefault();
            if (addressOld != null)
            {
                addressOld.AddressLine = addressNew.AddressLine;
                addressOld.AddressLine2 = addressNew.AddressLine2;
                addressOld.City = addressNew.City;
                addressOld.Country = addressNew.Country;
                addressOld.IdCustomer = addressNew.IdCustomer;
                addressOld.PostalCode = addressNew.PostalCode;
                addressOld.State = addressNew.State;
                addressOld.TypeAddressEnum = addressNew.TypeAddressEnum;
            }

            _context.SaveChanges();
        }
    }
}
