using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary
{
    public class AddressValidator
    {
        public List<Tuple<string, string>> ValidatorAddress(Address address)
        {
            var result = new List<Tuple<string, string>>();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(address);
            if (!Validator.TryValidateObject(address, context, results, true))
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
