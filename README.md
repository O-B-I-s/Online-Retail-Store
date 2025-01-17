# Online Retail Store

# Overview
The Online Retail Store is a web application built using ASP.NET MVC Framework. It allows customers to browse products, add items to their shopping cart, place orders, and track their order status. Admin users can manage orders, update their statuses, and oversee the retail store's operations.

---

# Features

# For Customers:
1. # User Registration and Login:
   - Customers can create an account and log in securely.

2. # Product Catalog:
   - Browse available products with details like price and description.

3. # Shopping Cart:
   - Add items to the cart, adjust quantities, and view the total price.

4. **Order Placement** :
   - Place orders and view order details.

5. **Order Tracking**:
   - Check the current status of orders (e.g., Pending, Processing, On Transit, Delivered).

---

### For Admins:
1. **Order Management**:
   - View and update the status of customer orders.

2. **Order Status Updates**:
   - Change the order status to "Pending," "Processing," "On Transit," or "Delivered."

3. **Product Management**:
   - Add, update, or remove products.

---

## Technologies Used

### Backend:
- ASP.NET MVC Framework
- Entity Framework

### Frontend:
- HTML5, CSS3, Bootstrap
- JavaScript, jQuery

### Database:
- Microsoft SQL Server


## Database Design

### Tables:
1. **Users**:
   - Stores user account details.

2. **Products**:
   - Stores information about products (e.g., name, price, description).

3. **Orders**:
   - Stores customer orders and their statuses.

4. **OrderDetails**:
   - Stores the details of each order (e.g., products and quantities).

---

## Order Workflow
1. Customers add products to their cart and place an order.
2. Orders are stored in the `Orders` table with the default status as `Pending`.
3. Admins can view orders and update their status:
   - **Pending**: The order has been placed but not yet processed.
   - **Processing**: The order is being prepared.
   - **On Transit**: The order is shipped.
   - **Delivered**: The order is delivered to the customer.


## Contact
For issues or feature requests, please contact:
- **Name**: Johnson Obioma
- **Email**: ifybioma@gmail.com

