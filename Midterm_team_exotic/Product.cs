using System;
using System.Collections.Generic;
using System.Text;

namespace Midterm_team_exotic
{
    class Product
    {
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string descriptionName;

        public string DiscriptionName
        {
            get { return descriptionName; }
            set { descriptionName = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }


    }
}
