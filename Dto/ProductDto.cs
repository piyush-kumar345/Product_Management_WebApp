using System.ComponentModel.DataAnnotations;

namespace Product_Management_WebApp.Dto;

public class ProductDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Product name must be between 2 and 100 characters.")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, MinimumLength = 10,
        ErrorMessage = "Description must be between 10 and 500 characters.")]
    public string Description { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999.99",
        ErrorMessage = "Price must be between 0.01 and 999999999.99.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Color is required.")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Color must be between 2 and 50 characters.")]
    public string Color { get; set; } = string.Empty;
}
