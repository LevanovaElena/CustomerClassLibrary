using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary
{
     public class Customer:Person
    {

        [StringLength(50, ErrorMessage = "FirstName should maximum 50 lenght.")]
        public override string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "LastName should maximum 50 lenght.")]
        public override string LastName { get; set; }


        [Required(ErrorMessage = "AddressesList is required.")]
        [MinLength(1, ErrorMessage = "AddressesList must be at least one address.")]
        public List<Address> AddressesList { get; set; }


        [Phone(ErrorMessage = "PhoneNumber accept only E.164 format.")]
        public string PhoneNumber { get; set; }


        [EmailAddress(ErrorMessage = "Email accept only email format.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Note cannot be empty, at least 1 note must be provided.")]
        [MinLength(1, ErrorMessage = "Notes must be at least one note.")]
        public List<string> Notes { get; set; }
        
        public decimal? TotalPurchasesAmount { get; set; }

        public int IdCustomer { get; set; } = 0;
    }
}
