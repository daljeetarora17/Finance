using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDFinance.API.ViewModels
{
    public class GuardianVM
    {
        public int UserId { get; set; }
        public int GuardianId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNo { get; set; }
        public int AltPhoneNo { get; set; }
    }
}
