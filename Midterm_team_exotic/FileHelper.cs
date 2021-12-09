using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Midterm_team_exotic
{

    //Please note the class name. It is different than the filename listed.
    public static class FileReader
    {

        //Creating a file to store our list of products.
        //Write to the file in the current directory. 
        //Optional variable included to get the current directory if the file changes location.
        public static void WriteFile(Product product)
        {
            //Optional variable for the path
            //string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\productlist.txt");

            string path = @"..\..\..\productlist.txt";

            if (File.Exists(path) == true)
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

        //Reading the ProductList file created by the WriteFile method above.
        //Read from current directory. 
        //Optional variable included to get the current directory in case it changes.
        public static List<Product> ReadFile()
        {
            List<Product> products = new List<Product>();

            //Optional variable for path
            //string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\productlist.txt");

            string path = @"..\..\..\productlist.txt";

            if (File.Exists(path) == true)
            {

                using (StreamReader streamReader = new StreamReader(path))
                {
                    string lineText;
                    while ((lineText = streamReader.ReadLine()) != null)
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

        //Add the last item of a list of products to a list
        //It then writes the LAST item of the list to a the file.
        public static void AddProductToFile(List<Product> product)
        {
            string path = @"..\..\..\productlist.txt";
            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {

                Product productItem = product.Last();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(productItem.ProductId);
                stringBuilder.Append(" | ");
                stringBuilder.Append(productItem.ProductName);
                stringBuilder.Append(" | ");
                stringBuilder.Append(productItem.ProductCategory);
                stringBuilder.Append(" | ");
                stringBuilder.Append(productItem.ProductDescription);
                stringBuilder.Append(" | ");
                stringBuilder.Append(productItem.ProductPrice);
                streamWriter.WriteLine(stringBuilder.ToString());
            }

        }
    }
}
