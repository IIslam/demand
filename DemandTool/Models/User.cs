using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemandTool.Models
{
    public class User
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
    }
}