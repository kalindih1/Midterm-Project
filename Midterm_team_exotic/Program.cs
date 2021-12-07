using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Midterm_team_exotic
{
    class Program
    {
        static void Main(string[] args)
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

            List<Product> mcDonaldsSortedList = mcDonaldsItems.OrderBy(x => x.ProductCategory).ToList();

            List<CustomerProductList> mcDonaldsfullList = new List<CustomerProductList>();


            foreach (var item in mcDonaldsSortedList)
            {
                CustomerProductList additem = new CustomerProductList()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductCategory = item.ProductCategory,
                    ProductDescription = item.ProductDescription,
                    ProductPrice = item.ProductPrice,
                    PaymentTotal = 0,
                    ProductOrderQuantity = 0
                };

                mcDonaldsfullList.Add(additem); 

            }


            Console.WriteLine("Welcome to Mcdonalds!");
            Console.WriteLine();

            string lastCategory = "";
            foreach (var item in mcDonaldsSortedList)
            {

                if (item.ProductCategory != lastCategory)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"{item.ProductCategory}");
                }

                Console.WriteLine($"{item.ProductName} ID({item.ProductId}) - {item.ProductDescription}: ${item.ProductPrice}");
                lastCategory = item.ProductCategory;
            }

            //asking user for items to order

            List<CustomerProductList> userpurchaseList = new List<CustomerProductList>();
            bool redo = false;
            do
            {

                Console.WriteLine("");
                Console.WriteLine("Please select an item (Number ID).");

                double inputNumberID = double.Parse(Console.ReadLine());

                Console.WriteLine("How many would you like?");
                int inputQuantity = int.Parse(Console.ReadLine());


                //var itempicked = mcDonaldsSortedList.Find(x => x.ProductId == inputNumberID);
                var itempicked = mcDonaldsfullList.Find(x => x.ProductId == inputNumberID);


                double paymentTotal = 0;

                paymentTotal = (inputQuantity * itempicked.ProductPrice) + paymentTotal;

                itempicked.PaymentTotal = (inputQuantity * itempicked.ProductPrice) + paymentTotal;

                itempicked.ProductOrderQuantity = inputQuantity;

                //List<CustomerProductList> userpurchaseList = new List<CustomerProductList>();
                userpurchaseList.Add(itempicked);


                bool pickAnotherItem = false;

                do
                {

                    Console.WriteLine("");
                    Console.WriteLine("Continue(y/n)?");

                    string userInput = "";
                    userInput = Console.ReadLine();

                    if (!string.IsNullOrEmpty(userInput))
                    {
                        pickAnotherItem = true;

                    }
                    else
                    {
                        Console.WriteLine("Sorry, your input is not valid. Try again.");
                        continue;
                    }


                    if (userInput.Trim().ToLower() == "y")
                    {
                        //Do nothing. 
                    }
                    else
                    {
                        redo = true;
                    }


                } while (!pickAnotherItem);

            } while (!redo);


            Console.WriteLine($" Done ");

            //Console.WriteLine("Would you like to ");
        }

    }
}
