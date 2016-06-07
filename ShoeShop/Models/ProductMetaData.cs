using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShoeShop.Models
{
    [MetadataTypeAttribute(typeof(Product.ProductMetaData))]
    public partial class Product
    {
        internal sealed class ProductMetaData
        {
            [Display(Name="Sản Phẩm ID")]
            public int SanPhamID { get; set; }
            [Display(Name = "Mã Sản Phẩm")]
            public string MaSanPham { get; set; }
            [Display(Name = "Tên Sản Phẩm ")]
            public string TenSanPham { get; set; }
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            [Display(Name = "Giá Bán")]
            public Nullable<decimal> GiaBan { get; set; }
            [Display(Name = "Chi Tiết")]
            public string Mota { get; set; }
            [Display(Name = "Nhà Sản Xuất")]
            public string NhaSanXuat { get; set; }
            [Display(Name = "Khuyến Mãi")]
            public Nullable<int> KhuyenMaiID { get; set; }
            [Display(Name = "Bí Danh")]
            public string BiDanh { get; set; }
            [Display(Name = "Số Lần Xem")]
            public Nullable<int> SoLanXem { get; set; }
        }

    }
}