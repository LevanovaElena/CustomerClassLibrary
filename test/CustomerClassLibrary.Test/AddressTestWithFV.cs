using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerClassLibrary.Test
{
   public class AddressTestWithFV
    {

        [Fact]
        public void ShouldCreateCorrectCustomerObject()
        {
            Address address = new Address();
            AddressValidatorFV addressValidator = new AddressValidatorFV();

            address.AddressLine = "Street 23";
            address.AddressLine2 = "Street 25";
            address.City = "Ottava";
            address.PostalCode = "234563";
            address.State = "Ontario";
            address.Country = "Canada";


            addressValidator.AddressValidator();
            ValidationResult result = addressValidator.Validate(address);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
        [Fact]
        public void ValidateErrorCustomerObject()
        {
            Address address = new Address();
            AddressValidatorFV addressValidator = new AddressValidatorFV();

            address.AddressLine = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Molestias voluptatum necessitatibus porro nam";
            address.AddressLine2 = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Molestias voluptatum necessitatibus porro nam";
            address.City = "Lorem ipsum dolor sit amet consectetufgfgfggfrfhfhfhfhfhfhfhf";
            address.PostalCode = "23456322";
            address.State = "Lorem ipsum dolor sit amet consectetur adipisicing elit";
            address.Country = "Russia";

            addressValidator.AddressValidator();
            ValidationResult result = addressValidator.Validate(address);

            Assert.False(result.IsValid);
            Assert.Equal(6,result.Errors.Count);

        }
    }
}
