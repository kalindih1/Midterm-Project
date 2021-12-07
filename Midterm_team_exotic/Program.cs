using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Midterm_team_exotic
{
    class Program
    {
        static void Main(string[] args)
        {
            //this is test data of customer purchase
            //var myProduct = new Product { ProductCategory = "test data", ProductDescription = "more test data", ProductPrice = 10.50, ProductName = "test name"};
            //var myProduct2 = new Product { ProductCategory = "super test data", ProductDescription = "super more test data", ProductPrice = 20.50, ProductName = "super test name" };
            //var products = new List<Product> { myProduct, myProduct2};
            //GetUserInput(products);

            var myProduct3 = new TestingProductList { ProductCategory = "test data", ProductDescription = "more test data", ProductPrice = 10.50, ProductName = "test name", PaymentTotal = 20.50, ProductId = 1, ProductOrderQuantity = 3, ProductTotalTax = 40 };
            var myProduct4 = new TestingProductList { ProductCategory = "test data", ProductDescription = "more test data", ProductPrice = 15.50, ProductName = "test name", PaymentTotal = 30.50, ProductId = 2, ProductOrderQuantity = 6, ProductTotalTax = 60 };
            var products = new List<TestingProductList> { myProduct3, myProduct4 };
            GetUserInput(products);
            //I suck


        }

        public static void GetUserInput(List<TestingProductList> products)
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

        public static void AcceptCash(List<TestingProductList> products)
        {
            Console.WriteLine("You've chosen cash");

            //calculate total of the products
            double totalProductCost = 0;
            foreach (TestingProductList product in products)
            {
                totalProductCost = totalProductCost + product.ProductPrice;
                Console.WriteLine(product.ProductPrice);
            }
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
        }
        public static void AcceptCheck(List<TestingProductList> products)
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
        public static void AcceptCredit(List<TestingProductList> products)
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

       



       
    }
}
