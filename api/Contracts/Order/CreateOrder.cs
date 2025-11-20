using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.Order
{
    public class CreateOrder
    {
        public Guid OrderAddressId { get; set; }
        public Guid? MyProperty { get; set; }
    }
}