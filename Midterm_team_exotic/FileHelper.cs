﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Midterm_team_exotic
{
    public static class FileReader
    {

        public static void WriteFile(string path, Product product)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), @"\ProductList.txt");

            if(File.Exists(path) == true)
            {            
            StreamWriter streamWriter = new StreamWriter(path, true);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(product.ProductId);
            stringBuilder.Append(" | ");
            stringBuilder.Append(product.ProductName);
            stringBuilder.Append(" | ");
            stringBuilder.Append(product.ProductCategory);
            stringBuilder.Append(" | ");
            stringBuilder.Append(product.ProductDescription);
            stringBuilder.Append(" | ");
            stringBuilder.Append(product.ProductPrice);
            streamWriter.WriteLine(stringBuilder.ToString());
            streamWriter.Flush();
            streamWriter.Close();
            }
            else
            {
                throw new Exception("Please double check the filename to ensure it matches ProductList.txt");
            }
        }

        public static List<Product> ReadFile(string path)
        {
            List<Product> products = new List<Product>();
            path = Path.Combine(Directory.GetCurrentDirectory(), @"\ProductList.txt");

            if(File.Exists(path) == true)
            { 

            using(StreamReader streamReader = new StreamReader(path))
            {
                string lineText;
                while ((lineText = streamReader.ReadLine()) !=null)
                {
                    string[] items = lineText.Split(" | ");
                    if (items.Length != 5)
                    {
                        continue;
                    }

                    Product productList = new Product();
                    productList.ProductId = int.Parse(items[0]);
                    productList.ProductName = items[1];
                    productList.ProductCategory = items[2];
                    productList.ProductDescription = items[3];
                    productList.ProductPrice = double.Parse(items[4]);

                    products.Add(productList);
                }
            }
            }
            else
            {
                throw new Exception("Please double check the filename to ensure it matches ProductList.txt");
            }

            return products;
        }
    }
}
