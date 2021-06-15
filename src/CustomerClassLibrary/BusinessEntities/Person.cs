using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary
{
    public abstract class Person
    {
        [StringLength(50, MinimumLength = 1)]
        public abstract string  FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, MinimumLength = 1)]
        public abstract string LastName { get; set; }
    }
}
