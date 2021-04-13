using System;
using System.ComponentModel.DataAnnotations;

namespace InvestorStore.Catalog.Application.Dtos
{
    public class CategoryDto
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public int Code { get; set; }
    }
}