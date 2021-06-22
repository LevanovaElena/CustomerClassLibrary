using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.Business
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
            _addressRepository = new AddressRepository();
        }

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = new AddressRepository();
        }
        public CustomerService(ICustomerRepository customerRepository, IAddressRepository addressRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }

        public Customer CreateCustomer()
        {
            Customer customer = new Customer
            {
                FirstName = "Elena",
                LastName = "Levanova",
                AddressesList = this.CreateAddressList(),
                PhoneNumber = "+7986452563",
                Email = "ellevanova@mail.ru",
                Notes = new List<string>()
            };
            customer.Notes.Add("Lorem.....");
            customer.TotalPurchasesAmount = 5;
            CustomerValidator customerValidator = new();
            List<Tuple<string, string>> resultValidation = customerValidator.ValidatorCustomer(customer);
            if (resultValidation.Count > 0) throw new NotCorrectValuesForCreateCustomer();
            return customer;
        }
        public Address CreateAddress()
        {
            AddressValidator addressValidator = new AddressValidator();
            List<Tuple<string, string>> resultValidation;
            Address address = new Address();

            address.AddressLine = "Street 23";
            address.AddressLine2 = "Street 25";
            address.City = "Ottava";
            address.PostalCode = "234563";
            address.State = "Ontario";
            address.Country = "Canada";
            address.IdAddress = 10;

            //test empty object
            resultValidation = addressValidator.ValidatorAddress(address);
            if (resultValidation.Count > 0) throw new NotCorrectValuesForCreateAddress();
            return address;
        }
        public List<Address> CreateAddressList()
        {
            List<Address> addresses = new List<Address>();
            int i = 1;
            while (i <= 3)
            {

                addresses.Add(CreateAddress());
                i++;
            }

            return addresses;
        }

        public object InsertCustomer(Customer customer)
        {
            return _customerRepository.Create(customer);
        }

        public Customer AddCustomer()
        {
            Customer customer = CreateCustomer();
            customer.IdCustomer= _customerRepository.Create(customer);
            customer.AddressesList = this.CreateAddressList();
            foreach (Address address in customer.AddressesList)
            {
                _addressRepository.Create(address, customer.IdCustomer);
            }
            return customer;
        }

        public Customer GetCustomer(int id)
        {
            Customer customer = _customerRepository.Read(id);
            return customer;
        }
    }
}
