using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.Business
{
    public class CustomerService:ICustomerService
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
            customer.Notes.Add("Lorem1.....");
            customer.Notes.Add("Lorem2.....");
            customer.TotalPurchasesAmount = 5;
            CustomerValidator customerValidator = new CustomerValidator();
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

        public int InsertCustomer(Customer customer)
        {
            customer.AddressesList=CreateAddressList();
            int k = _customerRepository.Create(customer);
            foreach (Address address in customer.AddressesList)
            {
                _addressRepository.Create(address, k);
            }
            return k;
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
            if(customer!=null) customer.AddressesList = _addressRepository.ReadByIdCustomer(customer.IdCustomer);
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> listCustomers = _customerRepository.ReadAll();

            if (listCustomers.Count > 0)
            {
                foreach(Customer customer in listCustomers)
                {
                    customer.AddressesList = _addressRepository.ReadByIdCustomer(customer.IdCustomer);
                }
            }
            return listCustomers;
        }

        public void DeleteCustomer(int idCustomer)
        {
             _customerRepository.Delete(idCustomer);
        }
    }

    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        public Customer GetCustomer(int id);
        public void DeleteCustomer(int idCustomer);
    }
}
