using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CustomerClassLibrary
{
    [Serializable]
     public class Customer:Person
    {

        [StringLength(50, ErrorMessage = "FirstName should maximum 50 lenght.")]
        [Column("first_name")]
        public override string FirstName { get; set; } = "";

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "LastName should maximum 50 lenght.")]
        [Column("last_name")]
        public override string LastName { get; set; }

        [AddressListAttribute]

        public List<Address> AddressesList { get; set; }


        [Phone(ErrorMessage = "PhoneNumber accept only E.164 format.")]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }


        [EmailAddress(ErrorMessage = "Email accept only email format.")]
        [Column("customer_email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Note cannot be empty, at least 1 note must be provided.")]
        [NotesListAttribute]
        [NotMapped]
        public List<string> Notes { get; set; }  = new List<string>();

        [Column("notes")]
        public string NotesString
        {
            get
            {
                if (Notes.Count > 0) return JsonConvert.SerializeObject(Notes);
                else return "";
            }
            set 
            {
                if (value != "") Notes= JsonConvert.DeserializeObject<List<string>>(value);
                else Notes = new List<string>();
            }
        }

        [Column("total_purchases_amount")]
        public decimal? TotalPurchasesAmount { get; set; }

        [Key]
        [Column("customer_id")]
        public int IdCustomer { get; set; } = 0;

        public Customer()
        {
            AddressesList = new List<Address>();
            //NotesString = "";
        }
    }
}
