using System;
using System.Linq;
using System.Collections.Generic;

namespace EnhancedShoppingCartSystem
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;
        public string Category; 

        public Product(int id, string name, double price, int remainingStock, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = remainingStock;
            Category = category;
        }

        public void DisplayProduct()
        {
            if (RemainingStock == 0)
            {
                Console.WriteLine(Id + " " + Name + " " + Price + " [" + Category + "] - Out of Stock");
            }
            else
            {
                Console.WriteLine(Id + " " + Name + " " + Price + " [" + Category + "] - Stock: " + RemainingStock);
            }
        }

        public bool HasEnoughStock(int quantity)
        {
            return RemainingStock >= quantity;
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public void ReduceStock(int quantity)
        {
            RemainingStock = RemainingStock - quantity;
        }

        public void RestoreStock(int quantity)
        {
            RemainingStock = RemainingStock + quantity;
        }
    }

    class Cartitem
    {
        public Product Product;
        public int Quantity;
        public double Subtotal;

        public Cartitem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Subtotal = product.Price * quantity;
        }

        public void Addmore(int additionalItems)
        {
            Quantity = Quantity + additionalItems;
            Subtotal = Product.Price * Quantity;
        }

        public void UpdateQuantity(int newQuantity)
        {
            int difference = newQuantity - Quantity;
            Quantity = newQuantity;
            Subtotal = Product.Price * Quantity;
            Product.ReduceStock(difference);
        }

        public void RemoveFromCart()
        {
            Product.RestoreStock(Quantity);
        }
    }

    class Order
    {
        public string ReceiptNumber;
        public DateTime OrderDate;
        public List<Cartitem> Items;
        public double GrandTotal;
        public double Discount;
        public double FinalTotal;
        public double Payment;
        public double Change;

        public Order(string receiptNumber, DateTime orderDate, List<Cartitem> items,
                     double grandTotal, double discount, double finalTotal,
                     double payment, double change)
        {
            ReceiptNumber = receiptNumber;
            OrderDate = orderDate;
            Items = new List<Cartitem>(items);
            GrandTotal = grandTotal;
            Discount = discount;
            FinalTotal = finalTotal;
            Payment = payment;
            Change = change;
        }

        public void DisplayOrderSummary()
        {
            Console.WriteLine("Receipt #" + ReceiptNumber + " - Final Total: PHP " + FinalTotal);
        }

        public void DisplayFullReceipt()
        {
            Console.WriteLine("");
            Console.WriteLine("================================");
            Console.WriteLine("RECEIPT #" + ReceiptNumber);
            Console.WriteLine("Date: " + OrderDate.ToString("MMMM dd, yyyy h:mm tt"));
            Console.WriteLine("================================");
            Console.WriteLine("Item               Qty   Price    Total");
            Console.WriteLine("-----------------------------------");
            foreach (var item in Items)
            {
                string itemName = item.Product.Name;
                int nameLength = itemName.Length;
                int spacesNeeded = 18 - nameLength;
                if (spacesNeeded < 0) spacesNeeded = 0;
                Console.Write(itemName);
                for (int s = 0; s < spacesNeeded; s++)
                {
                    Console.Write(" ");
                }
                Console.Write(item.Quantity + "     ");
                Console.Write("PHP " + item.Product.Price + "    ");
                Console.WriteLine("PHP " + item.Subtotal);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("GRAND TOTAL: PHP " + GrandTotal);
            if (Discount > 0)
            {
                Console.WriteLine("DISCOUNT (10%): -PHP " + Discount);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("FINAL TOTAL: PHP " + FinalTotal);
            Console.WriteLine("PAYMENT: PHP " + Payment);
            Console.WriteLine("CHANGE: PHP " + Change);
            Console.WriteLine("===================================");
        }
    }

    class Program
    {
        static int receiptCounter = 1;
        static List<Order> orderHistory = new List<Order>();  

        static void Main(string[] args)
        {

            Product product1 = new Product(1, "Monitor", 550.00, 20, "Electronics");
            Product product2 = new Product(2, "Keyboard", 450.00, 15, "Electronics");
            Product product3 = new Product(3, "Mouse", 350.00, 15, "Electronics");
            Product product4 = new Product(4, "Webcam", 550.00, 10, "Electronics");
            Product product5 = new Product(5, "Wireless Mouse", 500.00, 10, "Electronics");
            Product product6 = new Product(6, "Rice", 45.00, 50, "Food");
            Product product7 = new Product(7, "Bread", 35.00, 30, "Food");
            Product product8 = new Product(8, "T-Shirt", 250.00, 25, "Clothing");
            Product product9 = new Product(9, "Jeans", 550.00, 12, "Clothing");

            Product[] storeMenu = new Product[9];
            storeMenu[0] = product1;
            storeMenu[1] = product2;
            storeMenu[2] = product3;
            storeMenu[3] = product4;
            storeMenu[4] = product5;
            storeMenu[5] = product6;
            storeMenu[6] = product7;
            storeMenu[7] = product8;
            storeMenu[8] = product9;

            Cartitem[] cart = new Cartitem[20];
            int cartCount = 0;

            bool running = true;

            Console.WriteLine("=================================");
            Console.WriteLine("Welcome to Daril Store!");
            Console.WriteLine("Enhanced Shopping Cart System");
            Console.WriteLine("=================================");

            while (running)
            {
                Console.WriteLine("");
                Console.WriteLine("========== MAIN MENU ==========");
                Console.WriteLine("1. Browse Products");
                Console.WriteLine("2. Search Product by Name");
                Console.WriteLine("3. Filter by Category");
                Console.WriteLine("4. Cart Management");
                Console.WriteLine("5. View Order History");
                Console.WriteLine("0. Exit");
                Console.WriteLine("===============================");
                Console.Write("Enter choice: ");

                string menuInput = Console.ReadLine();
                int menuChoice;
                bool isValidMenu = int.TryParse(menuInput, out menuChoice);

                if (!isValidMenu)
                {
                    Console.WriteLine("Error: Please enter a number.");
                    continue;
                }

                switch (menuChoice)
                {
                    case 1:
                        BrowseProducts(storeMenu, cart, ref cartCount);
                        break;
                    case 2:
                        SearchProduct(storeMenu);
                        break;
                    case 3:
                        FilterByCategory(storeMenu);
                        break;
                    case 4:
                        CartManagement(storeMenu, cart, ref cartCount);
                        break;
                    case 5:
                        ViewOrderHistory();
                        break;
                    case 0:
                        running = false;
                        Console.WriteLine("Thank you for using the system!");
                        break;
                    default:
                        Console.WriteLine("Error: Invalid menu choice.");
                        break;
                }
            }
        }

        static void BrowseProducts(Product[] storeMenu, Cartitem[] cart, ref int cartCount)
        {
            bool keepShopping = true;

            while (keepShopping)
            {
                Console.WriteLine("");
                Console.WriteLine("================================");
                Console.WriteLine("STORE MENU");
                Console.WriteLine("================================");
                for (int i = 0; i < storeMenu.Length; i++)
                {
                    storeMenu[i].DisplayProduct();
                }
                Console.WriteLine("================================");
                Console.WriteLine("Enter 0 to return to main menu");
                Console.WriteLine("");
                Console.WriteLine("Enter product number");

                string userInput = Console.ReadLine();
                int productNumber;
                bool isNumber = int.TryParse(userInput, out productNumber);

                if (!isNumber)
                {
                    Console.WriteLine("Error: Please Enter number only");
                    continue;
                }
                if (productNumber == 0)
                {
                    return;
                }

                Product selectedProduct = null;
                for (int i = 0; i < storeMenu.Length; i++)
                {
                    if (storeMenu[i].Id == productNumber)
                    {
                        selectedProduct = storeMenu[i];
                        break;
                    }
                }

                if (selectedProduct == null)
                {
                    Console.WriteLine("Error: Product number does not exist");
                    continue;
                }

                if (selectedProduct.RemainingStock == 0)
                {
                    Console.WriteLine("Error: Product is out of stock");
                    continue;
                }

                Console.WriteLine("Enter quantity: ");
                string quantityInput = Console.ReadLine();
                int quantity;
                bool isQuantityNumber = int.TryParse(quantityInput, out quantity);

                if (!isQuantityNumber)
                {
                    Console.WriteLine("Error: Please enter number only");
                    continue;
                }
                if (quantity <= 0)
                {
                    Console.WriteLine("Error: Quantity must be 1 or more");
                    continue;
                }

                bool enoughStock = selectedProduct.HasEnoughStock(quantity);
                if (!enoughStock)
                {
                    Console.WriteLine("Error: Not enough stock. " + selectedProduct.RemainingStock + " available");
                    continue;
                }

                int foundInCart = -1;
                for (int i = 0; i < cartCount; i++)
                {
                    if (cart[i] != null && cart[i].Product.Id == selectedProduct.Id)
                    {
                        foundInCart = i;
                        break;
                    }
                }

                if (foundInCart != -1)
                {
                    bool hasEnoughStock = selectedProduct.HasEnoughStock(quantity);
                    if (!hasEnoughStock)
                    {
                        Console.WriteLine("Error: You already have " + cart[foundInCart].Quantity + " in your cart. Adding " + quantity + " more would exceed stock.");
                        Console.WriteLine("Maximum you can add now: " + selectedProduct.RemainingStock);
                        continue;
                    }
                    cart[foundInCart].Addmore(quantity);
                    selectedProduct.ReduceStock(quantity);
                    Console.WriteLine("SUCCESS: Updated cart! Now you have " + cart[foundInCart].Quantity + " " + selectedProduct.Name);
                    Console.WriteLine("Subtotal: PHP " + cart[foundInCart].Subtotal);
                }
                else
                {
                    if (cartCount >= cart.Length)
                    {
                        Console.WriteLine("Error: Cart is full. Cannot add more items.");
                        continue;
                    }
                    Cartitem newItem = new Cartitem(selectedProduct, quantity);
                    cart[cartCount] = newItem;
                    cartCount++;
                    selectedProduct.ReduceStock(quantity);
                    Console.WriteLine("Success: Added to cart!");
                    Console.WriteLine(" " + quantity + " x " + selectedProduct.Name + " = PHP " + newItem.Subtotal);
                }

                keepShopping = AskYesNo("Continue shopping?");
            }
        }

        static void SearchProduct(Product[] storeMenu)
        {
            Console.WriteLine("");
            Console.WriteLine("========== SEARCH PRODUCT ==========");
            Console.Write("Enter product name to search: ");
            string searchTerm = Console.ReadLine().ToLower();

            bool found = false;
            Console.WriteLine("Result:");
            for (int i = 0; i < storeMenu.Length; i++)
            {
                if (storeMenu[i].Name.ToLower().Contains(searchTerm))
                {
                    storeMenu[i].DisplayProduct();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No products found matching '" + searchTerm + "'.");
            }
        }

        static void FilterByCategory(Product[] storeMenu)
        {
            Console.WriteLine("");
            Console.WriteLine("========== CATEGORIES ==========");
            Console.WriteLine("1. Electronics");
            Console.WriteLine("2. Food");
            Console.WriteLine("3. Clothing");
            Console.Write("Enter category number: ");

            string catInput = Console.ReadLine();
            int catChoice;
            string selectedCategory = "";

            if (!int.TryParse(catInput, out catChoice))
            {
                Console.WriteLine("Error: Please enter a number.");
                return;
            }

            switch (catChoice)
            {
                case 1: selectedCategory = "Electronics"; break;
                case 2: selectedCategory = "Food"; break;
                case 3: selectedCategory = "Clothing"; break;
                default:
                    Console.WriteLine("Error: Invalid category.");
                    return;
            }

            Console.WriteLine("");
            Console.WriteLine("========== " + selectedCategory.ToUpper() + " PRODUCTS ==========");
            bool found = false;
            for (int i = 0; i < storeMenu.Length; i++)
            {
                if (storeMenu[i].Category == selectedCategory)
                {
                    storeMenu[i].DisplayProduct();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No products found in this category.");
            }
        }

        static void CartManagement(Product[] storeMenu, Cartitem[] cart, ref int cartCount)
        {
            bool inCartMenu = true;

            while (inCartMenu)
            {
                Console.WriteLine("");
                Console.WriteLine("========== CART MANAGEMENT ==========");
                Console.WriteLine("1. View Cart");
                Console.WriteLine("2. Remove an Item");
                Console.WriteLine("3. Update Item Quantity");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("0. Return to Main Menu");
                Console.WriteLine("====================================");
                Console.Write("Enter choice: ");

                string choiceInput = Console.ReadLine();
                int choice;
                if (!int.TryParse(choiceInput, out choice))
                {
                    Console.WriteLine("Error: Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ViewCart(cart, cartCount);
                        break;
                    case 2:
                        RemoveItem(cart, ref cartCount);
                        break;
                    case 3:
                        UpdateQuantity(cart, cartCount);
                        break;
                    case 4:
                        ClearCart(cart, ref cartCount);
                        break;
                    case 5:
                        Checkout(storeMenu, cart, ref cartCount);
                        break;
                    case 0:
                        inCartMenu = false;
                        break;
                    default:
                        Console.WriteLine("Error: Invalid choice.");
                        break;
                }
            }
        }

        static void ViewCart(Cartitem[] cart, int cartCount)
        {
            Console.WriteLine("");
            Console.WriteLine("========== YOUR CART ==========");
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.WriteLine("Item               Qty   Price    Subtotal");
            Console.WriteLine("------------------------------------------");
            double total = 0;
            for (int i = 0; i < cartCount; i++)
            {
                string itemName = cart[i].Product.Name;
                int nameLength = itemName.Length;
                int spacesNeeded = 18 - nameLength;
                if (spacesNeeded < 0) spacesNeeded = 0;
                Console.Write(itemName);
                for (int s = 0; s < spacesNeeded; s++)
                {
                    Console.Write(" ");
                }
                Console.Write(cart[i].Quantity + "     ");
                Console.Write("PHP " + cart[i].Product.Price + "    ");
                Console.WriteLine("PHP " + cart[i].Subtotal);
                total += cart[i].Subtotal;
            }
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("CART TOTAL: PHP " + total);
        }

        static void RemoveItem(Cartitem[] cart, ref int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty. Nothing to remove.");
                return;
            }

            ViewCart(cart, cartCount);
            Console.WriteLine("");
            Console.Write("Enter the number of the item to remove (1-" + cartCount + "): ");
            string removeInput = Console.ReadLine();
            int removeIndex;
            if (!int.TryParse(removeInput, out removeIndex))
            {
                Console.WriteLine("Error: Please enter a number.");
                return;
            }

            removeIndex--; 
            if (removeIndex < 0 || removeIndex >= cartCount)
            {
                Console.WriteLine("Error: Invalid item number.");
                return;
            }

            cart[removeIndex].RemoveFromCart();
          
            for (int i = removeIndex; i < cartCount - 1; i++)
            {
                cart[i] = cart[i + 1];
            }
            cart[cartCount - 1] = null;
            cartCount--;

            Console.WriteLine("Item removed successfully.");
        }

        static void UpdateQuantity(Cartitem[] cart, int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty. Nothing to update.");
                return;
            }

            ViewCart(cart, cartCount);
            Console.WriteLine("");
            Console.Write("Enter the number of the item to update (1-" + cartCount + "): ");
            string updateInput = Console.ReadLine();
            int updateIndex;
            if (!int.TryParse(updateInput, out updateIndex))
            {
                Console.WriteLine("Error: Please enter a number.");
                return;
            }

            updateIndex--; 
            if (updateIndex < 0 || updateIndex >= cartCount)
            {
                Console.WriteLine("Error: Invalid item number.");
                return;
            }

            Console.Write("Enter new quantity: ");
            string newQtyInput = Console.ReadLine();
            int newQuantity;
            if (!int.TryParse(newQtyInput, out newQuantity))
            {
                Console.WriteLine("Error: Please enter a number.");
                return;
            }
            if (newQuantity <= 0)
            {
                Console.WriteLine("Error: Quantity must be 1 or more.");
                return;
            }

            int currentQty = cart[updateIndex].Quantity;
            int difference = newQuantity - currentQty;

            if (difference > 0)
            {
                if (!cart[updateIndex].Product.HasEnoughStock(difference))
                {
                    Console.WriteLine("Error: Not enough stock. Only " + cart[updateIndex].Product.RemainingStock + " available.");
                    return;
                }
            }

            if (difference > 0)
            {
                cart[updateIndex].Product.ReduceStock(difference);
            }
            else if (difference < 0)
            {
                cart[updateIndex].Product.RestoreStock(Math.Abs(difference));
            }

            cart[updateIndex].Quantity = newQuantity;
            cart[updateIndex].Subtotal = cart[updateIndex].Product.Price * newQuantity;

            Console.WriteLine("Quantity updated successfully.");
        }

        static void ClearCart(Cartitem[] cart, ref int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is already empty.");
                return;
            }

            for (int i = 0; i < cartCount; i++)
            {
                cart[i].RemoveFromCart();
                cart[i] = null;
            }
            cartCount = 0;
            Console.WriteLine("Cart cleared successfully.");
        }
        static void Checkout(Product[] storeMenu, Cartitem[] cart, ref int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty. Cannot checkout.");
                return;
            }

            double grandTotal = 0;
            for (int i = 0; i < cartCount; i++)
            {
                grandTotal += cart[i].Subtotal;
            }

            double discount = 0;
            double finalTotal = grandTotal;

            if (grandTotal >= 5000)
            {
                discount = grandTotal * 0.10;
                finalTotal = grandTotal - discount;
            }

            Console.WriteLine("");
            Console.WriteLine("================================");
            Console.WriteLine("CHECKOUT");
            Console.WriteLine("================================");
            Console.WriteLine("Item               Qty   Price    Total");
            Console.WriteLine("-----------------------------------");
            for (int i = 0; i < cartCount; i++)
            {
                string itemName = cart[i].Product.Name;
                int nameLength = itemName.Length;
                int spacesNeeded = 18 - nameLength;
                if (spacesNeeded < 0) spacesNeeded = 0;
                Console.Write(itemName);
                for (int s = 0; s < spacesNeeded; s++)
                {
                    Console.Write(" ");
                }
                Console.Write(cart[i].Quantity + "     ");
                Console.Write("PHP " + cart[i].Product.Price + "    ");
                Console.WriteLine("PHP " + cart[i].Subtotal);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("GRAND TOTAL: PHP " + grandTotal);
            if (discount > 0)
            {
                Console.WriteLine("DISCOUNT (10%): -PHP " + discount);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("FINAL TOTAL: PHP " + finalTotal);

            double payment = 0;
            bool validPayment = false;
            while (!validPayment)
            {
                Console.Write("Enter payment: ");
                string paymentInput = Console.ReadLine();
                if (!double.TryParse(paymentInput, out payment))
                {
                    Console.WriteLine("Error: Payment must be numeric.");
                    continue;
                }
                if (payment < finalTotal)
                {
                    Console.WriteLine("Insufficient payment.");
                    continue;
                }
                validPayment = true;
            }

            double change = payment - finalTotal;
            Console.WriteLine("Change: PHP " + change);

            string receiptNumber = receiptCounter.ToString("D4");
            receiptCounter++;
            DateTime orderDate = DateTime.Now;

            List<Cartitem> orderItems = new List<Cartitem>();
            for (int i = 0; i < cartCount; i++)
            {
                orderItems.Add(cart[i]);
            }

            Order newOrder = new Order(receiptNumber, orderDate, orderItems,
                                        grandTotal, discount, finalTotal,
                                        payment, change);
            orderHistory.Add(newOrder);

            Console.WriteLine("");
            Console.WriteLine("================================");
            Console.WriteLine("RECEIPT #" + receiptNumber);
            Console.WriteLine("Date: " + orderDate.ToString("MMMM dd, yyyy h:mm tt"));
            Console.WriteLine("================================");
            Console.WriteLine("Item               Qty   Price    Total");
            Console.WriteLine("-----------------------------------");
            for (int i = 0; i < cartCount; i++)
            {
                string itemName = cart[i].Product.Name;
                int nameLength = itemName.Length;
                int spacesNeeded = 18 - nameLength;
                if (spacesNeeded < 0) spacesNeeded = 0;
                Console.Write(itemName);
                for (int s = 0; s < spacesNeeded; s++)
                {
                    Console.Write(" ");
                }
                Console.Write(cart[i].Quantity + "     ");
                Console.Write("PHP " + cart[i].Product.Price + "    ");
                Console.WriteLine("PHP " + cart[i].Subtotal);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("GRAND TOTAL: PHP " + grandTotal);
            if (discount > 0)
            {
                Console.WriteLine("DISCOUNT (10%): -PHP " + discount);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("FINAL TOTAL: PHP " + finalTotal);
            Console.WriteLine("PAYMENT: PHP " + payment);
            Console.WriteLine("CHANGE: PHP " + change);
            Console.WriteLine("===================================");

            Console.WriteLine("");
            Console.WriteLine("========== LOW STOCK ALERT ==========");
            bool hasLowStock = false;
            for (int i = 0; i < storeMenu.Length; i++)
            {
                if (storeMenu[i].RemainingStock <= 5)
                {
                    Console.WriteLine(storeMenu[i].Name + " has only " + storeMenu[i].RemainingStock + " stocks left.");
                    hasLowStock = true;
                }
            }
            if (!hasLowStock)
            {
                Console.WriteLine("All products have sufficient stock.");
            }

            for (int i = 0; i < cartCount; i++)
            {
                cart[i] = null;
            }
            cartCount = 0;

            Console.WriteLine("");
            Console.WriteLine("Checkout complete! Thank you for shopping with us!");
        }
        static void ViewOrderHistory()
        {
            Console.WriteLine("");
            Console.WriteLine("========== ORDER HISTORY ==========");

            if (orderHistory.Count == 0)
            {
                Console.WriteLine("No orders yet.");
                return;
            }

            for (int i = 0; i < orderHistory.Count; i++)
            {
                orderHistory[i].DisplayOrderSummary();
            }

            Console.WriteLine("");
            if (AskYesNo("View full receipt details?"))
            {
                Console.Write("Enter receipt number (e.g., 0001): ");
                string receiptNum = Console.ReadLine();
                bool found = false;
                for (int i = 0; i < orderHistory.Count; i++)
                {
                    if (orderHistory[i].ReceiptNumber == receiptNum)
                    {
                        orderHistory[i].DisplayFullReceipt();
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Receipt not found.");
                }
            }
        }

        static bool AskYesNo(string question)
        {
            while (true)
            {
                Console.Write(question + " (Y/N): ");
                string answer = Console.ReadLine();
                answer = answer.ToUpper().Trim();

                if (answer == "Y")
                {
                    return true;
                }
                else if (answer == "N")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y or N only.");
                }
            }
        }
    }
}

