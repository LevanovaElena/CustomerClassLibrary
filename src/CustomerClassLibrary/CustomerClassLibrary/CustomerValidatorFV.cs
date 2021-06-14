using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;

namespace CustomerClassLibrary
{
    public class CustomerValidatorFV : AbstractValidator<Customer>
    {
        public void CustomerValidator()
        {
            RuleFor(customer => customer.FirstName).Length(2, 50).WithMessage("LastName should maximum 50 lenght."); 

            RuleFor(customer => customer.LastName).NotNull().WithMessage("LastName is required.");
            RuleFor(customer => customer.LastName).Length(2, 50).WithMessage("LastName should maximum 50 lenght.");

            RuleFor(customer => customer.TotalPurchasesAmount).Must(i=> i is decimal).WithMessage("TotalPurchasesAmount should decimal.");

            
            RuleFor(customer => customer.PhoneNumber).Must(BeAValidPhone).WithMessage("PhoneNumber accept only E.164 format.");
            string patternEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            RuleFor(customer => customer.Email).Must(item => Regex.IsMatch(item, patternEmail, RegexOptions.IgnoreCase)).WithMessage("Email accept only email format.");

            RuleFor(customer => customer.AddressesList).NotEmpty().WithMessage("AddressesList must be at least one address.");
            RuleFor(customer => customer.Notes).NotEmpty().WithMessage("Note cannot be empty, at least 1 note must be provided.");


        }

        private bool BeAValidPhone(string phone)
        {
            if (phone == null || phone == "") return false;

            string patternPhone = @"^\+[1-9]\d{10,14}$";
            return Regex.IsMatch(phone, patternPhone, RegexOptions.IgnoreCase);
        }
    }

}
