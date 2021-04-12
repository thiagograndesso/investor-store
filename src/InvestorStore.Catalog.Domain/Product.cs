using System;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public bool IsActive { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public decimal Price { get; private set; }
        public int AmountInStock { get; private set; }
        public Category Category { get; private set; }
        public Dimensions Dimensions { get; private set; }
        public Guid CategoryId { get; private set; }

        public Product(string name, string description, string image, bool isActive, DateTimeOffset createdAt, decimal price, Guid categoryId, Dimensions dimensions)
        {
            Name = name;
            Description = description;
            Image = image;
            IsActive = isActive;
            CreatedAt = createdAt;
            Price = price;
            CategoryId = categoryId;
            Dimensions = dimensions;
            
            Validate();
        }

        public void Activate() => IsActive = true;
        
        public void Deactivate() => IsActive = true;

        public void UpdateCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }
        
        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void DebitStock(int amount)
        {
            if (amount < 0)
            {
                amount *= -1;
            }

            if (!ContainsStock(amount))
            {
                throw new DomainException("Insufficient stock");
            }

            AmountInStock -= amount;
        }
        
        public void RefillStock(int amount)
        {
            AmountInStock += amount;
        } 
        
        public bool ContainsStock(int amount)
        {
            return AmountInStock >= amount;
        }

        public void Validate()
        {
             AssertionConcern.ThrowIfEmpty(Name, $"The field {nameof(Name)} cannot be empty");
             AssertionConcern.ThrowIfEmpty(Description, $"The field {nameof(Description)} cannot be empty");
             AssertionConcern.ThrowIfEmpty(Image, $"The field {nameof(Image)} cannot be empty");
             AssertionConcern.ThrowIfEqual(CategoryId, Guid.Empty, $"The field {nameof(CategoryId)} cannot be empty");
             AssertionConcern.ThrowIfLessThan(Price, 0, $"The field {nameof(Price)} cannot be less than zero");
        }
    }
}