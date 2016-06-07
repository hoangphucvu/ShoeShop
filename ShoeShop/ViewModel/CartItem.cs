using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoeShop.Models;
namespace ShoeShop.ViewModel
{
    public class CartItem
    {
        public int SanPhamSizeID { get; set; }
        public SanPham SanPham { get; set; }
        public Size Size { get; set; }
        public int SoLuong { get; set; }
        public SanPhamHinh SanPhamHinh { get; set; }
        public CartItem()
        {

        }
        public CartItem(int SanPhamSizeID, SanPham SanPham, Size Size, int SoLuong)
        {
            this.SanPhamSizeID = SanPhamSizeID;
            this.SanPham = SanPham;
            this.Size = Size;
            this.SoLuong = SoLuong;
        }
    }
}