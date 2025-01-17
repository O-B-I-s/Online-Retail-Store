using Microsoft.AspNet.Identity;
using Online_Retail_Store.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Online_Retail_Store.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include("Customer").ToList();
            if (User.IsInRole("Manager"))
            {
                return View("AdminView", orders);
            }

            var userId = User.Identity.GetUserId();

            // Get the customer for the logged-in user
            var customer = db.Customers.FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer == null)
            {
                return HttpNotFound("Customer not found.");
            }
            if (!User.IsInRole("Manager"))
            {
                // Get orders for the logged-in customer
                orders = db.Orders
                           .Where(o => o.CustomerId == customer.CustomerId)
                           .Include(o => o.OrderDetails)
                           .Include(o => o.OrderDetails.Select(od => od.Product)) // Include product details
                           .ToList();
                return View("CustomerView", orders.ToList());
            }

            return View(orders);
        }

        // GET: Order/OrderDetails/5
        public ActionResult OrderDetails(int id)
        {
            // Find the order with the given id
            var order = db.Orders
                          .Include(o => o.OrderDetails.Select(od => od.Product)) // Include product details
                          .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return HttpNotFound("Order not found.");
            }

            // Return the order to the view
            return View(order);
        }


        [Authorize(Roles = "Manager")]
        public ActionResult UpdateStatus(int id, string newStatus)
        {
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            if (newStatus == "Pending" || newStatus == "Processing" || newStatus == "On Transit" || newStatus == "Delivered")
            {
                order.Status = newStatus;
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