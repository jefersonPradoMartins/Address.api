using System.Collections;

namespace Address.Domain.Entities
{
    public sealed class Address : IEquatable<Address>
    {
        public Guid AddressId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Complements { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string Ibge { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }


        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="other"></param>
        /// <returns> <paramref name="true"/> if the specified atribures of object is equal to the current object; otherwise, false. </returns>
        public bool Equals(Address? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Street == other.Street
                && City == other.City
                && State == other.State
                && ZipCode == other.ZipCode
                && Complements == other.Complements
                && Neighborhood == other.Ibge
                && Ibge == other.Ibge;
        }
        public override bool Equals(object obj) => Equals(obj as Address);
        public override int GetHashCode() => (Street, City, State, ZipCode, Complements, Neighborhood, Ibge).GetHashCode();

        public override string ToString()
        {
            return $"Street: {this.Street}, City: {this.City}, State: {this.State}, ZipCode: {this.ZipCode}, Complements: {this.Complements}, Neighborhood: {this.Neighborhood}, Ibge: {this.Ibge}";
        }

    }
}



