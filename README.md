# Llaguno_Daryl_ShoppingCartActivity

Daryl James M. Llaguno

Shopping Cart System (with the use of Get and Set accessors)

A console-based shopping cart application built in C# that demonstrates encapsulation through properties with get and set accessors. The system features product browsing, cart management, payment validation, and order history tracking.

Key Programming Concept: Properties with Get and Set Accessors

All class data is properly encapsulated using auto-implemented properties ({ get; set; }), ensuring controlled access to object state:

 • Product class: Id, Name, Price, RemainingStock, Category
 • Cartitem class: Product, Quantity, Subtotal
 • Order class: ReceiptNumber, OrderDate, Items, GrandTotal, Discount, FinalTotal, Payment, Change

Features:

Part 1: Basic Features

 • Browse products with stock display
   — View all available items with live stock counts
 • Add items to cart with quantity selection
  — Select products and specify how many to purchase
 • Automatic stock deduction 
  — Inventory updates immediately when items are added to cart
 • Receipt generation with grand total 
  — Itemized receipt with full pricing breakdown
 • 10% discount applied when total reaches PHP 5,000 — Automatic discount calculation at checkout

Part 2: Enhanced Features

Cart Management Menu!!!

Manage your cart before checkout with a dedicated menu:

 • View Cart — See all items, quantities, prices, and subtotals
 • Remove Item — Delete a specific item from cart (stock restored automatically)
 • Update Quantity — Change item quantity (stock adjusts automatically via property updates)
 • Clear Cart — Empty entire cart and restore all stock
 • Checkout — Proceed to payment and receipt generation

Product Search!

Search products by name with partial matching.

Product Categories
Products are organized by category:
 • Electronics
 • Food
 • Clothing

Filter the store menu to show only products from a selected category.

Stock Reorder Alert!!

After checkout, automatically displays products running low.

Checkout Payment Validation:

Secure payment process:
 • Payment must be numeric (re-prompts if letters entered)
 • Payment must be greater than or equal to final total (re-prompts if insufficient)
 • Automatically computes change

Receipt Number and Date:

Every receipt includes:

 • Auto-generated receipt number (0001, 0002, and so on)
 • Checkout date and time
 • Itemized list with quantities and prices
 • Grand total, discount, final total
 • Payment amount and change

Order History:

All completed transactions are stored during the program run.

View full receipt details by entering the receipt number!

Better Input Validation:

All user inputs are strictly validated:

 • Menu choices: only valid numbers accepted
 • Y/N prompts: re-prompt until Y or N entered
 • Quantities: must be positive numbers
 • Product IDs: must exist in store

How do you run this?

1. Clone the repository
2. Open in Visual Studio or any C# compiler
3. Run Program.cs
4. Follow the menu prompts

Code Structure!

Product class - Product data and stock management
CartItem class - Cart line items and quantity tracking
Order class - Receipt generation and order records
Store class - Business logic, cart operations, checkout, history
Program class - User interface, menus, input handling




