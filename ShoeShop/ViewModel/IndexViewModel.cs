using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoeShop.Models;

namespace ShoeShop.ViewModel
{
    public class IndexViewModel
    {
        public IList<SanPham> Populars { get; set; }
        public IList<SanPham> Exams { get; set; }
        public IndexViewModel(IList<SanPham> Populars, IList<SanPham> Exams)
        {
            this.Populars = Populars;
            this.Exams = Exams;
        }

        public IndexViewModel()
        {
            // Do nothing
        }
    }
}