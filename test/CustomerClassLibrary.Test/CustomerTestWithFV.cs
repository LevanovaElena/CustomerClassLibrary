using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerClassLibrary.Test
{
   public class CustomerTestWithFV
    {

        [Fact]
        public void ShouldCreateCorrectCustomerObject()
        {
            Customer customer = new Customer();
            CustomerValidatorFV validator = new CustomerValidatorFV();

            customer.FirstName = "Elena";
            customer.LastName = "Levanova";
            customer.AddressesList = new List<Address>();
            customer.AddressesList.Add(new Address());
            customer.PhoneNumber = "+37494330650";
            customer.Email = "ellevanova@mail.ru";
            customer.Notes = new List<string>();
            customer.Notes.Add("Lorem.....");
            customer.TotalPurchasesAmount = 5;

            validator.CustomerValidator();
            ValidationResult result = validator.Validate(customer);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
        [Fact]
        public void ValidateErrorCustomerObject()
        {
            Customer customer = new Customer();
            CustomerValidatorFV validator = new CustomerValidatorFV();

            customer.FirstName = "Lorem ipsum dolor sit amet consecteturрпрпррпрппрпрп";
            customer.LastName = "Lorem ipsum dolor sit amet consecteturпрпрпрпрпрпрпр";
            customer.AddressesList = new List<Address>();
            customer.PhoneNumber = "+7232323232кекекеке33";
            customer.Email = "ellevanova-mail.ru";
            customer.Notes = new List<string>();
            customer.TotalPurchasesAmount = 5;

            validator.CustomerValidator();
            ValidationResult result = validator.Validate(customer);

            Assert.False(result.IsValid);
            Assert.Equal(6,result.Errors.Count);

        }
    }
}
