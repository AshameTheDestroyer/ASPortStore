namespace ASPortStore.Models;

public class Cart
{
    public List<CartLine> Lines { get; set; } = [];

    public void AddItem(Product product, int quantity)
    {
        CartLine? line = Lines
            .Where(product_ => product_.Product.ProductID == product.ProductID)
            .FirstOrDefault();

        if (line != null)
        {
            line.Quantity += quantity;
            return;
        }

        Lines.Add(new CartLine { Product = product, Quantity = quantity });
    }

    public void RemoveLine(Product product) =>
        Lines.RemoveAll(line => line.Product.ProductID == product.ProductID);

    public decimal ComputeTotalValue() => Lines.Sum(line => line.Product.Price * line.Quantity);

    public void Clear() => Lines.Clear();
}

public class CartLine
{
    public int CartLineID { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
}
