using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary
{
    public class CustomerValidator
    {
        public List<Tuple<string, string>> ValidatorCustomer(Customer cust)
        {
            var result = new List<Tuple<string, string>>();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(cust);
            if (!Validator.TryValidateObject(cust, context, results, true))
            {
                foreach (var error in results)

                {
                    foreach (var memberName in error.MemberNames)
                    {
                        result.Add(new Tuple<string, string>(memberName, error.ErrorMessage));
                    }

                }
            }
            return result;
        }
    }
}
