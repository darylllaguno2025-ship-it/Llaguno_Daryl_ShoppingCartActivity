# Llaguno_Daryl_ShoppingCartActivity

Daryl James M. Llaguno 

“ Shopping Cart System “

A console-based shopping cart application built in C# with product browsing, cart management, payment validation, and order history.

Features:

 Part 1: Basic Features

- Browse products with stock display
- Add items to cart with quantity selection
- Automatic stock deduction
- Receipt generation with grand total
- 10% discount applied when total reaches PHP 5,000

  Part 2: Enhanced Features

1. Cart Management Menu

Manage your cart before checkout with a dedicated menu:

- View Cart - See all items, quantities, prices, and subtotals
- Remove Item - Delete a specific item from cart (stock restored)
- Update Quantity - Change item quantity (stock adjusts automatically)
- Clear Cart - Empty entire cart and restore all stock
- Checkout - Proceed to payment and receipt generation

2. Product Search

Search products by name with partial matching.

Example:
Enter product name to search: mouse
Result:
3 Mouse - PHP 350 [Electronics] - Stock: 15
5 Wireless Mouse - PHP 500 [Electronics] - Stock: 10

3. Product Categories

Products are organized by category:

- Electronics
- Food
- Clothing

Filter the store menu to show only products from a selected category.

 4. Stock Reorder Alert

After checkout, automatically displays products running low.

Example:

LOW STOCK ALERT:
Webcam has only 2 stocks left.
Keyboard has only 1 stock left.

Alert triggers when RemainingStock
is less than or equal to 5.

 5. Checkout Payment Validation
Secure payment process:

- Payment must be numeric (re-prompts if letters entered)
- Payment must be greater than or equal to final total (re-prompts if insufficient)
- Automatically computes change

Example:
FINAL TOTAL: PHP 5200
Enter payment: 5000
Insufficient payment.

Enter payment: 6000
Change: PHP 800


6. Receipt Number and Date

Every receipt includes:

- Auto-generated receipt number (0001, 0002, and so on)
- Checkout date and time
- Itemized list with quantities and prices
- Grand total, discount, final total
- Payment amount and change

Example:
================================
RECEIPT #0001
Date: April 24, 2026 8:30 PM
================================
Item               Qty   Price    Total
-----------------------------------
Monitor              2   PHP 550    PHP 1100
Keyboard             1   PHP 450    PHP 450
-----------------------------------
GRAND TOTAL:  PHP 1550
-----------------------------------
FINAL TOTAL:  PHP 1550
PAYMENT:      PHP 2000
CHANGE:       PHP 450
===================================

7. Order History

All completed transactions are stored during the program run.

Example:
ORDER HISTORY
Receipt #0001 - Final Total: PHP 5200
Receipt #0002 - Final Total: PHP 1800

View full receipt details by entering the receipt number!

 8. Better Input Validation

All user inputs are strictly validated:

- Menu choices: only valid numbers accepted
- Y/N prompts: re-prompt until Y or N entered
- Quantities: must be positive numbers
- Product IDs: must exist in store

Example:
Add another item? (Y/N): maybe
Invalid input. Please enter Y or N only.
Add another item? (Y/N):

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




