using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Midterm_team_exotic
{
    class Program
    {
        static void Main(string[] args)
        {

            //string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\ProductList.txt");
            List<Product> savedList = FileReader.ReadFile();

            Console.WriteLine("Welcome to Mcdonalds!");
            Console.WriteLine();

            List<LineItemData> customerItemPurchaseList = OrderMenu(savedList);
            DisplayInvoiceSummary(customerItemPurchaseList);
          
          
            var myProduct1 = new LineItemData { ProductCategory = "dinner", ProductDescription = "more test data", ProductPrice = 13.50, ProductName = "Cheeseburger", LineItemTotal = 20.50, ProductId = 1, LineItemQuantity = 3, LineItemTax = 40 };
            var myProduct2 = new LineItemData { ProductCategory = "dinner", ProductDescription = "more test data", ProductPrice = 18.50, ProductName = "cookies", LineItemTotal = 30.50, ProductId = 2, LineItemQuantity = 6, LineItemTax = 60 };

            var myProduct3 = new LineItemData { ProductCategory = "lunch", ProductDescription = "more test data", ProductPrice = 10.50, ProductName = "nuggets", LineItemTotal = 20.50, ProductId = 1, LineItemQuantity = 3, LineItemTax = 40 };
            var myProduct4 = new LineItemData { ProductCategory = "lunch", ProductDescription = "more test data", ProductPrice = 15.50, ProductName = "coke", LineItemTotal = 30.50, ProductId = 2, LineItemQuantity = 6, LineItemTax = 60 };
            var lunchProducts = new List<LineItemData> { myProduct3, myProduct4, };
            var allProducts = new List<LineItemData> { myProduct1, myProduct2, myProduct3, myProduct4 };
           
            PaymentMethod(lunchProducts);

          
          Console.ReadLine();

        }

        public static void DisplayInvoiceSummary(List<LineItemData> customerItemPurchaseList)
        {
            Console.WriteLine("");
            Console.WriteLine($"Subtotal    =  {Invoice.CalculateSubtotal(customerItemPurchaseList):C}");
            Console.WriteLine($"Sales Tax   =  {Invoice.CalculateSalesTaxTotal(customerItemPurchaseList):C}");
            Console.WriteLine($"Grand Total =  {Invoice.CalculateGrandTotal(Invoice.CalculateSubtotal(customerItemPurchaseList), Invoice.CalculateSalesTaxTotal(customerItemPurchaseList)):C}");
        }

        public static void MenuDisplay(List<LineItemData> mcDonaldsSortedList)
        {

            string lastCategory = "";
            foreach (var item in mcDonaldsSortedList)
            {

                if (item.ProductCategory != lastCategory)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"{item.ProductCategory}");
                }

                Console.WriteLine($"{item.ProductName} ID({item.ProductId}) - {item.ProductDescription}: {item.ProductPrice:C}");
                lastCategory = item.ProductCategory;
            }

        }


        public static List<LineItemData> OrderMenu(List<Product> products)
        {
            List<Product> mcDonaldsSortedList = products.OrderBy(x => x.ProductCategory).ToList();

            List<LineItemData> mcDonaldsfullList = new List<LineItemData>();

            foreach (var item in mcDonaldsSortedList)
            {
                mcDonaldsfullList.Add(new LineItemData()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductCategory = item.ProductCategory,
                    ProductDescription = item.ProductDescription,
                    ProductPrice = item.ProductPrice

                });

            }

            MenuDisplay(mcDonaldsfullList);

            List<LineItemData> userpurchaseList = new List<LineItemData>();
            bool redo = false;
            do
            {
                bool wrongItemInput = false;
                string selectText = "Please select an item (Number ID).";
                double inputNumberID;
                do
                {

                    Console.WriteLine("");
                    if (wrongItemInput == false)
                    {
                        Console.WriteLine(selectText);
                    }
                    else
                    {
                        Console.WriteLine($"Item number ID not eligible - {selectText}");
                    }

                    bool successfullUserItemInput = double.TryParse(Console.ReadLine(), out inputNumberID);

                    if (successfullUserItemInput == false)
                    {
                        wrongItemInput = true;
                    }

                    try
                    {
                        var result = mcDonaldsSortedList.Find(x => x.ProductId == inputNumberID).ProductId;
                        wrongItemInput = false;
                    }
                    catch (Exception)
                    {
                        wrongItemInput = true;
                    }

                } while (wrongItemInput);

                bool wrongQuantityInput = false;
                int inputQuantity = 0;

                do
                {
                    Console.WriteLine("How many would you like?");
                    bool successfullUserQuantityInput = int.TryParse(Console.ReadLine(), out inputQuantity);

                    if (successfullUserQuantityInput == false || inputQuantity <= 0)
                    {
                        Console.WriteLine("Quantity needs to be a positive interger. Try again - ");
                        wrongQuantityInput = true;
                    }
                    else
                    {
                        wrongQuantityInput = false;
                    }

                } while (wrongQuantityInput);

                var itempicked = mcDonaldsfullList.Find(x => x.ProductId == inputNumberID);

                itempicked.LineItemQuantity = inputQuantity;

                itempicked.LineItemTotal = LineItemData.calculateItemTotal(inputQuantity, itempicked.ProductPrice);
                itempicked.LineItemTax = LineItemData.calculateItemTax(itempicked.LineItemTotal);

                userpurchaseList.Add(itempicked);


                bool pickAnotherItem = false;

                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("Continue(y/n)?");
                    String errorResponce = "Sorry, your input is not valid. Try again."; 
                    string userInput = "";
                    userInput = Console.ReadLine();

                    if (!string.IsNullOrEmpty(userInput))
                    {
                        pickAnotherItem = true;
                    }
                    else
                    {
                        Console.WriteLine(errorResponce);
                        continue;
                    }

                    if (userInput.Trim().ToLower() == "y")
                    {
                        MenuDisplay(mcDonaldsfullList);
                    }
                    else if (userInput.Trim().ToLower() != "n")
                    {
                        Console.WriteLine(errorResponce);
                        pickAnotherItem = false;
                        continue;
                    }
                    else
                    {
                        redo = true;
                    }

                } while (!pickAnotherItem);

            } while (!redo);

            return userpurchaseList;

        }

        public static List<Product> MenuDisplayTestData()
        {
            Product itemA = new Product()
            {
                ProductId = 1,
                ProductName = "McDouble",
                ProductCategory = "Burgers",
                ProductDescription = "The classic McDouble® burger",
                ProductPrice = 1.50
            };

            Product itemB = new Product()
            {
                ProductId = 2,
                ProductName = "McRib",
                ProductCategory = "Sandwiches",
                ProductDescription = "It’s no wonder the McRib",
                ProductPrice = 5.50
            };

            Product itemC = new Product()
            {
                ProductId = 3,
                ProductName = "Sausage McMuffin",
                ProductCategory = "Breakfast",
                ProductDescription = "McDonald's Sausage McMuffin",
                ProductPrice = 2.30
            };

            Product itemD = new Product()
            {
                ProductId = 5,
                ProductName = "McEddie",
                ProductCategory = "Sandwiches",
                ProductDescription = "It’s no wonder the McRib",
                ProductPrice = 5.50
            };

            Product itemE = new Product()
            {
                ProductId = 4,
                ProductName = "Sausage McEddie",
                ProductCategory = "Breakfast",
                ProductDescription = "McDonald's Eddie McMuffin",
                ProductPrice = 10.30
            };
          
          
            List<Product> mcDonaldsItems = new List<Product>();
            mcDonaldsItems.Add(itemA);
            mcDonaldsItems.Add(itemB);
            mcDonaldsItems.Add(itemC);
            mcDonaldsItems.Add(itemD);
            mcDonaldsItems.Add(itemE);

            return mcDonaldsItems;



        }

        public static void PaymentMethod(List<LineItemData> products)
        {


            Console.WriteLine("Would you like to pay with Cash, Check or Credit?");
            string checkUserInput = Console.ReadLine().Trim().ToLower();

            if (checkUserInput == "cash")
            {
                AcceptCash(products);
            }
            else if (checkUserInput == "check")
            {
                AcceptCheck(products);
            }
            else if (checkUserInput == "credit")
            {
                AcceptCredit(products);

            }
            else
            {
                Console.WriteLine("Please check your spelling or select payment method again");
                Console.WriteLine("Would you like to pay with Cash, Check or Credit?");
                checkUserInput = Console.ReadLine().Trim().ToLower();

            }
        }

        public static void AcceptCash(List<LineItemData> products)
        {
            Console.WriteLine("You've chosen cash");
            //calculate total of the products
            double totalProductCost = CalculatePaymentTotal(products);
            Console.WriteLine("Your total cost is : " + totalProductCost);
            Console.WriteLine("Please enter tendered amount");

            double cashAmount;
            while (double.TryParse(Console.ReadLine(),out cashAmount) == false)
            {
                Console.WriteLine("Not a valid input. Please input your amount.");
            }
            if (cashAmount > totalProductCost)
            {
                Console.WriteLine("This is your amount given : " + cashAmount);
            }
            else
            {
                Console.WriteLine("You don't have enough money.");
                return;
            }
            //subtract total from cashAmount
            double change = cashAmount - totalProductCost;
            Console.WriteLine("Your change is : " + change);
            PrintCashReceipt(products);
        }
        public static void AcceptCheck(List<LineItemData> products)
        {
            Console.WriteLine("You've chosen check");
            Console.WriteLine("Please enter your check number");

            // TODO: make sure to ask instructor what is a valid check number
            int checkNumber;
            while (int.TryParse(Console.ReadLine(), out checkNumber) == false)
            {
                Console.WriteLine("Not a valid input. Please input your check number");
            }
            Console.WriteLine("This is your check number " + checkNumber);
        }
        public static void AcceptCredit(List<LineItemData> products)
        {   
            Console.WriteLine("You've chosen credit");
            //Take in credit card number
            Console.WriteLine("Please enter your credit card number");

            string cardNumber = Console.ReadLine().Trim();
            cardNumber = cardNumber.Replace(" ", "");
            
            while (cardNumber.All(char.IsDigit) == false || cardNumber.Length != 16)
            {
                
                Console.WriteLine("Not a valid input. Make sure you entered 16 numbers");
                cardNumber = Console.ReadLine().Trim();
                cardNumber = cardNumber.Replace(" ", "");
            }
            
            //Take in expiration date
            Console.WriteLine("Please enter your expiration date in the form 'MM/YY' ");
            string expirationDate = Console.ReadLine().Trim();

            Regex expDatePattern = new Regex(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$");
            Match match = expDatePattern.Match(expirationDate);
            while (match.Success == false )
            {
                Console.WriteLine("Not a valid input.Make sure you entered the date in the correct format");
                expirationDate = Console.ReadLine().Trim();
                match = expDatePattern.Match(expirationDate);
            }
            
            //Take in cw number
            Console.WriteLine("Please enter your cw number");
            string cardCw = Console.ReadLine().Trim();
            while (cardCw.All(char.IsDigit) == false || cardCw.Length != 3)
            {
                Console.WriteLine("Not a valid input. Make sure you entered 3 numbers");
                cardCw = Console.ReadLine().Trim();
            }
            
        }

        public static double CalculatePaymentTotal(List<LineItemData> products)
        {
            double totalProductCost = 0;
            foreach (LineItemData product in products)
            {
                totalProductCost = totalProductCost + product.ProductPrice;
            }
            return totalProductCost;
        }

       public static void PrintSubTotal(List<LineItemData> products)
        {
            Dictionary<string, List<LineItemData>> categoryDictionary = new Dictionary<string, List<LineItemData>>();
            categoryDictionary = SeperateCategories(products);
            foreach (KeyValuePair<string, List<LineItemData>> entry in categoryDictionary)
            {
                Console.Write("Category : ");
                Console.WriteLine(entry.Key);
            }


        }

        public static void PrintCashReceipt(List<LineItemData> products)
        {
           Console.WriteLine("Customer receipt");

           foreach(LineItemData product in products)
            {
                Console.Write(product.ProductName);
                Console.Write(" - " + "$" + product.ProductPrice);
                Console.Write(" - " + product.ProductCategory);
                Console.WriteLine(" - " + "quantity: " + product.LineItemQuantity);
                
               
                
            }
            Console.WriteLine("This is your sub total : ");
            PrintSubTotal(products);
            Console.WriteLine("This is your grand total : " + CalculatePaymentTotal(products));
            Console.WriteLine("You paid with cash");
        }

        public static void PrintCreditReceipt(List<LineItemData> products)
        {
            foreach (LineItemData product in products)
            {
                Console.WriteLine(product.ProductName, product.LineItemTotal, product.ProductCategory, product.LineItemQuantity);

            }
            Console.WriteLine("This is your total : " + CalculatePaymentTotal(products));
            Console.WriteLine("You paid with credit");
        }

        public static void PrintCheckReceipt(List<LineItemData> products)
        {
            foreach (LineItemData product in products)
            {
                Console.WriteLine(product.ProductName, product.LineItemTotal, product.ProductCategory, product.LineItemQuantity);

            }
            Console.WriteLine("This is your total : " + CalculatePaymentTotal(products));
            Console.WriteLine("You paid with check");
        }

        public static Dictionary<string, List<LineItemData>> SeperateCategories(List<LineItemData> products)
        {

            Dictionary<string, List<LineItemData>> categoryDictionary = new Dictionary<string, List<LineItemData>>();

            //loop through product list
            foreach (LineItemData product in products )
            {   
                //if new catergory, add key to dictionary
                if (categoryDictionary.ContainsKey(product.ProductCategory) == false)
                {
                    categoryDictionary.Add(product.ProductCategory, new List<LineItemData> {product});
                }
                //if old category, add new product
                else
                {
                    categoryDictionary[product.ProductCategory].Add(product); 
                }   

            }

            return categoryDictionary;
        }


    }
}
