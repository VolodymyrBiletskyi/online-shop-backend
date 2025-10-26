using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        public Cart? Cart { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public UserRole Role { get; set; } = UserRole.Customer;
        public DateTime? CreatedAt { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<UserAddress> Addres { get; set; } = new List<UserAddress>();
    }
    public enum UserRole
    {
        Customer = 0,
        Admin = 1
     }

}