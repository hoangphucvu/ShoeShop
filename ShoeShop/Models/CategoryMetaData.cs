using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShoeShop.Models
{
    [MetadataTypeAttribute(typeof(Loai.CategoryMetaData))]
    public partial class Loai
    {
        internal sealed class CategoryMetaData{
            [Display(Name="Tên chủng loại")]
            [Required(ErrorMessage="{0} không được để trống") ]
            //[RegularExpression("^[D]+$", ErrorMessage = "Vui lòng nhập chữ")]
            [MinLength(1,ErrorMessage="{0} tối thiểu là {1} kí tự")]
            [MaxLength(20, ErrorMessage = "{0} tối đa là {1} kí tự")]
            public string Ten { get; set; }
            [Display(Name="Tên loại")]
            public Nullable<int> ChungLoaiID { get; set; }
          
        }
    }
}