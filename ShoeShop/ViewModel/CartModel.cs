using System.Collections.Generic;
using System.Linq;

namespace ShoeShop.ViewModel
{
    public class CartModel
    {
        public List<CartItem> Items { get; } = new List<CartItem>();

        public void Clear()
        {
            Items.Clear();
        }

        public void Add(CartItem item)
        {
            var cartItem = Items.Find(product => product.SanPhamSizeID == item.SanPhamSizeID);
            if (cartItem == null)
                Items.Add(item);
            else
                cartItem.SoLuong += item.SoLuong;
        }

        public decimal Total()
        {
            var total = Items.Sum(product => (product.SoLuong*product.SanPham.GiaBan));
            return total;
        }

        public void Update(int SanPhamSizeId, int quantity)
        {
            var item = Items.Find(product => product.SanPhamSizeID == SanPhamSizeId);
            item.SoLuong = quantity;
        }

        public void Remove(int SanPhamSizeId)
        {
            var item = Items.Find(product => product.SanPhamSizeID == SanPhamSizeId);
            Items.Remove(item);
        }
    }
}