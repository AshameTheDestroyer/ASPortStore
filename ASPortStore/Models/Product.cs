using System.ComponentModel.DataAnnotations.Schema;

namespace ASPortStore.Models;

public class Product
{
    public long? ProductID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
}