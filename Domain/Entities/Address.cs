using System.Collections;

namespace Address.Domain.Entities
{
    public sealed class Address : IEquatable<Address>
    {
        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Complements { get; set; }
        public string Neighborhood { get; set; }
        public string Ibge { get; set; }


        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="other"></param>
        /// <returns> <paramref name="true"/> if the specified atribures of object is equal to the current object; otherwise, false. </returns>
        public bool Equals(Address? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Street == other.Street && City == other.City && State == other.State && ZipCode == other.ZipCode;
        }
        public override bool Equals(object obj) => Equals(obj as Address);
        public override int GetHashCode() => (Street, City, State, ZipCode).GetHashCode();

    }
}



