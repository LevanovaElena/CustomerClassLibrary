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
}
