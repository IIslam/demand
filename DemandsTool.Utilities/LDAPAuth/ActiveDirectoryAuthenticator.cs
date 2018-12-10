using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using DemandsTool.Utilities.LDAPAuth.Entities;
namespace DemandsTool.Utilities.LDAPAuth
{
    public class ActiveDirectoryAuthenticator
    {
        private string _path;
        private SearchResult _result;
        public ActiveDirectoryAuthenticator(string path)
        {
            _path = path;
        }
        public User GetUser()
        {
            string _username = GetProperty(_result, "SAMAccountName");
            string _mobile = GetProperty(_result, "mobile");
            string _staffId = GetProperty(_result, "employeeid");
            string _email = GetProperty(_result, "mail");
            Gender _gender = GetProperty(_result, "gwegender") == "F" ? Gender.Female : Gender.Male;
            string _fullName = GetProperty(_result, "fullname");
            var member_of = _result.Properties["memberof"];
            string _role = string.Empty;
            foreach (var item in member_of)
            {
                if (item.ToString().Contains("VF UK-EG SmartWallet Users"))
                {
                    _role = "user";
                    break;
                }
                if (item.ToString().Contains("VF UK-EG SmartWallet Admins"))
                {
                    _role = "admin";
                    break;
                }
                if (item.ToString().Contains("VF UK-EG SmartWallet TMs"))
                {
                    _role = "leader";
                    break;
                }
            }
            return new User() { Username = _username, Mobile = _mobile, StaffId = _staffId, Email = _email, Gender = _gender, Role = _role, FullName = _fullName };

        }

        public bool IsAuthenticated(string domain, string username, string password)
        {
            if (Regex.IsMatch(username, "[-;|&()+:^$#='*%~`]") || Regex.IsMatch(password, "[;]"))
            {
                return false;
            }
            string fullUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, fullUsername, password);
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                SearchResult result = search.FindOne();
                if (null == result)
                    return false;
                // Update the new path to the user in the directory
                _path = result.Path;
                _result = result;
            }
            catch (Exception ex)
            {
                //throw new Exception("Error authenticating user. " + ex.Message);
                return false;
            }
            return true;

        }

        private static string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
