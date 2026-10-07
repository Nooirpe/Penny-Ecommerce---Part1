using Microsoft.AspNetCore.Mvc;
using PennyEcommerce.Models;

namespace PennyEcommerce.Controllers;

public class ProductController : Controller
{
    // In-memory data store for demonstration and homework testing
    private static readonly List<Product> _products = new()
    {
        new Product 
        { 
            Id = 1, 
            Name = "Mechanical Gaming Keyboard", 
            Price = 89.99m, 
            Description = "RGB backlit mechanical keyboard with hot-swappable switches", 
            StockQuantity = 25 
        },
        new Product 
        { 
            Id = 2, 
            Name = "Ergonomic Wireless Mouse", 
            Price = 34.50m, 
            Description = "Rechargeable 2.4GHz wireless mouse with adjustable DPI", 
            StockQuantity = 50 
        },
        new Product 
        { 
            Id = 3, 
            Name = "7-in-1 USB-C Hub", 
            Price = 29.99m, 
            Description = "Multi-port aluminum adapter with 4K HDMI and 100W PD charging", 
            StockQuantity = 80 
        },
        new Product 
        { 
            Id = 4, 
            Name = "Noise-Cancelling Headphones", 
            Price = 149.00m, 
            Description = "Wireless Bluetooth over-ear headphones with 30-hour battery life", 
            StockQuantity = 15 
        }
    };

    // GET: /Product or /Product/Index
    public IActionResult Index()
    {
        ViewData["Title"] = "Product Catalog";
        ViewData["TotalCount"] = _products.Count;
        ViewData["StoreNotice"] = "Welcome to PennyEcommerce! Manage your inventory below.";

        return View(_products);
    }

    // GET: /Product/Details/1
    public IActionResult Details(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"Details - {product.Name}";
        ViewData["Badge"] = product.StockQuantity > 0 ? "In Stock" : "Out of Stock";

        return View(product);
    }

    // GET: /Product/Create
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Add New Product";
        ViewData["Instruction"] = "Fill in the product details below. All fields marked with * are required.";

        return View(new Product());
    }

    // POST: /Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Add New Product";
            ViewData["Instruction"] = "Please correct the errors in the form.";
            return View(product);
        }

        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);
        TempData["SuccessMessage"] = $"Product '{product.Name}' added successfully!";

        return RedirectToAction(nameof(Index));
    }

    // GET: /Product/Edit/1
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"Edit Product - {product.Name}";
        ViewData["Instruction"] = "Update the product information and click Save Changes.";

        return View(product);
    }

    // POST: /Product/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewData["Title"] = $"Edit Product - {product.Name}";
            ViewData["Instruction"] = "Please correct the errors in the form.";
            return View(product);
        }

        existing.Name = product.Name;
        existing.Price = product.Price;
        existing.Description = product.Description;
        existing.StockQuantity = product.StockQuantity;

        TempData["SuccessMessage"] = $"Product '{existing.Name}' updated successfully!";

        return RedirectToAction(nameof(Index));
    }

    // GET: /Product/Delete/1
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"Delete Product - {product.Name}";
        ViewData["WarningMessage"] = "Are you sure you want to permanently delete this product? This action cannot be undone.";

        return View(product);
    }

    // POST: /Product/Delete/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
            TempData["SuccessMessage"] = $"Product '{product.Name}' was deleted.";
        }

        return RedirectToAction(nameof(Index));
    }
}
