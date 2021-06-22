using CustomerClassLibrary;
using CustomerClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary
{
    public class ImplementationWorkOfClasses
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        public ImplementationWorkOfClasses()
        {
            _customerRepository = new CustomerRepository();
            _addressRepository = new AddressRepository();
        }

        public ImplementationWorkOfClasses(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = new AddressRepository();
        }
        public ImplementationWorkOfClasses(ICustomerRepository customerRepository,IAddressRepository addressRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }
        public List<Customer> Customers { get; set; } = new List<Customer>();

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


        public Customer GetCustomer(int id)
        {
            Customer customer = _customerRepository.Read(id);
            return customer;
        }

        public void AddedCustomer()
        {

            Customer customer = CreateCustomer();
            CustomerValidator customerValidator = new();
            List<Tuple<string, string>> resultValidation=customerValidator.ValidatorCustomer(customer);
            if (resultValidation.Count > 0) throw new NotCorrectValuesForCreateCustomer();
            else
            {
                customer.IdCustomer = SaveCustomerInBd(customer);
                foreach (Address address in customer.AddressesList)
                {
                    SaveAddressInBd(address, customer.IdCustomer);
                }
                Customers.Add(customer);
            }

        }

        public void DeleteCustomer()
        {
            if (Customers.Count > 0)
            {
                _customerRepository.Delete(Customers[0].IdCustomer);
            }
        }

        public void UpdateCustomer()
        {
            Customer customer = Customers[0];
            customer.FirstName = "Ivan";
            customer.LastName = "Ivanov";
            customer.AddressesList[0].Country = "United States";

            _customerRepository.Update(customer);
            UpdateAddressList(customer);
        }

        public void UpdateAddressList(Customer customer)
        {
            foreach (Address address in customer.AddressesList)
            {
                _addressRepository.Update(address, customer.IdCustomer);
            }
        }

        public int SaveCustomerInBd(Customer customer)
        {
            int idCustomer= _customerRepository.Create(customer);

            return idCustomer;
        }

       public int SaveAddressInBd(Address address,int idCustomer)
        {
            return _addressRepository.Create(address, idCustomer);
        }


    }
}
