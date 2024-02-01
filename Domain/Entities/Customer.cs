using System.ComponentModel.DataAnnotations;

namespace Address.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<Address> Address { get; set; }
    }
}
