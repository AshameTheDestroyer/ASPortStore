using System.Text.Json.Serialization;
using ASPortStore.Infrastructure;

namespace ASPortStore.Models;

public class SessionCart : Cart
{
    [JsonIgnore]
    public ISession? Session { get; set; }

    public static Cart GetCart(IServiceProvider serviceProvider)
    {
        ISession? session = serviceProvider
            .GetRequiredService<IHttpContextAccessor>()
            .HttpContext?.Session;
        SessionCart sessionCart = session?.GetJson<SessionCart>("Cart") ?? new();

        sessionCart.Session = session;
        return sessionCart;
    }

    public override void AddItem(Product product, int quantity)
    {
        base.AddItem(product, quantity);
        Session?.SetJson("Cart", this);
    }

    public override void RemoveLine(Product product)
    {
        base.RemoveLine(product);
        Session?.SetJson("Cart", this);
    }

    public override void Clear()
    {
        base.Clear();
        Session?.Remove("Cart");
    }
}
