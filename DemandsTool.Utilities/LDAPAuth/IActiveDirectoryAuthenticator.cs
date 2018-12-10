using DemandsTool.Utilities.LDAPAuth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemandsTool.Utilities.LDAPAuth
{
    public interface IActiveDirectoryAuthenticator
    {
        bool IsAuthenticated(string domain, string username, string password);

        User GetUser();
    }
}