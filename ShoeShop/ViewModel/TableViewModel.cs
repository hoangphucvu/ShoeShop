using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoeShop.Models;
namespace ShoeShop.ViewModel
{
    public class TableViewModel
    {
        public IList<SanPham> SanPhams { get; set; }
        public IList<SanPhamHinh> SanPhamHinhs { get; set; }
        public IList<Loai> Loais { get; set; }
        public CartModel Cart { get; set; }
        public TableViewModel(IList<SanPham> SanPhams, IList<SanPhamHinh> SanPhamHinhs, IList<Loai> Loais, CartModel Cart)
        {
            this.SanPhamHinhs = SanPhamHinhs;
            this.SanPhams = SanPhams;
            this.Loais = Loais;
            this.Cart = Cart;
        }
        public TableViewModel()
        {
            // Default
            SanPhams = new List<SanPham>();
            SanPhamHinhs = new List<SanPhamHinh>();
            Loais = new List<Loai>();
            Cart = new CartModel();
        }
    }
}