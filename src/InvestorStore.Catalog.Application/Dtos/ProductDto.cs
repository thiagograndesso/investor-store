using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestorStore.Catalog.Application.Dtos
{
    public class ProductDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} needs to have the minimum value of {1}")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public int InventoryAmount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} needs to have the minimum value of {1}")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public int Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} needs to have the minimum value of {1}")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public int Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} needs to have the minimum value of {1}")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public int Depth { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}