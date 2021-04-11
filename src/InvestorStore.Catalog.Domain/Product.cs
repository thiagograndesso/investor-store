using System;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; }
        public string Description { get; private set; }
        public string Image { get; }
        public bool IsActive { get; private set; }
        public DateTimeOffset CreatedAt { get; }
        public decimal Price { get; }
        
        public int AmountInStock { get; private set; }
        
        public Category Category { get; private set; }
        
        public Guid CategoryId { get; private set; }

        public Product(string name, string description, string image, bool isActive, DateTimeOffset createdAt, decimal price, Guid categoryId)
        {
            Name = name;
            Description = description;
            Image = image;
            IsActive = isActive;
            CreatedAt = createdAt;
            Price = price;
            CategoryId = categoryId;
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
             
        }
    }

    public class Category : Entity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public Category(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }
    }
}