using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public Category(string name, string code)
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