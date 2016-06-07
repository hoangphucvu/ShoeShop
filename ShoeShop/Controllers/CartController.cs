using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using ShoeShop.Models;
using ShoeShop.ViewModel;

namespace ShoeShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoesDbContext db = new ShoesDbContext();

        #region Index

        /// <summary>
        ///     If shopping cart is empty return to index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var cart = Session["Cart"] as CartModel;

            if (cart == null || cart.Items.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(cart);
        }

        #endregion

        #region AddToCart

        /// <summary>
        ///     Add product to cart
        /// </summary>
        /// <param name="sanPhamSizeId">Compare product size id with resource</param>
        /// <param name="quantity">Get product quantity</param>
        /// <returns>Info of cart</returns>
        [Route("gio-hang/them-san-pham/{sanPhamSizeId}/{quantity}")]
        [HttpPost]
        public RedirectToRouteResult AddToCart(int sanPhamSizeId, int quantity = 1)
        {
            var productSize = db.SanPhamSizes
                .Include("SanPham")
                .Include("Size")
                .Include("SanPham.KhuyenMai")
                .SingleOrDefault(product => product.SanPhamSizeID == sanPhamSizeId);
            var cart = GetCart();
            var cartItem = new CartItem(sanPhamSizeId, productSize.SanPham, productSize.Size, quantity);
            cart.Add(cartItem);
            return RedirectToAction("Index");
        }

        #endregion

        #region Update Cart

        /// <summary>
        ///     Update product
        /// </summary>
        /// <param name="sanPhamSizeId">Compare product size id with resource</param>
        /// <param name="quantity">Get product quantity</param>
        /// <returns>Info of cart</returns>
        [Route("gio-hang/cap-nhat-gio-hang/{sanPhamSizeId}/{quantity}")]
        [HttpPost]
        public RedirectToRouteResult UpdateCart(int sanPhamSizeID, int quantity)
        {
            var cart = GetCart();
            cart.Update(sanPhamSizeID, quantity);
            return RedirectToAction("Index");
        }

        #endregion

        #region RemoveFromCart

        /// <summary>
        ///     Remove product from shopping cart
        /// </summary>
        /// <param name="sanPhamSizeID"></param>
        /// <returns></returns>
        [Route("gio-hang/xoa-san-pham/{sanPhamSizeId}")]
        [HttpPost]
        public ActionResult RemoveFromCart(int sanPhamSizeID)
        {
            var cart = GetCart();
            cart.Remove(sanPhamSizeID);
            return RedirectToAction("Index");
        }

        #endregion

        #region Checkout step 1

        [Route("gio-hang/thanh-toan-buoc-1.html")]
        public PartialViewResult CheckOutStepOne()
        {
            ViewBag.Cart = GetCart();
            var item = new TableViewModel();
            return PartialView(item);
        }

        #endregion

        #region Checkout step 1 partial

        [Route("gio-hang/chi-tiet-gio-hang.html")]
        public PartialViewResult CatDetails()
        {
            ViewBag.Cart = GetCart();
            var item = new TableViewModel();
            return PartialView(item);
        }

        #endregion

        #region Completed

        /// <summary>
        /// </summary>
        /// <returns>Show the message to user that they completed their transaction</returns>
        [Route("gio-hang/hoan-tat-thanh-toan.html")]
        public ViewResult Completed()
        {
            return View();
        }

        #endregion

        #region GetCart

        /// <summary>
        ///     Create new shopping cart
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

        #region Checkout Step 2

        /// <summary>
        ///     Process to checkout step 2 where user input information
        /// </summary>
        /// <returns></returns>
        [Route("gio-hang/thanh-toan-buoc-2.html")]
        public ActionResult CheckoutStepTwo()
        {
            var cart = Session["Cart"] as CartModel;

            if (cart == null || cart.Items.Count() ==0)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Checkout";
            ViewBag.Message = "Nhập Thông tin đặt hàng!";
            return View();
        }

        [HttpPost]
        public ActionResult CheckoutStepTwo_Post(DonHang order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var transac = new TransactionScope())
                    {
                        var cart = GetCart();
                        order.NgayDatHang = DateTime.Now;
                        order.UserId = null;
                        order.DaGiaoHang = false;
                        db.DonHangs.Add(order);
                        db.SaveChanges();

                        foreach (var item in cart.Items)
                        {
                            var details = new DonHangChiTiet();
                            details.DonHangID = order.DonHangID;
                            details.SanPhamSizeID = item.SanPhamSizeID;
                            details.SoLuong = item.SoLuong;
                            details.DonGia = item.SanPham.GiaBan;
                            details.ThanhTien = item.SoLuong*item.SanPham.GiaBan;
                            db.DonHangChiTiets.Add(details);
                        }

                        db.SaveChanges();
                        cart.Clear();
                        transac.Complete();
                        ViewBag.Message = "Hoàn tất - Thông tin đặt hàng!";
                        return RedirectToAction("Completed");
                    }
                }

                catch
                {
                    object mess = "Ghi thông tin đặt hàng thất bại!";
                    return View("Error", mess);
                }
            }

            return View(order);
        }

        #endregion
    }
}