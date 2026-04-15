using System;

namespace ShoppingCartSystem
{
class Product
{
public int Id;
public string Name;
public double Price;
public int RemainingStock;

public Product(int id, string name, double price, int remainingStock)  
    {  
        Id = id;  
        Name = name;  
        Price = price;  
        RemainingStock = remainingStock;  
    }  
    public void DisplayProduct()  
    {  
        if (RemainingStock == 0)  
        {  
            Console.WriteLine(Id + " " + Name + " " + Price + " - Out of Stock");  
        }  
        else  
        {  
            Console.WriteLine(Id + " " + Name + " " + Price + " - Stock: " + RemainingStock);  
        }  
    }  
    public bool HasEnoughStock(int quantity)  
    {  
        if (RemainingStock >= quantity)  
        {  
            return true;  
        }  
        else  
        {  
            return false;  
        }  
    }  
    public double GetItemTotal(int quantity)  
    {  
        return Price * quantity;  
    }  
    public void ReduceStock(int quantity)  
    {  
        RemainingStock = RemainingStock - quantity;  
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

    public void Addmore(int AditionalItems)  
    {  
        Quantity = Quantity + AditionalItems;  
        Subtotal = Product.Price * Quantity;  
    }  
}  

class Program  
{  
    static void Main(string[] args)  
    {  
        Product product1 = new Product(1, "Monitor", 550.00, 20);  
        Product product2 = new Product(2, "Keyboard", 450.00, 15);  
        Product product3 = new Product(3, "Mouse", 350.00, 15);  
        Product product4 = new Product(4, "Webcam", 550.00, 10);  

        Product[] storeMenu = new Product[4];  
        storeMenu[0] = product1;  
        storeMenu[1] = product2;  
        storeMenu[2] = product3;  
        storeMenu[3] = product4;  

        Cartitem[] cart = new Cartitem[10];  
        int cartCount = 0;  

        bool keepShopping = true;  

        Console.WriteLine("=================================");  
        Console.WriteLine("Welcome to PC Appliances!");  
        Console.WriteLine("=================================");  

        while (keepShopping == true)  
        {  
            Console.WriteLine("");  
            Console.WriteLine("================================");  
            Console.WriteLine("STORE MENU");  
            Console.WriteLine("================================");  
            for (int i = 0; i < 4; i++)  
            {  
                storeMenu[i].DisplayProduct();  
            }  
            Console.WriteLine("================================");  
            Console.WriteLine("Enter 0 to finish shopping");  
            Console.WriteLine("");  
            Console.WriteLine("Enter product number");  

            string userInput = Console.ReadLine();  

            int productNumber;  
            bool isNumber = int.TryParse(userInput, out productNumber);  


            if (isNumber == false)  
            {  
                Console.WriteLine("Error: PLease Enter number only");  
                continue;  
            }  
            if (productNumber == 0)  
            {  
                break;  
            }  
            Product selectedProduct = null;  
            for (int i = 0; i < 4; i++)  
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
            if (isQuantityNumber == false)  
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
            if (enoughStock == false)  
            {  
                Console.WriteLine("Error: Not enough stock" + selectedProduct.RemainingStock + "available");  
                continue;  
            }  

            int foundInCart = -1;  
            for (int i = 0; i < cartCount; i++)  
            {  
                if (cart[i].Product.Id == selectedProduct.Id)  
                {  
                    foundInCart = i;  
                    break;  
                }  
            }  

            if (foundInCart != -1)  
            {  
                int newTotalQuantity = cart[foundInCart].Quantity + quantity;  
                bool hasEnoughStock = selectedProduct.HasEnoughStock(newTotalQuantity);  
                if (hasEnoughStock == false)  
                {  
                    Console.WriteLine("Error: You already have " + cart[foundInCart].Quantity + " in your cart. Adding " + quantity + " more would exceed stock.");  
                    Console.WriteLine("Maximum you can have: " + selectedProduct.RemainingStock);  
                    continue;  
                }  
                cart[foundInCart].Addmore(quantity);  
                selectedProduct.ReduceStock(quantity);  
                Console.WriteLine("SUCCESS: Updated cart! Now you have " + newTotalQuantity + " " + selectedProduct.Name);  
                Console.WriteLine("Subtotal: $" + cart[foundInCart].Subtotal);  
            }  
            else  
            {  
                if (cartCount == 10)  
                {  
                    Console.WriteLine("Error: Cart is full. Cannot add more items.");  
                    continue;  
                }  
                Cartitem newItem = new Cartitem(selectedProduct, quantity);  
                cart[cartCount] = newItem;  
                cartCount = cartCount + 1;  
                selectedProduct.ReduceStock(quantity);  
                Console.WriteLine("Success: Added to cart!");  
                Console.WriteLine(" " + quantity + " x " + selectedProduct.Name + " = $" + newItem.Subtotal);  
            }  
            Console.WriteLine("");  
            Console.Write("Continue shopping? (Y/N): ");  
            string answer = Console.ReadLine();  
            answer = answer.ToUpper();  
            if (answer == "N")  
            {  
                keepShopping = false;  
            }  
        }  
        Console.WriteLine("");  
        Console.WriteLine("================================");  
        Console.WriteLine("RECEIPT");  
        Console.WriteLine("================================");  
        Console.WriteLine("Item Qty Price Total");  
        Console.WriteLine("-----------------------------------");  
        double grandTotal = 0;  
        for (int i = 0; i < cartCount; i++)  
        {  
            string itemName = cart[i].Product.Name;  
            int itemQty = cart[i].Quantity;  
            double itemPrice = cart[i].Product.Price;  
            double itemTotal = cart[i].Subtotal;  
            grandTotal = grandTotal + itemTotal;  
            Console.Write(itemName);  
            int nameLength = itemName.Length;  
            int spacesNeeded = 18 - nameLength;  
            for (int s = 0; s < spacesNeeded; s++)  
            {  
                Console.Write(" ");  
            }  
            Console.Write(itemQty + "     ");  
            Console.Write("$" + itemPrice + "    ");  
            Console.WriteLine("$" + itemTotal);  
        }  
        Console.WriteLine("-----------------------------------");  
        Console.WriteLine("GRAND TOTAL: $" + grandTotal);  
        double finalTotal = grandTotal;  
        double discount = 0;  
        if (grandTotal >= 5000)  
        {  
            discount = grandTotal * 0.10;  
            finalTotal = grandTotal - discount;  
            Console.WriteLine("DISCOUNT (10%): -$" + discount);  
            Console.WriteLine("-----------------------------------");  
            Console.WriteLine("FINAL TOTAL: $" + finalTotal);  
        }  
        Console.WriteLine("===================================");  
        Console.WriteLine("");  
        for (int i = 0; i < 4; i++)  
        {  
            Console.WriteLine(storeMenu[i].Name + ": " + storeMenu[i].RemainingStock + " left");  
        }  
        Console.WriteLine("Thank you for shopping with us!");  
        Console.WriteLine("Press any key to exit...");  
        try  
        {  
            Console.ReadKey();  
        }  
        catch (InvalidOperationException)  
        {  
            Console.WriteLine("Press Enter to exit...");  
            Console.ReadLine();  
        }  
    }  
}

}
