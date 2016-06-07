using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ShoeShop.Models;

namespace ShoeShop.Controllers
{
    public class AdminSiteController : Controller
    {
        private readonly ShoesDbContext db = new ShoesDbContext();

        #region IndexPage

        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        #endregion

        #region ViewAllSize

        /// <summary>
        ///     View lists of size
        /// </summary>
        /// <returns>List Size</returns>
        public ActionResult SizeManage()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            var listSize = db.Sizes.ToList();
            return View(listSize);
        }

        #endregion

        #region DeleteSize

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteSize(int id = 0)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            try
            {
                var size = db.Sizes.Find(id);

                if (size == null)
                {
                    return RedirectToAction("Index");
                }

                db.Sizes.Remove(size);
                db.SaveChanges();
                return RedirectToAction("SizeManage");
            }

            catch
            {
                object message = "Không xóa được do ràng buộc csdl ";
                return View("Error", message);
            }
        }

        #endregion

        #region ViewAllCategory

        /// <summary>
        /// </summary>
        /// <returns>List of category item</returns>
        public ActionResult CategoryManage()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            var cat = db.Loais.Where(result => !result.ChungLoaiID.HasValue).ToList();
            return View(cat);
        }

        #endregion

        #region DeleteCat

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCat(int id = 0)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            try
            {
                var cat = db.Loais.Find(id);
                if (cat == null)
                {
                    return RedirectToAction("CategoryManage");
                }
                db.Loais.Remove(cat);
                db.SaveChanges();
                return RedirectToAction("CategoryManage");
            }
            catch (Exception ex)
            {
                object mess = "Không xóa được do " + ex.Message;
                return View("Error", mess);
            }
        }

        #endregion

        #region ViewAllBrand

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ActionResult BrandManage()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            var cat = db.Loais
                .Select(p => new {p.ID, p.Ten, ChungLoai = p.Loai2.Ten, p.ChungLoaiID, p.BiDanh})
                .Where(result => result.ChungLoaiID.HasValue).ToList();
            return View(cat);
        }

        #endregion

        #region DeleteBrand

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteBrand(int id = 0)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            try
            {
                var cat = db.Loais.Find(id);

                if (cat == null)
                {
                    return RedirectToAction("BrandManage");
                }

                db.Loais.Remove(cat);
                db.SaveChanges();
                return RedirectToAction("BrandManage");
            }
            catch (Exception ex)
            {
                object mess = "Không xóa được do " + ex.Message;
                return View("Error", mess);
            }
        }

        #endregion

        #region ViewAllProduct

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductManage()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            var product = db.SanPhams.Include(s => s.Loai);
            return View(product);
        }

        #endregion

        #region ProductDetails

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ProductDetails(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return RedirectToAction("ProductManage");
            }

            var product = db.SanPhams.
                Include("Loai").
                Include("KhuyenMai").
                Include("SanPhamHinhs").
                Include("SanPhamSizes").
                Include("SanPhamSizes.Size").SingleOrDefault(result => result.SanPhamID == id);

            if (product == null)
            {
                return View("404");
            }

            return View(product);
        }

        #endregion

        #region DeleteProduct

        public ActionResult DeleteProduct(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            try
            {
                var countOrder =
                    db.SanPhamSizes.Count(result => result.SanPhamID == id && result.DonHangChiTiets.Any());

                if (countOrder > 0)
                {
                    throw new Exception("Không hủy được do có đơn đặt hàng");
                }

                var deleteProduct = db.SanPhams
                    .Include(s => s.SanPhamSizes)
                    .Include(i => i.SanPhamHinhs)
                    .SingleOrDefault(result => result.SanPhamID == id);
                db.SanPhamSizes.RemoveRange(deleteProduct.SanPhamSizes);
                db.SanPhamHinhs.RemoveRange(deleteProduct.SanPhamHinhs);

                foreach (var item in deleteProduct.SanPhamHinhs)
                {
                    var fileName = item.TenHinh;
                    var path = Server.MapPath("~/Photos/" + fileName);
                    var file = new FileInfo(path);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }

                db.SanPhams.Remove(deleteProduct);
                db.SaveChanges();
                return RedirectToAction("ProductManage");
            }

            catch (Exception ex)
            {
                object mess = "Không xóa được do " + ex.Message;
                return View("Error", mess);
            }
        }

        #endregion

        #region Login - Logout

        /// <summary>
        ///     Login page
        /// </summary>
        /// <returns>get login page</returns>
        public ActionResult Login()
        {
            return View();
        }

        ///// <summary>
        ///// Login page
        ///// </summary>
        ///// <returns>get login page</returns>
        [HttpPost]
        public ActionResult Login(string userName, string passWord)
        {
            var user =
                db.QuanTris.Single(result => result.Username.Equals(userName) && result.Password.Equals(passWord));
            if (user != null)
            {
                Session["user"] = new QuanTri {Username = userName, Level = 1};
                return View("Index", Session["user"]);
            }

            return View();
        }

        /// <summary>
        ///     session destroy
        /// </summary>
        /// <returns>login page</returns>
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        #endregion

        #region NewSize

        /// <summary>
        /// </summary>
        /// <returns>Return View for admin</returns>
        public ActionResult NewSize()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(new Size());
        }

        /// <summary>
        /// </summary>
        /// <param name="size">Compare size params post back from view the size from source</param>
        /// <returns>New Size</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewSize(Size size)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var sizeId = db.Sizes.Count(result => result.MaSize.Equals(size.MaSize));
            if (sizeId > 0) ModelState.AddModelError("MaSize", "Mã Size bị trùng");
            var sizeName = db.Sizes.Count(result => result.TenSize.Equals(size.TenSize));
            if (sizeName > 0) ModelState.AddModelError("TenSize", "Tên Size bị trùng");
            if (ModelState.IsValid)
            {
                try
                {
                    db.Sizes.Add(size);
                    db.SaveChanges();
                    TempData["Success_Mess"] = "<script>alert('Thêm size mới thành công')</script>";
                    return View(new Size());
                }
                catch (Exception ex)
                {
                    object message = "Không thêm được do" + ex.Message;
                    return View("Error", message);
                }
            }
            return View(new Size());
        }

        #endregion

        #region UpdateSize

        /// <summary>
        /// </summary>
        /// <param name="id">Compare id post back by view and compare with source if id not found return to 404 view</param>
        /// <returns></returns>
        public ActionResult EditSize(int id = 0)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var size = db.Sizes.Find(id);
            if (size == null)
            {
                return View("404");
            }
            return View(size);
        }

        /// <summary>
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSize(Size size)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var countSizeName = db.Sizes.Count(result => result.TenSize.Equals(size.TenSize));
            if (countSizeName > 0)
            {
                ModelState.AddModelError("TenSize", "Tên size bị trùng");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(size).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success_Mess"] = "<script>alert('Cập nhật thành công')</script>";
                    return View(size);
                }
                catch (Exception ex)
                {
                    TempData["Error_Mess"] = "<script>alert(Update không thành công do'" + ex.Message + "')</script>";
                    return View(size);
                }
            }
            return View(size);
        }

        #endregion

        #region NewCat

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ActionResult NewCat()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            return View(new Loai());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCat(Loai loai)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var cat = db.Loais.Count(result => result.Ten.Equals(loai.Ten));
            if (cat > 0) ModelState.AddModelError("Ten", "Tên chủng loại bị trùng");
            if (ModelState.IsValid)
            {
                try
                {
                    loai.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(loai.Ten);
                    db.Loais.Add(loai);
                    db.SaveChanges();
                    TempData["Success_Mess"] = "<script>alert('Thêm chủng loại mới thành công')</script>";
                    return View(new Loai());
                }

                catch (Exception ex)
                {
                    object message = "Không thêm được do" + ex.Message;
                    return View("Error", message);
                }
            }
            return View(new Loai());
        }

        #endregion

        #region UpdateCat

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditCat(int id = 0)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var cat = db.Loais.Find(id);

            if (cat == null)
            {
                return View("404");
            }

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCat(Loai loai)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var countCat = db.Loais.Count(result => result.Ten.Equals(loai.Ten));
            if (countCat > 0)
                ModelState.AddModelError("Ten", "Tên chủng loại bị trùng");
            if (ModelState.IsValid)
            {
                try
                {
                    loai.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(loai.Ten);
                    db.Entry(loai).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success_Mess"] = "<script>alert('Cập nhật thành công')</script>";
                    return View(loai);
                }
                catch (Exception ex)
                {
                    TempData["Error_Mess"] = "<script>alert(Update không thành công do'" + ex.Message + "')</script>";
                    return View(loai);
                }
            }
            return View(loai);
        }

        #endregion

        #region NewBrand

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ActionResult NewBrand()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.ChungLoaiID = new SelectList(db.Loais.Where(result => !result.ChungLoaiID.HasValue), "ID", "Ten");
            return View(new Loai());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewBrand(Loai loai)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.ChungLoaiID = new SelectList(db.Loais.Where(result => !result.ChungLoaiID.HasValue), "ID", "Ten");
            var countBrand = db.Loais.Count(result => result.Ten.Equals(loai.Ten));
            if (countBrand > 0)
                ModelState.AddModelError("Ten", "Tên loại bị trùng");
            if (ModelState.IsValid)
            {
                try
                {
                    var chungLoaiId = Convert.ToInt32(Request["ChungLoaiID"]);
                    loai.ChungLoaiID = chungLoaiId;
                    loai.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(loai.Ten);
                    db.Loais.Add(loai);
                    db.SaveChanges();
                    TempData["Success_Mess"] = "<script>alert('Thêm loại mới thành công')</script>";
                    return View(loai);
                }
                catch (Exception ex)
                {
                    TempData["Error_Mess"] = "<script>alert(Thêm không thành công do'" + ex.Message + "')</script>";
                    return View(loai);
                }
            }
            return View(loai);
        }

        #endregion

        #region UpdateBrand

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditBrand(int id = 0)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var brand = db.Loais.Find(id);

            if (brand == null)
            {
                return View("404");
            }

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand(Loai loai)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var countBrand = db.Loais.Count(result => result.Ten.Equals(loai.Ten));
            if (countBrand > 0)
                ModelState.AddModelError("Ten", "Tên loại bị trùng");
            if (ModelState.IsValid)
            {
                try
                {
                    var chungLoaiId = Convert.ToInt32(Request["ChungLoaiID"]);
                    loai.ChungLoaiID = chungLoaiId;
                    loai.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(loai.Ten);
                    db.Entry(loai).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success_Mess"] = "<script>alert('Cập nhật thành công')</script>";
                    return View(loai);
                }

                catch (Exception ex)
                {
                    TempData["Error_Mess"] = "<script>alert(Update không thành công do'" + ex.Message + "')</script>";
                    return View(loai);
                }
            }
            return View(loai);
        }

        #endregion

        #region NewProduct

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ActionResult NewProduct()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "Ten");
            ViewBag.LoaiID = new SelectList(db.Loais.Where(result => result.ChungLoaiID.HasValue), "ID", "Ten");
            ViewBag.Sizes = db.Sizes.ToList();

            ViewBag.TrangThai = new List<SelectListItem>
            {
                new SelectListItem {Value = "2", Text = "Bình thường"},
                new SelectListItem {Value = "1", Text = "Nổi bật"}
            };

            return View(new SanPham());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewProduct(SanPham product, int[] sizeID, HttpPostedFileBase[] files)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            var productId = db.SanPhams.Count(result => result.MaSanPham == product.MaSanPham);
            if (productId > 0) ModelState.AddModelError("MaSanPham", "Mã sản phẩm bị trùng");
            var productName = db.SanPhams.Count(result => result.TenSanPham == product.TenSanPham);
            if (productName > 0) ModelState.AddModelError("TenSanPham", "Tên sản phẩm bị trùng");
            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "Ten");
            ViewBag.LoaiID = new SelectList(db.Loais.Where(result => result.ChungLoaiID.HasValue), "ID", "Ten");
            ViewBag.Sizes = db.Sizes.ToList();

            ViewBag.TrangThai = new List<SelectListItem>
            {
                new SelectListItem {Value = "2", Text = "Bình thường"},
                new SelectListItem {Value = "1", Text = "Nổi bật"}
            };

            if (ModelState.IsValid)
            {
                try
                {
                    product.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(product.TenSanPham);
                    product.SoLanXem = 0;
                    product.DanhGia = 0;
                    product.Deleted = product.Deleted;
                    db.SanPhams.Add(product);

                    if (sizeID != null)
                    {
                        for (var i = 0; i < sizeID.Count(); i++)
                        {
                            var productSize = new SanPhamSize();
                            productSize.SanPhamID = productSize.SanPhamID;
                            productSize.SizeID = sizeID[i];
                            db.SanPhamSizes.Add(productSize);
                        }
                    }

                    db.SaveChanges();
                    var imgList = files.Where(p => p != null);

                    foreach (var imgUpload in imgList)
                    {
                        var img = new SanPhamHinh();
                        img.SanPhamID = product.SanPhamID;
                        img.TenHinh = imgUpload.FileName;
                        db.SanPhamHinhs.Add(img);
                        var path = Server.MapPath("~/Photos/" + imgUpload.FileName);
                        imgUpload.SaveAs(path);
                    }

                    TempData["Success_Mess"] = "<script>alert('Thêm sản phẩm mới thành công')</script>";
                    db.SaveChanges();
                    return View(product);
                }

                catch (Exception ex)
                {
                    TempData["Error_Mess"] = "<script>alert(Thêm không thành công do'" + ex.Message + "')</script>";
                    return View(product);
                }
            }
            return View(product);
        }

        #endregion

        #region EditProduct

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditProduct(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = db.SanPhams
                .Include(s => s.SanPhamSizes)
                .SingleOrDefault(p => p.SanPhamID == id);

            if (product == null)
            {
                object meessage = "Thông tin truy cập không tồn tại!";
                return View("Error", meessage);
            }

            ViewBag.LoaiID = new SelectList(db.Loais.Where(p => p.ChungLoaiID.HasValue), "ID", "Ten", product.LoaiID);
            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "Ten", product.KhuyenMaiID);

            var dsTrangThai = new[]
            {
                new {TrangThai = 2, Ten = "Bình thường"},
                new {TrangThai = 1, Ten = "Nổi bật"}
            };

            ViewBag.TrangThai = new SelectList(dsTrangThai, "TrangThai", "Ten", product.TrangThai);
            ViewBag.Sizes = db.Sizes.ToList();
            ViewBag.SizesChon = product.SanPhamSizes.Select(p => p.SizeID).ToArray();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(
            [Bind(Include = "SanPhamID,MaSanPham,TenSanPham,GiaBan,TrangThai,Mota,LoaiID,NhaSanXuat,KhuyenMaiID,Deleted"
                )] SanPham product, int[] sizeIDs)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cProduct = db.SanPhams
                        .Include(s => s.SanPhamSizes)
                        .Include("SanPhamSizes.Size")
                        .SingleOrDefault(p => p.SanPhamID == product.SanPhamID);
                    product.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(product.TenSanPham);
                    product.Deleted = product.Deleted;
                    TryUpdateModel(cProduct, string.Empty,
                        new[]
                        {
                            "SanPhamID", "MaSanPham", "TenSanPham", "GiaBan", "TrangThai", "Mota", "LoaiID",
                            "NhaSanXuat",
                            "KhuyenMaiID", "Deleted"
                        });
                    db.SaveChanges();
                    var sizesCurrent = cProduct.SanPhamSizes.ToList();
                    var sizesRemove = sizesCurrent.Where(p => !sizeIDs.Contains(p.SizeID));

                    foreach (var item in sizeIDs)
                    {
                        if (sizesCurrent.Find(p => p.SizeID == item) == null)
                            db.SanPhamSizes.Add(new SanPhamSize {SanPhamID = product.SanPhamID, SizeID = item});
                    }

                    db.SaveChanges();

                    var sb = new StringBuilder();

                    foreach (var item in sizesRemove)
                    {
                        var entity = db.DonHangChiTiets.FirstOrDefault(p => p.SanPhamSizeID == item.SanPhamSizeID);
                        if (entity == null)
                        {
                            db.SanPhamSizes.Remove(item);
                        }
                        else
                        {
                            sb.AppendFormat(", {0} (Tên size: {1})", item.SanPhamSizeID, item.Size.TenSize);
                        }
                    }

                    db.SaveChanges();

                    if (sb.Length > 0)
                    {
                        object tb = string.Format("Sản phẩm size ID:{0} Không gỡ bỏ được vì đã có chi tiết đặt hàng!",
                            sb.ToString().Remove(0, 2));
                        return View("Error", tb);
                    }

                    return RedirectToAction("ProductManage");
                }

                catch (Exception ex)
                {
                    object tb = string.Format("Cập nhật không thành công!", ex.Message);
                    return View("Error", tb);
                }
            }
            ViewBag.LoaiID = new SelectList(db.Loais.Where(p => p.ChungLoaiID.HasValue), "ID", "Ten", product.LoaiID);
            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "Ten", product.KhuyenMaiID);

            var listTrangThai = new[]
            {
                new {TrangThai = 2, Ten = "Bình thường"},
                new {TrangThai = 1, Ten = "Nổi bật"}
            };

            ViewBag.TrangThai = new SelectList(listTrangThai, "TrangThai", "Ten", product.TrangThai);
            ViewBag.Sizes = db.Sizes.ToList();
            ViewBag.SizesChon = sizeIDs;
            return View(product);
        }

        #endregion

        #region Img

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UploadImg(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var product = db.SanPhams
                .Include(s => s.SanPhamHinhs)
                .SingleOrDefault(p => p.SanPhamID == id);
            if (product == null)
            {
                object err = "Thông tin truy cập không tồn tại!";
                return View("Error", err);
            }
            return View(product);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">compare id of img with source</param>
        /// <param name="files">allow to upload file img</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImg(int id, HttpPostedFileBase[] files)
        {
            byte max = 0;
            var listImg = db.SanPhamHinhs.Where(p => p.SanPhamID == id).ToList();
            if (listImg.Count > 0)
                max = listImg.Max(p => p.ThuTuHienThi);
            var listFile = files.Where(p => p != null);
            foreach (var f in listFile)
            {
                var img = new SanPhamHinh();
                img.SanPhamID = id;
                img.TenHinh = f.FileName;
                img.ThuTuHienThi = ++max;
                db.SanPhamHinhs.Add(img);
                var path = Server.MapPath("~/Photos/" + f.FileName);
                f.SaveAs(path);
            }
            if (listFile.Any())
                db.SaveChanges();
            return RedirectToAction("UploadImg", new {id});
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sanPhamID"></param>
        /// <returns>To action Upload Img when user completed deleted img</returns>
        public ActionResult DeleteImg(int id, int? sanPhamID)
        {
            if (sanPhamID.HasValue)
            {
                try
                {
                    var img = db.SanPhamHinhs.Find(id);
                    if (img == null)
                        return RedirectToAction("ProductManage");
                    db.SanPhamHinhs.Remove(img);
                    var fileName = img.TenHinh;
                    var path = Server.MapPath("~/Photos/" + fileName);
                    var file = new FileInfo(path);

                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    db.SaveChanges();
                }

                catch (Exception ex)
                {
                    object mess = "Không xóa được do " + ex.Message;
                    return View("Error", mess);
                }
            }

            TempData["Success_Mess"] = "<script>alert('Xoá thành công')</script>";
            return Redirect("~/AdminSite/UploadImg/" + sanPhamID);
        }

        #endregion
    }
}