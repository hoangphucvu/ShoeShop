using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShoeShop.Models
{
    [MetadataTypeAttribute(typeof(Order.OrderMetaData))]
    public partial class Order
    {
        internal sealed class OrderMetaData
        {
            [Display(Name = "Tên khách hàng")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TenKhachHang { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string DiaChi { get; set; }
            [Display(Name = "Số Điện Thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [MinLength(10, ErrorMessage = "{0} tối thiểu là {1} số")]
            [MaxLength(15, ErrorMessage = "{0} tối đa là {1} số")]
            public string SoDienThoai { get; set; }
            [Display(Name = "Ghi Chú")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [MinLength(3, ErrorMessage = "{0} tối thiểu là {1} kí tự")]
            [MaxLength(50, ErrorMessage = "{0} tối đa là {1} kí tự")]
            public string GhiChu { get; set; }
        }
    }
}