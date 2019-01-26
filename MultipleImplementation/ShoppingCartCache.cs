namespace MultipleImplementation
{
    public class ShoppingCartCache : IShoppingCart
    {
        public object GetCart()
        {
            return "Cart loaded from cache.";
        }
    }
}
