using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDFinance.API.Entities
{
    public class UserVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserType { get; set; }
        public int PhoneNo { get; set; }
    }
}
