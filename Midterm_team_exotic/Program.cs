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

            List<Product> savedList = FileReader.ReadFile();

            RunPOS(savedList); 

        }

        public static void RunPOS(List<Product> savedList)
        {
            bool tureOffPOS = false;
            string userInput;

            do
            {
                Console.WriteLine("Welcome to Mcdonalds!");
                Console.WriteLine();

                List<LineItemData> customerItemPurchaseList = OrderMenu(savedList);
                DisplayInvoiceSummary(customerItemPurchaseList);

                PaymentMethod(customerItemPurchaseList);

                customerItemPurchaseList.Clear();

                Console.WriteLine("Would you like to turn off POS? (y/n) ");
                userInput = Console.ReadLine();


                if (userInput.Trim().ToLower() == "y")
                {
                    tureOffPOS = true;
                }
                else if (userInput.Trim().ToLower() != "n")
                {
                    Console.WriteLine("Incorect input...error...turning off");
                    tureOffPOS = true;

                }

            } while (!tureOffPOS);
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
                    Console.WriteLine("Add another item? (y/n)");
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

            Console.WriteLine("Would you like to pay with Cash, Check or Credit?\n");

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
                Console.WriteLine("Please check your spelling or select payment method again\n");
                Console.WriteLine("Would you like to pay with Cash, Check or Credit?\n");
                checkUserInput = Console.ReadLine().Trim().ToLower();

            }
        }

        public static void AcceptCash(List<LineItemData> products)
        {

            Console.WriteLine("You've chosen cash\n");
            //calculate total of the products
            double totalProductCost = CalculatePaymentTotal(products);
            Console.WriteLine("Your total cost is : " + totalProductCost + "\n");
            Console.WriteLine("Please enter tendered amount\n");

            double cashAmount;
            while (double.TryParse(Console.ReadLine(), out cashAmount) == false)
            {

                Console.WriteLine("Not a valid input. Please input your amount.\n");
            }
            if (cashAmount > totalProductCost)
            {
                Console.WriteLine("This is your amount given : " + cashAmount + "\n");
            }
            else
            {
                Console.WriteLine("You don't have enough money.\n");
                return;
            }
            //subtract total from cashAmount
            double change = cashAmount - totalProductCost;
            Console.WriteLine("Your change is : " + change + "\n");
            PrintReceipt(products, "cash");

        }
        public static void AcceptCheck(List<LineItemData> products)
        {
            Console.WriteLine("You've chosen check\n");
            Console.WriteLine("Please enter your check number\n");


            // TODO: make sure to ask instructor what is a valid check number
            int checkNumber;
            while (int.TryParse(Console.ReadLine(), out checkNumber) == false)
            {
                Console.WriteLine("Not a valid input. Please input your check number\n");
            }
            Console.WriteLine("This is your check number " + checkNumber + "\n");
            PrintReceipt(products, "check");
        }
        public static void AcceptCredit(List<LineItemData> products)
        {
            Console.WriteLine("You've chosen credit\n");

            //Take in credit card number
            Console.WriteLine("Please enter your credit card number\n");

            string cardNumber = Console.ReadLine().Trim();
            cardNumber = cardNumber.Replace(" ", "");

            while (cardNumber.All(char.IsDigit) == false || cardNumber.Length != 16)
            {

                Console.WriteLine("Not a valid input. Make sure you entered 16 numbers\n");

                cardNumber = Console.ReadLine().Trim();
                cardNumber = cardNumber.Replace(" ", "");
            }

            //Take in expiration date
            Console.WriteLine("Please enter your expiration date in the form 'MM/YY' \n");
            string expirationDate = Console.ReadLine().Trim();

            Regex expDatePattern = new Regex(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$");
            Match match = expDatePattern.Match(expirationDate);
            while (match.Success == false)
            {
                Console.WriteLine("Not a valid input.Make sure you entered the date in the correct format\n");
                expirationDate = Console.ReadLine().Trim();
                match = expDatePattern.Match(expirationDate);
            }

            //Take in cw number
            Console.WriteLine("Please enter your cw number\n");
            string cardCw = Console.ReadLine().Trim();
            while (cardCw.All(char.IsDigit) == false || cardCw.Length != 3)
            {
                Console.WriteLine("Not a valid input. Make sure you entered 3 numbers\n");
                cardCw = Console.ReadLine().Trim();
            }
            PrintReceipt(products, "credit");



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

        public static double CalculatePaymentWithTax(List<LineItemData> products)
        {
            const double tax = 0.6;
            double totalProductCost = 0;
            foreach (LineItemData product in products)
            {
                totalProductCost = totalProductCost + (product.ProductPrice * tax);
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
                Console.WriteLine(entry.Key + "\n");
                Console.Write("This is the subtotal : ");
                Console.WriteLine(CalculatePaymentTotal(entry.Value)+ "\n");

              

            }
            


        }
        public static void PrintReceipt(List<LineItemData> products, string paymentType)
        {

            Console.WriteLine("Customer receipt\n");


            foreach (LineItemData product in products)
            {
                Console.Write(product.ProductName);
                Console.Write(" - " + "$" + product.ProductPrice);
                Console.Write(" - " + product.ProductCategory);

                Console.WriteLine(" - " + "quantity: " + product.LineItemQuantity + "\n");




            }

            PrintSubTotal(products);
            Console.WriteLine("This is your grand total : " + CalculatePaymentWithTax(products)+ "\n");
            Console.WriteLine("You paid with " + paymentType + "\n");
        }


       

        

        

        public static Dictionary<string, List<LineItemData>> SeperateCategories(List<LineItemData> products)
        {

            Dictionary<string, List<LineItemData>> categoryDictionary = new Dictionary<string, List<LineItemData>>();

            //loop through product list
            foreach (LineItemData product in products)
            {
                //if new catergory, add key to dictionary
                if (categoryDictionary.ContainsKey(product.ProductCategory) == false)
                {
                    categoryDictionary.Add(product.ProductCategory, new List<LineItemData> { product });
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
