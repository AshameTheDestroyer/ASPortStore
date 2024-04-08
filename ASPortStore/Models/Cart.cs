namespace ASPortStore.Models;

public class Cart
{
    public List<CartLine> Lines { get; set; } = [];
    public decimal TotalPrice
    {
        get =>
            Lines.Aggregate(
                0m,
                (accumulator, line) => accumulator + line.Product.Price * line.Quantity
            );
    }
    public int TotalQuantity
    {
        get => Lines.Aggregate(0, (accumulator, line) => accumulator + line.Quantity);
    }

    public virtual void AddItem(Product product, int quantity)
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

    public virtual void RemoveLine(Product product) =>
        Lines.RemoveAll(line => line.Product.ProductID == product.ProductID);

    public decimal ComputeTotalValue() => Lines.Sum(line => line.Product.Price * line.Quantity);

    public virtual void Clear() => Lines.Clear();
}

public class CartLine
{
    public int CartLineID { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
}
