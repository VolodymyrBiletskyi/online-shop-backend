using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.Users
{
    public class UpdateAddress
    {
        
        public string? Country { get; set; } 
        public string? City { get; set; } 
        public string? Street { get; set; } 
        public string? NumOfObject { get; set; } 
        public string? PostalCode { get; set; } 
        public bool? IsDefault { get; set; } = false;
    }
}