using System;
using System.Collections.Generic;

namespace Midterm_team_exotic
{
    public class Invoice
    {

        public static double CalculateSubtotal(List<LineItemData> customerItemPurchaseList)
        {
            double subtotal = 0;
            foreach (var item in customerItemPurchaseList)
            {
                subtotal += item.LineItemTotal; 
            }
            return subtotal;
        }

        public static double CalculateSalesTaxTotal(List<LineItemData> customerItemPurchaseList)
        {
            double taxTotal = 0;

            foreach (var item in customerItemPurchaseList)
            {
                taxTotal += item.LineItemTax;
                
            }
            taxTotal.ToString("0.##");
            return taxTotal; 
        }

        public static double CalculateGrandTotal(double subtotal, double taxTotal)
        {
            double grandTotal = 0;

            grandTotal = subtotal + taxTotal;

            return grandTotal; 

        }

    }
}
