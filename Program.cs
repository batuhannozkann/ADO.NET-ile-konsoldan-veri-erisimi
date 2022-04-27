using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            var productdal = new MySqlProductDal();
            ProductManager _products = new ProductManager(new MySqlProductDal());
            /*_products.create(new Product()
            {
                Name = "İphone 11",
                Price=12200

            });*/
            //-------------------------------------
            /*var products = _products.GetProductById(4);                          
            Console.WriteLine(products.Name);*/
            //-------------------------------------
            /* var products = _products.FindProduct("trade");                     
             foreach(var pr in products)
             {
                 Console.WriteLine(pr.Name);
             }*/
            //--------------------------------------
            /*var products = _products.GetAllProducts();
            foreach(var pr in products)
            {
                Console.WriteLine(pr.Name);
            }*/
            //--------------------------------------
            /*_products.update(new Product()
            {
                Name = "İphone12",
                Price=15000,
                ProductId = 100

            });*/
            //--------------------------------------
            //_products.delete(100);



            
            

        }
        
    }
}
