using Microsoft.AspNet.Identity;
using Online_Retail_Store.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace Online_Retail_Store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: Cart/AddToCart

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int ProductId)
        {
            var userId = User.Identity.GetUserId();
            //if (userId == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            // Check if the product already exists in the cart for this user
            var cartItem = db.CartItems.FirstOrDefault(c => c.ProductId == ProductId && c.UserId == userId);

            if (cartItem == null)
            {
                // Add new cart item
                cartItem = new CartItem
                {
                    ProductId = ProductId,
                    UserId = userId,
                    Quantity = 1
                };
                db.CartItems.Add(cartItem);
            }
            else
            {
                // Increment quantity if the item already exists
                cartItem.Quantity++;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }


        // GET: Cart/Index
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //if (userId == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            // Get cart items for the logged-in user
            var cartItems = db.CartItems
                                .Where(c => c.UserId == userId)
                                .Include(c => c.Product)
                                .ToList();

            return View(cartItems);
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart(int CartItemId)
        {
            var cartItem = db.CartItems.Find(CartItemId);
            if (cartItem != null)
            {
                db.CartItems.Remove(cartItem);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout()
        {
            var userId = User.Identity.GetUserId();  // Get the logged-in user's ID
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get the corresponding Customer for the logged-in user
            var customer = db.Customers.FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer == null)
            {
                return HttpNotFound("Customer not found. Please register as a customer.");
            }

            // Get the user's cart items from the database
            var cart = db.CartItems
                         .Where(c => c.UserId == userId)
                         .Include(c => c.Product)
                         .ToList();

            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index");  // If cart is empty, go back to cart
            }

            // Create a new order
            var order = new Order
            {
                CustomerId = customer.CustomerId,  // Associate the order with the customer
                OrderDate = DateTime.Now,
                TotalAmount = cart.Sum(item => item.Quantity * item.Product.Price), // Calculate total amount
                OrderDetails = cart.Select(item => new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                }).ToList()
            };

            db.Orders.Add(order);
            db.SaveChanges();

            // Clear the cart
            db.CartItems.RemoveRange(cart);
            db.SaveChanges();

            return RedirectToAction("OrderConfirmation", "Cart", new { id = order.OrderId });
        }


        public ActionResult OrderConfirmation(int id)
        {
            var order = db.Orders
                          .Include(o => o.OrderDetails.Select(od => od.Product))  // Include product details
                          .Include(o => o.Customer)  // Include customer details
                          .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
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


