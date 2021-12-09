using System;
using System.Collections.Generic;
using System.IO;


namespace Midterm_team_exotic
{
    class Program
    {
        static void Main(string[] args)
        {

           

            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\ProductList.txt");



            List<Product> savedList = FileReader.ReadFile();
            

            foreach (var item in savedList)
            {
                Console.WriteLine(item.ProductName);
            }

      



            Console.ReadLine();
        }
    }
}
