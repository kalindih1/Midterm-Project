using System;
using System.Collections.Generic;
namespace Midterm_team_exotic
{
    public class CustomerProductList
    {

        public List<Product> Products { get; set; } = new List<Product>();
        public double PaymentTotal { get; set; }
        public int ProductOrderQuantity { get; set; }
        public double ProductTotalTax { get; set; }
    }
}
