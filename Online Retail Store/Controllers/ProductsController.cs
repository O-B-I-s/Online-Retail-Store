using Newtonsoft.Json;
using Online_Retail_Store.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Online_Retail_Store.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var totalProducts = db.Products.Count();
            var totalOrders = db.Orders.Count();
            var totalCustomers = db.Customers.Count();

            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalCustomers = totalCustomers;

            // Data for product category distribution
            var categoryData = db.Categories.Select(c => new
            {
                c.Name,
                ProductCount = c.Products.Count()
            }).ToList();

            // Data for sales trends (last 7 days)
            var salesData = db.Orders
                        .AsEnumerable() // Switch to in-memory LINQ
                        .GroupBy(o => new
                        {
                            Date = o.OrderDate.Date // Apply the Date property here
                        })
                        .Select(g => new
                        {
                            Date = g.Key.Date,
                            TotalSales = g.Sum(o => o.TotalAmount)
                        })
                        .OrderBy(d => d.Date)
                        .ToList();

            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.CategoryData = categoryData;
            ViewBag.SalesData = salesData;

            // Pass serialized data to ViewBag
            ViewBag.CategoryDataJson = JsonConvert.SerializeObject(categoryData);
            ViewBag.SalesDataJson = JsonConvert.SerializeObject(salesData);

            var products = db.Products.Include(p => p.Category);
            if (User.IsInRole("Manager"))
                return View("AdminView", products);
            return View("CustomerView", products.ToList());
        }

        // GET: Products/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "ProductId,Name,Description,Price,Stock,CategoryId")] Product product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    try
                    {
                        // Log the upload details for debugging
                        var fileName = Path.GetFileName(upload.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        upload.SaveAs(filePath); // Save file to the server

                        // Log success
                        System.Diagnostics.Debug.WriteLine($"Image uploaded: {filePath}");
                        product.ImagePath = "~/Content/Images/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        // Log any exceptions
                        System.Diagnostics.Debug.WriteLine("Image upload failed: " + ex.Message);
                    }
                }
                else
                {
                    // Log if no file was uploaded
                    System.Diagnostics.Debug.WriteLine("No file uploaded.");
                    product.ImagePath = "~/Content/Images/images.png"; // Default image
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }



        // GET: Products/Edit/5
        [Authorize(Roles = "Manager")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Description,Price,Stock,CategoryId")] Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Save the new image if provided
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    imageFile.SaveAs(path);

                    // Update the image path
                    product.ImagePath = "~/Content/Images/" + fileName;
                }
                else
                {
                    // Retain the existing ImagePath
                    var existingProduct = db.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == product.ProductId);
                    if (existingProduct != null)
                    {
                        product.ImagePath = existingProduct.ImagePath;
                    }
                }

                // Mark the entity as modified and save changes
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }


        // GET: Products/Delete/5
        //[Authorize(Roles = "Manager")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                // Delete the image file if it exists
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    var imagePath = Server.MapPath(product.ImagePath);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
