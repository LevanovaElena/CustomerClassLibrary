using CustomerClassLibrary.EFData;
using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.Business
{
    public class EFCustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;

        public EFCustomerService()
        {
            _customerRepository = new EFCustomerRepository();
            _addressRepository = new EFAddressRepository();
        }

        public EFCustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = new EFAddressRepository();
        }
        public EFCustomerService(ICustomerRepository customerRepository, IAddressRepository addressRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }

        public Customer Create(Customer customer)
        {
            customer.IdCustomer= _customerRepository.Create(customer);
            return customer;
        }

        public void DeleteCustomer(int idCustomer)
        {
            _customerRepository.Delete(idCustomer);
        }

        public List<Customer> GetAllCustomers()
        {
            var list = _customerRepository.ReadAll();
            return list;
        }

        public List<Customer> GetAllCustomersFromNumber(int numberOfRow, int sumRow)
        {
            var list = _customerRepository.ReadCustomerFromNumber(numberOfRow,sumRow);
            return list;
        }

        public int GetCountCustomer()
        {
            return _customerRepository.CountOfCustomers();
        }

        public Customer GetCustomer(int id)
        {
            Customer customer = _customerRepository.Read(id);
            return customer;
        }

        public void UpdateAddress(Address address, int idCustomer)
        {
            _addressRepository.Update(address, idCustomer);
        }
        public void DeleteAddress(int idAddress)
        {
            _addressRepository.DeleteByIdAddress(idAddress);
        }
        public void UpdateAddressList(Customer customer)
        {
            foreach(Address address in customer.AddressesList)
            {
                if (address.IdAddress != 0) _addressRepository.Update(address, customer.IdCustomer);
                else _addressRepository.Create(address, customer.IdCustomer);
            }
        }

        public Customer UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
            UpdateAddressList(customer);
            return customer;
        }

        public List<Tuple<string, string>> Validate(Customer customer)
        {
            CustomerValidator customerValidator = new CustomerValidator();
            List<Tuple<string, string>> resultValidation = customerValidator.ValidatorCustomer(customer);
            return resultValidation;
        }

        public List<Tuple<string, string>> ValidateAddress(Address address)
        {
            AddressValidator addressValidator = new AddressValidator();
            List<Tuple<string, string>> resultValidation = addressValidator.ValidatorAddress(address);
            return resultValidation;
        }
    }
}
