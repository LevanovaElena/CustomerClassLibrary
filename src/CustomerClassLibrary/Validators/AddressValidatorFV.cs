using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;

namespace CustomerClassLibrary
{
    public class AddressValidatorFV : AbstractValidator<Address>
    {
        public void AddressValidator()
        {
            RuleFor(address => address.AddressLine).NotNull().WithMessage("AddressLine is required.");
            RuleFor(address => address.AddressLine).Length(2, 100).WithMessage("AddressLine should maximum 100 lenght.");

            RuleFor(address => address.AddressLine2).Length(0, 100).WithMessage("AddressLine2 should maximum 100 lenght.");

            RuleFor(address => address.TypeAddressEnum).NotNull().WithMessage("TypeAddressEnum is required.");
            RuleFor(address => address.TypeAddressEnum).Must(item=> item==AddressType.Billing||item==AddressType.Shipping).WithMessage("TypeAddressEnum should AddressType.");

            RuleFor(address => address.City).NotNull().WithMessage("City is required.");
            RuleFor(address => address.City).Length(2, 50).WithMessage("City should maximum 50 lenght.");

            RuleFor(address => address.PostalCode).NotNull().WithMessage("PostalCode is required.");
            RuleFor(address => address.PostalCode).Length(0, 6).WithMessage("PostalCode should maximum 50 lenght.");

            RuleFor(address => address.State).NotNull().WithMessage("State is required.");
            RuleFor(address => address.State).Length(2, 20).WithMessage("State should maximum 20 lenght.");

            RuleFor(address => address.Country).NotNull().WithMessage("Country is required.");
            RuleFor(address => address.Country).Must(item => item == "United States" || item == "Canada").WithMessage("Country should AddressType.");


        }

    }

}
