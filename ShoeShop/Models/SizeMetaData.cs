using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShoeShop.Models
{
    [MetadataTypeAttribute(typeof(Size.SizeMetaData))]
    public partial class Size
    {
        internal sealed class SizeMetaData
        {
            [Display(Name="Mã Size")]
            [Required(ErrorMessage="{0} không được để trống") ]
            [MinLength(1,ErrorMessage="{0} tối thiểu là {1} kí tự")]
            [MaxLength(10, ErrorMessage = "{0} tối đa là {1} kí tự")]
            public string MaSize { get; set; }
            [Display(Name = "Tên Size")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [MinLength(1, ErrorMessage = "{0} tối thiểu là {1} kí tự")]
            [MaxLength(10, ErrorMessage = "{0} tối đa là {1} kí tự")]
            public string TenSize { get; set; }
            [Display(Name = "Ghi Chú")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [MinLength(3, ErrorMessage = "{0} tối thiểu là {1} kí tự")]
            [MaxLength(50, ErrorMessage = "{0} tối đa là {1} kí tự")]
            public string GhiChu { get; set; }
        }
    }
}