using System.Collections.Generic;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }
        
        // EF relationship
        public ICollection<Product> Products { get; set; }

        protected Category() { }
        
        public Category(string name, int code)
        {
            Name = name;
            Code = code;
            
            Validate();
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            AssertionConcern.ThrowIfEmpty(Name, $"The field {nameof(Name)} cannot be empty");
            AssertionConcern.ThrowIfEqual(Code, 0, $"The field {nameof(Code)} cannot be zero");
        }
    }
}