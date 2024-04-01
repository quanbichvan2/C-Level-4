using Lab_8_2_Net_4.Models;

namespace Lab_8_2_Net_4.Helpers
{
    //Lớp này cung cấp một phương thức IsExits để kiểm tra xem một sản phẩm có tồn tại trong giỏ hàng không.
    public static class IsExitsProductInCartHelper
    {
        public static bool IsExits(int idProduct, List<CartItem> cartItems)
        {
            return cartItems.Find(p => p.ProductId == idProduct) != null;
        }
    }
}
