using System;
namespace Midterm_team_exotic
{
    public class LineItemData : Product
    {
        private const double salesTax  = .06;

        public double LineItemTotal { get; set; }
        public int LineItemQuantity { get; set; }
        public double LineItemTax { get; set; }

        public static double calculateItemTax(double itemTotal)
        {
            double lineItemTax = -1;
            lineItemTax = itemTotal * salesTax; 
            return lineItemTax; 
        }

        public static double calculateItemTotal(double itemQuantity, double productPrice)
        {
            double lineItemTotal = -1;
            lineItemTotal = itemQuantity * productPrice;
            return lineItemTotal; 
        }

    }
}
