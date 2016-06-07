using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShop.Models
{
    public class SearchResult
    {
        public int SanPhamID { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public Nullable<int> DanhGia { get; set; }
        public Nullable<byte> TrangThai { get; set; }
        public string Mota { get; set; }
        public int LoaiID { get; set; }
        public string NhaSanXuat { get; set; }
        public bool Deleted { get; set; }
        public int KhuyenMaiID { get; set; }
        public string BiDanh { get; set; }
        public Nullable<int> SoLanXem { get; set; }
        public int SanPhamHinhID { get; set; }
        public string TenHinh { get; set; }
        public byte ThuTuHienThi { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<SanPhamHinh> SanPhamHinhs { get; set; }
        public virtual ICollection<SanPhamSize> SanPhamSizes { get; set; }
    }
}