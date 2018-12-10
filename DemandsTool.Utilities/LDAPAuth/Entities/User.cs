using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemandsTool.Utilities.LDAPAuth.Entities
{
    public class User
    {
        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        // NT
        public string FullName { get; set; }
        public string Username { get; set; }
        public string StaffId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Gender Gender { get; set; }
        public string Role { get; set; }
    }
}