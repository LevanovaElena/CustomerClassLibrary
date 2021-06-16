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
        public List<Customer> Customers { get; set; } = new List<Customer>();


        public void AddedCustomer()
        {
            Customer customer = new Customer();
            customer.FirstName = "Elena";
            customer.LastName = "Levanova";
            customer.AddressesList = this.CreateAddressList();
            customer.PhoneNumber = "+7986452563";
            customer.Email = "ellevanova@mail.ru";
            customer.Notes = new List<string>();
            customer.Notes.Add("Lorem.....");
            customer.TotalPurchasesAmount = 5;

            CustomerValidator customerValidator = new CustomerValidator();
            List<Tuple<string, string>> resultValidation=customerValidator.ValidatorCustomer(customer);
            if (resultValidation.Count > 0) throw new NotCorrectValuesForCreateCustomer();
            else
            {
                SaveCustomerInBd(customer);
                Customers.Add(customer);
            }

        }

        public void DeleteCustomer()
        {
            if (Customers.Count > 0)
            {
                CustomerRepository customerRepository = new CustomerRepository();
                customerRepository.Delete(Customers[0].IdCustomer);
            }
        }

        public void UpdateCustomer()
        {
            Customer customer = Customers[0];
            CustomerRepository customerRepository = new CustomerRepository();
            customer.FirstName = "Ivan";
            customer.LastName = "Ivanov";
            customer.AddressesList[0].Country = "United States";

            customerRepository.Update(customer);
            UpdateAddressList(customer);
        }

        public void UpdateAddressList(Customer customer)
        {
            AddressRepository addressRepository = new AddressRepository();
            foreach (Address address in customer.AddressesList)
            {
                addressRepository.Update(address, customer.IdCustomer);
            }
        }

        public void SaveCustomerInBd(Customer customer)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            int idCustomer= customerRepository.Create(customer);
            customer.IdCustomer = idCustomer;
            AddressRepository addressRepository = new AddressRepository();
            foreach (Address address in customer.AddressesList)
            {
                addressRepository.Create(address, idCustomer);
            }
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
    }
}
