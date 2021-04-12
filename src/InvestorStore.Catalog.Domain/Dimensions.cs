using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Catalog.Domain
{
    public sealed class Dimensions
    {
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }

        public Dimensions(decimal height, decimal width, decimal depth)
        {
            Height = height;
            Width = width;
            Depth = depth;
            
            Validate();
        }

        public void Validate()
        {
            AssertionConcern.ValidateIfLessThan(Height, 1, $"The field {nameof(Height)} needs to be at least 1");
            AssertionConcern.ValidateIfLessThan(Width, 1, $"The field {nameof(Width)} needs to be at least 1");
            AssertionConcern.ValidateIfLessThan(Depth, 1, $"The field {nameof(Depth)} needs to be at least 1");
        }
    }
}