using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.Business
{
    class Program
    {
        static public void Main(String[] args)
        {
            var customer = new Customer();
            customer.FirstName = "Elena";
            customer.LastName = "Levanova";

            customer.PhoneNumber = "+7986452563";
            customer.Email = "ellevanova@mail.ru";
            customer.TotalPurchasesAmount = 5;

            customer.AddressesList = new List<Address>();
            
            customer.Notes = new List<string>();
            customer.Notes.Add("Note1");
            customer.Notes.Add("Note2");
            customer.Notes.Add("Note3");

            var address = new Address();

            address.AddressLine = "Street 23";
            address.AddressLine2 = "Street 25";
            address.City = "Ottava";
            address.PostalCode = "234563";
            address.State = "Ontario";
            address.Country = "Canada";

            customer.AddressesList.Add(address);
            EFData.EFCustomerRepository eFCustomerRepository = new EFData.EFCustomerRepository();
            //eFCustomerRepository.Create(customer);
            customer.IdCustomer = eFCustomerRepository.Create(customer);
            Console.WriteLine(customer.IdCustomer);


            Console.WriteLine("id для выборки: "+customer.IdCustomer);

            Customer customer1 = eFCustomerRepository.Read(customer.IdCustomer);

            Console.WriteLine("прочли:"+customer1.FirstName);

            Console.WriteLine("customersCunt:" + eFCustomerRepository.CountOfCustomers());

            eFCustomerRepository.Delete(customer.IdCustomer);

            Console.WriteLine("customersCunt:" + eFCustomerRepository.CountOfCustomers());
        }
    }
}
