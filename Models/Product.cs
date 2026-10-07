using System.ComponentModel.DataAnnotations;

namespace PennyEcommerce.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 100 characters")]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 100000.00, ErrorMessage = "Price must be between $0.01 and $100,000.00")]
    [DataType(DataType.Currency)]
    [Display(Name = "Unit Price ($)")]
    public decimal Price { get; set; }

    [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters")]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Stock quantity is required")]
    [Range(0, 10000, ErrorMessage = "Stock quantity must be between 0 and 10,000")]
    [Display(Name = "Stock Quantity")]
    public int StockQuantity { get; set; }
}
