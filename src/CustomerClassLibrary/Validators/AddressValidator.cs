using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    [Serializable]
    [NotMapped]
    public class AddressWithValidationField : Address
    {
        public bool IsError { get; set; } = false;
        public string CountryError { get; set; } ="";
        
        public string AddressLineError { get; set; } = "";
        public string AddressLine2Error { get; set; } = "";
        public string CityError { get; set; } = "";
        public string PostalCodeError { get; set; } = "";
        public string StateError { get; set; } = "";
        public string TypeAddressError { get; set; } = "";

        public AddressWithValidationField(Address address)
        {
            Country = address.Country;
            AddressLine = address.AddressLine;
            AddressLine2 = address.AddressLine2;
            City = address.City;
            PostalCode = address.PostalCode;
            State = address.State;
            TypeAddress = address.TypeAddress;
            IdAddress = address.IdAddress;
            IdCustomer = address.IdCustomer;
        }
        public void ValidatorAddress(Address address)
        {
            var result = new List<Tuple<string, string>>();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(address);
            if (!Validator.TryValidateObject(address, context, results, true))
            {
                IsError = true;
                foreach (var error in results)

                {
                    foreach (var memberName in error.MemberNames)
                    {
                        switch (memberName){
                            case "Country":
                                CountryError = error.ErrorMessage;
                                break;
                            case "AddressLine":
                                AddressLineError = error.ErrorMessage;
                                break;
                            case "AddressLine2":
                                AddressLine2Error = error.ErrorMessage;
                                break;
                            case "City":
                                CityError = error.ErrorMessage;
                                break;
                            case "PostalCode":
                                PostalCodeError = error.ErrorMessage;
                                break;
                            case "State":
                                StateError = error.ErrorMessage;
                                break;
                            case "TypeAddress":
                                TypeAddressError = error.ErrorMessage;
                                break;
                        }
                        
                    }

                }
            }
            
        }

    }
}
