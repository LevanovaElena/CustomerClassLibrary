using System;
using System.Collections.Generic;
using Xunit;

namespace CustomerClassLibrary.Test
{
    public class UnitTestSAddress
    {
        [Fact]
        public void ShouldCreateAdressType()
        {
            Assert.Equal(0,(decimal)AddressType.Shipping);
            Assert.Equal(1, (decimal)AddressType.Billing);
        }
        [Fact]
        public void ShouldCreateEmptyAdressObject()
        {
            Address address = new Address();
            AddressValidator addressValidator = new AddressValidator();
            List<Tuple<string, string>> resultValidation;

            //test empty object
            resultValidation= addressValidator.ValidatorAddress(address);
            Assert.Equal(5,resultValidation.Count);

            foreach(Tuple<string,string> item in resultValidation)
            {
                switch (item.Item1)
                {
                    case "AddressLine":
                        Assert.Equal("AddressLine is required.", item.Item2);
                        break;
                    case "City":
                        Assert.Equal("City is required.", item.Item2);
                        break;
                    case "PostalCode":
                        Assert.Equal("PostalCode is required.", item.Item2);
                        break;
                    case "State":
                        Assert.Equal("State is required.", item.Item2);
                        break;
                    case "Country":
                        Assert.Equal("Country is required.", item.Item2);
                        break;
                }
                    
            }

            address.TypeAddress = AddressType.Billing;
            Assert.Equal(1, (decimal)address.TypeAddress);
            address.TypeAddress = AddressType.Shipping;
            Assert.Equal(0, (decimal)address.TypeAddress);
          

        }
        [Fact]
        public void ShouldCreateCorrectAdressObject()
        {
            Address address = new Address();
            AddressValidator addressValidator = new AddressValidator();
            List<Tuple<string, string>> resultValidation;

            address.AddressLine = "Street 23";
            address.AddressLine2 = "Street 25";
            address.City = "Ottava";
            address.PostalCode = "234563";
            address.State = "Ontario";
            address.Country = "Canada";

            //test empty object
            resultValidation = addressValidator.ValidatorAddress(address);
            Assert.Empty(resultValidation);

            address.Country = "United States";
            resultValidation = addressValidator.ValidatorAddress(address);
            Assert.Empty(resultValidation);

            address.AddressLine2 = "";
            resultValidation = addressValidator.ValidatorAddress(address);
            Assert.Empty(resultValidation);

        }
        [Fact]
        public void ValidateErrorAdressObject()
        {
            Address address = new Address();
            AddressValidator addressValidator = new AddressValidator();
            List<Tuple<string, string>> resultValidation;

            address.AddressLine = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Molestias voluptatum necessitatibus porro nam";
            address.AddressLine2 = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Molestias voluptatum necessitatibus porro nam";
            address.City = "Lorem ipsum dolor sit amet consectetur";
            address.PostalCode = "23456322";
            address.State = "Lorem ipsum dolor sit amet consectetur adipisicing elit";
            address.Country = "Russia";

            resultValidation = addressValidator.ValidatorAddress(address);
            Assert.NotEmpty(resultValidation);

            foreach (Tuple<string, string> item in resultValidation)
            {
                switch (item.Item1)
                {
                    case "AddressLine":
                        Assert.Equal("AddressLine should maximum 100 lenght.", item.Item2);
                        break;
                    case "AddressLine2":
                        Assert.Equal("AddressLine should maximum 100 lenght.", item.Item2);
                        break;
                    case "City":
                        Assert.Equal("City should maximum 50 lenght.", item.Item2);
                        break;
                    case "PostalCode":
                        Assert.Equal("PostalCode should string with maximum 6 lenght.", item.Item2);
                        break;
                    case "State":
                        Assert.Equal("State should maximum 20 lenght.", item.Item2);
                        break;
                    case "Country":
                        Assert.Equal("Country should be only 'United States' or 'Canada'", item.Item2);
                        break;
                }

            }
        }


    }
}
