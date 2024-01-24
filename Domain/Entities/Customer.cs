using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        [EmailAddressAttribute]
        public string Email { get; set; }

        public IList<Address> Address { get; set; }

    }
}
