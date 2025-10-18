using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using api.Contracts.Users.Request;
using api.Interfaces;

namespace api.Validators
{
    public class UserValidator : IUserValidator
    {
        public bool IsValidEmail(string email)
        {
            if (!MailAddress.TryCreate(email, out var addr)) return false;

            var host = addr.Host;
            if (!host.Contains('.')) return false;
            if (email.Length > 254) return false;

            return true;
        }

        public void ValidateCreateUser(CreateUserDto dto)
        {
            var email = dto.Email?.Trim();
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            if (!IsValidEmail(email))
                throw new ArgumentException("Email is invalid");


            var password = dto.Password;
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password field is epmty");
            if (password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters");
        }
    }
}