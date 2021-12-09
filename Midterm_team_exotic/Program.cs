using System;
using System.Collections.Generic;
using System.Linq;

namespace Midterm_team_exotic
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Welcome to Mcdonalds!");
            Console.WriteLine();

            List<LineItemData> customerItemPurchaseList = OrderMenu(savedList);
            DisplayInvoiceSummary(customerItemPurchaseList);

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

    }
}
