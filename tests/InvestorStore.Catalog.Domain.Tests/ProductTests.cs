using System;
using InvestorStore.Core.DomainObjects;
using Xunit;

namespace InvestorStore.Catalog.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Validate_AssertionsShouldThrowExceptions()
        {
            var ex = Assert.Throws<DomainException>(() => 
                new Product(string.Empty, "Description", "Image", false, DateTimeOffset.Now, 100, Guid.NewGuid(), new Dimensions(1, 1, 1))
            );

            Assert.Equal("The field Name cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", string.Empty, "Image", false, DateTimeOffset.Now, 100, Guid.NewGuid(), new Dimensions(1, 1, 1))
            );

            Assert.Equal("The field Description cannot be empty", ex.Message);
            
            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", "Image", false, DateTimeOffset.Now, -1, Guid.NewGuid(), new Dimensions(1, 1, 1))
            );

            Assert.Equal("The field Price cannot be less than zero", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", "Image", false, DateTimeOffset.Now, 0, Guid.Empty, new Dimensions(1, 1, 1))
            );

            Assert.Equal("The field CategoryId cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", string.Empty, false, DateTimeOffset.Now, 0, Guid.NewGuid(), new Dimensions(1, 1, 1))
            );

            Assert.Equal("The field Image cannot be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", "Image", false, DateTimeOffset.Now, 0, Guid.NewGuid(), new Dimensions(0, 1, 1))
            );

            Assert.Equal("The field Height needs to be at least 1", ex.Message);
        }
    }
}