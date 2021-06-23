using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary
{
    public class CountryAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string Country = value.ToString();
                if (Country == "United States"|| Country == "Canada")
                    return true;
                else
                    this.ErrorMessage = "Country should be only 'United States' or 'Canada'";
            }
            return false;
        }
    }
    public class AddressListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                List<Address> addressList = value as List<Address>;
                if (addressList.Count>0)
                    return true;
                else
                    this.ErrorMessage = "AddressList  must contain at least one address";
            }
            return false;
        }
    }

    public class NotesListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                List<string> addressList = value as List<string>;
                if (addressList.Count > 0)
                    return true;
                else
                    this.ErrorMessage = "Note cannot be empty, at least 1 note must be provided.";
            }
            return false;
        }
    }
}
