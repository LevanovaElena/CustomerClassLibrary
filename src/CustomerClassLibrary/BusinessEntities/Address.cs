using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary
{
    [Serializable]
    public class Address:Entity
    {
        [Required(ErrorMessage = "AddressLine is required.")]
        [MaxLength(100,ErrorMessage = "AddressLine should maximum 100 lenght.")]
        public string AddressLine { get; set; }


        [MaxLength(100, ErrorMessage = "AddressLine should maximum 100 lenght.")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "AddressType is required.")]
        public AddressType TypeAddress { get; set; }


        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City should maximum 50 lenght.")]
        public string City { get; set; }


        [Required(ErrorMessage = "PostalCode is required.")]
        [StringLength(6, ErrorMessage = "PostalCode should string with maximum 6 lenght.")]
        public string PostalCode { get; set; }


        [Required(ErrorMessage = "State is required.")]
        [StringLength(20, ErrorMessage = "State should maximum 20 lenght.")]
        public string State { get; set; }


        [Required(ErrorMessage = "Country is required.")]
        [CountryAttribute]
        public string Country { get; set; }
        public int IdAddress { get; set; } = 0;
        public int IdCustomer { get; set; } = 0;

        public Address()
        {

        }

        public Address(int idCustomer)
        {
            IdCustomer = idCustomer;
        }
    }


    [Serializable]
    public class Entity
    {

    }
}
