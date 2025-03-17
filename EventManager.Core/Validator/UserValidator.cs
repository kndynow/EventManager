using EventManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Validator
{
    public interface IUserValidator
    {
        bool IsValidPassword(string password);
        bool IsValidUsername(string userName);
    }
    public class UserValidator : IUserValidator
    {
        public bool IsValidUsername(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return false;

            if (userName.Length < 3 || userName.Length > 16)
                return false;

            // Check if username doesn't start or end with a space
            if (userName.Contains(' '))
                return false;

            return true;
        }

        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < 6 || password.Length > 24)
                return false;

            if (password.Contains(' '))
                return false;

            return true;
        }
    }
}