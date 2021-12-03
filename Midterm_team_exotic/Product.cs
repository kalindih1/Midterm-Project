using System;
using System.Collections.Generic;
using System.Text;

namespace Midterm_team_exotic
{
    public class Product
    {
        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }


    }
}
