using System;
using System.Collections.Generic;
using Xunit;

namespace CustomerClassLibrary.Test
{
    public class UnitTestCustomer
    {
        [Fact]
        public void ShouldCreateEmptyCustomerObject()
        {
            
            Customer address = new Customer();
            CustomerValidator addressValidator = new CustomerValidator();
            List<Tuple<string, string>> resultValidation;

            //test empty object
            resultValidation= addressValidator.ValidatorCustomer(address);
            Assert.Equal(3,resultValidation.Count);

            foreach(Tuple<string,string> item in resultValidation)
            {
                switch (item.Item1)
                {
                    case "LastName":
                        Assert.Equal("LastName is required.", item.Item2);
                        break;
                    case "AddressesList":
                       // Assert.Equal("AddressesList is required.", item.Item2);
                        break;
                    case "Notes":
                        Assert.Equal("Note cannot be empty, at least 1 note must be provided.", item.Item2);
                        break;
                }
                    
            }

        }
        [Fact]
        public void ShouldCreateCorrectCustomerObject()
        {
            Customer customer = new Customer();
            CustomerValidator addressValidator = new CustomerValidator();
            List<Tuple<string, string>> resultValidation;

            customer.FirstName = "Elena";
            customer.LastName = "Levanova";
            customer.AddressesList = new List<Address> ();
            customer.AddressesList.Add(new Address());
            customer.PhoneNumber = "+7986452563";
            customer.Email = "ellevanova@mail.ru";
            customer.Notes= new List<string>();
            customer.Notes.Add("Lorem.....");
            customer.TotalPurchasesAmount = 5;

            resultValidation = addressValidator.ValidatorCustomer(customer);
            Assert.Empty(resultValidation);

            customer.TotalPurchasesAmount = null;
            resultValidation = addressValidator.ValidatorCustomer(customer);
            Assert.Empty(resultValidation);
        }

        [Fact]
        public void ValidateErrorCustomerObject()
        {
            Customer customer = new Customer();
            CustomerValidator addressValidator = new CustomerValidator();
            List<Tuple<string, string>> resultValidation;

            customer.FirstName = "Lorem ipsum dolor sit amet consecteturποποπποποοποπο";
            customer.LastName = "Lorem ipsum dolor sit amet consecteturοποποποποποποπ";
            customer.AddressesList = new List<Address>();
            customer.PhoneNumber = "+7232323232κεκεκεκε33";
            customer.Email = "ellevanova-mail.ru";
            customer.Notes = new List<string>();
            //customer.TotalPurchasesAmount = 5.3;

            //test empty object
            resultValidation = addressValidator.ValidatorCustomer(customer);
            Assert.Equal(6, resultValidation.Count);

            foreach (Tuple<string, string> item in resultValidation)
            {
                switch (item.Item1)
                {
                    case "FirstName":
                        Assert.Equal("FirstName should maximum 50 lenght.", item.Item2);
                        break;
                    case "LastName":
                        Assert.Equal("LastName should maximum 50 lenght.", item.Item2);
                        break;
                    case "AddressesList":
                        Assert.Equal("AddressList  must contain at least one address", item.Item2);
                        break;
                    case "PhoneNumber":
                        Assert.Equal("PhoneNumber accept only E.164 format.", item.Item2);
                        break;
                    case "Email":
                        Assert.Equal("Email accept only email format.", item.Item2);
                        break;
                    case "Notes":
                        Assert.Equal("Note cannot be empty, at least 1 note must be provided.", item.Item2);
                        break;
                }

            }

        }

    }
}
