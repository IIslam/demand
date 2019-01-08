using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemandTool.Models
{
    public class UpdatePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}