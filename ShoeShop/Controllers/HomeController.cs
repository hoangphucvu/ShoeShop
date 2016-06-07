using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using ShoeShop.Models;
using ShoeShop.ViewModel;
using IndexViewModel = ShoeShop.ViewModel.IndexViewModel;

namespace ShoeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShoesDbContext db = new ShoesDbContext();

        #region Index

        /// <summary>
        ///     Show product at index page
        ///     Pass 2 parameters to IndexViewModel to get item
        /// </summary>
        /// <returns>4 new product and 8 most view product  </returns>
        [Route("trang-chu")]
        [Route("")]
        public ActionResult Index()
        {
            var e = db.SanPhams.Include("SanPhamHinhs").OrderBy(result => result.SoLanXem).Take(4).ToList();
            var p = db.SanPhams.Include("SanPhamHinhs").OrderByDescending(result => result.SoLanXem).Take(8).ToList();
            var model = new IndexViewModel(p, e);
            return View(model);
        }

        #endregion

        #region Contact

        /// <summary>
        ///     Contact page to show contact information
        /// </summary>
        /// <returns>Contact page</returns>
        [Route("lien-he.html")]
        public ViewResult Contact()
        {
            return View();
        }

        #endregion

        #region About

        /// <summary>
        ///     Rule of page
        /// </summary>
        /// <returns>About page</returns>
        [Route("quy-dinh-chung.html")]
        public ViewResult About()
        {
            return View();
        }

        #endregion

        #region Category

        /// <summary>
        ///     Get Sub_Product by Name and paging
        ///     Compare LoaiID form resource with id params
        /// </summary>
        /// <param name="id">id of product</param>
        /// <param name="name">name of product(use BiDanh name)</param>
        /// <param name="page">default params</param>
        /// <param name="pageSize">determine how many product to show at once</param>
        /// <returns>Sub_Product by Name and paging  </returns>
        [Route("chung-loai/{name}-{id}/{page?}/{pageSize?}")]
        public ActionResult Category(int id = 0, string name = "", int page = 1, int pageSize = 12)
        {
            var item = db.SanPhams.Where(result => result.LoaiID == id).ToList();
            var category = db.Loais.SingleOrDefault(result => result.ChungLoaiID.HasValue && result.ID == id);
            ViewBag.Title = "Chủng loại " + category.Ten;
            ViewBag.Name = category.Ten;
            var proPag = new PagedList<SanPham>(item, page, pageSize);
            return View(proPag);
        }

        #endregion

        #region Parents_Product

        /// <summary>
        ///     Get Parents_Product by Name and paging
        ///     Compare LoaiID form resource with id param
        ///     this.db.SanPhams.Where(t => f_id.Contains(t.LoaiID)).ToList() mean
        ///     select xxx from SanPham where Exists SELECT id from Loai
        /// </summary>
        /// <param name="id">id of product</param>
        /// <param name="name">name of product(use BiDanh name)</param>
        /// <param name="page">default params</param>
        /// <param name="pageSize">determine how many product to show at once</param>
        /// <returns>Parents_Product by Name and paging  </returns>
        [Route("danh-muc-san-pham/{name}-{id}/{page?}/{pageSize?}")]
        public ActionResult F_Category(int id, string name = "", int page = 1, int pageSize = 4)
        {
            var fId = db.Loais.Where(result => result.ChungLoaiID == id).Select(result => result.ID);
            var item = db.SanPhams.Where(t => fId.Contains(t.LoaiID)).ToList();
            var fCat = new PagedList<SanPham>(item, page, pageSize);
            var fName = db.Loais.SingleOrDefault(result => !result.ChungLoaiID.HasValue && result.ID == id).Ten;
            ViewBag.Title = "Danh Mục " + fName;
            return View(fCat);
        }

        #endregion

        #region Details

        /// <summary>
        ///     Show details of product
        ///     ViewBag.RelatedProduct get related product by LoaiID except the current entities
        /// </summary>
        /// <param name="id">ID of product</param>
        /// <param name="name">BiDanh of product</param>
        /// <returns>Details of product and related product</returns>
        [Route("san-pham/{name}-{id}.html")]
        public ActionResult Details(int id = 0, string name = "")
        {
            var viewModel = db.SanPhams.Find(id);
            if (id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

            viewModel.SoLanXem++;
            db.SaveChanges();

            var productDetails = db.SanPhams
                .Include("Loai")
                .Include("KhuyenMai")
                .Include("SanPhamHinhs")
                .Include("SanPhamSizes")
                .Include("SanPhamSizes.Size")
                .SingleOrDefault(item => item.SanPhamID == id);

            if (productDetails == null)
            {
                return View("404");
            }

            var exceptedEntities = db.SanPhams.Where(result => result.MaSanPham == viewModel.MaSanPham);
            ViewBag.RelatedProduct =
                db.SanPhams.Where(item => item.LoaiID == viewModel.LoaiID).Except(exceptedEntities).ToList();
            return View(productDetails);
        }

        #endregion

        #region Search

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("tim-kiem/ketqua")]
        public ActionResult Search(string ketqua)
        {
            var item = db.SanPhams.
                Where(product => product.TenSanPham.Contains(ketqua)).
                Include("SanPhamHinhs").ToList();

            //List<SearchResult> results = new List<SearchResult>();
            //foreach (var i in item)
            //{
            //    results.Add(new SearchResult{
            //        SanPhamID = i.SanPhamID,
            //        MaSanPham = i.MaSanPham,
            //        TenSanPham = i.TenSanPham,
            //        BiDanh=i.BiDanh,
            //        GiaBan=i.GiaBan,
            //        LoaiID=i.LoaiID,
            //        Mota=i.Mota,
            //        NhaSanXuat=i.NhaSanXuat,
            //        TenHinh =i.SanPhamHinhs.First().TenHinh,

            //    });
            //
            //return Json(results, JsonRequestBehavior.AllowGet);
            //}
            ViewBag.Title = "Kết quả tìm kiếm " + ketqua;
            var countResult = db.SanPhams.
                Where(product => product.TenSanPham.Contains(ketqua)).Count();
            ViewBag.Count = "Có " + countResult + " kết quả tương ứng";
            return View(item);
        }

        #endregion

        #region GetCart

        // GET: Cart
        /// <summary>
        ///     Create new shopping cart if session cart is null
        ///     or else return the item in the shopping cart
        /// </summary>
        /// <returns>New Shopping cart</returns>
        private CartModel GetCart()
        {
            var cart = Session["Cart"] as CartModel;
            if (cart == null)
            {
                cart = new CartModel();
                Session["Cart"] = cart;
            }

            return cart;
        }

        #endregion

        #region Menu-Partial

        /// <summary>
        ///     Show name of Parents product and sub product
        /// </summary>
        /// <returns>All information of Parents product and sub product</returns>
        [ChildActionOnly]
        [Route("menu.html")]
        public PartialViewResult _MenuPartial()
        {
            ViewBag.Cart = GetCart();
            var cart = GetCart();
            ViewBag.Menu = db.Loais.Where(result => !result.ChungLoaiID.HasValue).ToList();

            ViewBag.Img = new SanPhamHinh();
            if (cart.Items.Count > 0)
            {
                var id = cart.Items[0].SanPham.SanPhamID;
                ViewBag.Img = db.SanPhamHinhs.Select(p => p.SanPhamID == id).FirstOrDefault();
            }

            var item = new TableViewModel();
            return PartialView(item);
        }

        /// <summary>
        ///     Load menu when user buy something
        /// </summary>
        /// <returns>Ajax call to partial menu</returns>
        [Route("load-menu.html")]
        public PartialViewResult _MenuLoad()
        {
            ViewBag.Cart = GetCart();
            var cart = GetCart();
            var item = new TableViewModel();
            ViewBag.Img = new SanPhamHinh();
            if (cart.Items.Count > 0)
            {
                var id = cart.Items[0].SanPham.SanPhamID;
                ViewBag.Img = db.SanPhamHinhs.Select(p => p.SanPhamID == id).FirstOrDefault();
            }

            return PartialView(item);
        }

        #endregion
    }
}