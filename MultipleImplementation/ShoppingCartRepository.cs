using System;

namespace MultipleImplementation
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly Func<string, IShoppingCart> shoppingCart;
        public ShoppingCartRepository(Func<string, IShoppingCart> shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }

        public object GetCart()
        {
            return shoppingCart(CartSource.DB.ToString()).GetCart();
        }
    }
}
