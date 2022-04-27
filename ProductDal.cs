using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces
{
    public interface IProductDal
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        List<Product> FindProduct(string name);

        void create(Product p);
        void update(Product p);
        void delete(int id);

    }
    public class MySqlProductDal : IProductDal
    {
        private MySqlConnection Getconnection()
        {
            string connectionString = @"server=localhost;port=3306;database=northwind;user=root;password=mysql1234";

            return new MySqlConnection(connectionString);

        }
        public void create(Product p)
        {
            using (var connection = Getconnection())
            {
                try
                {
                    connection.Open();
                    string sql = "insert into products(product_name,list_price,discontinued) VALUES (@productname,@price,@disc)";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@productname", p.Name);
                    cmd.Parameters.AddWithValue("@price", p.Price);
                    cmd.Parameters.AddWithValue("@disc", 0);
                    var result = cmd.ExecuteNonQuery();
                    Console.WriteLine(result+" Kayıt Eklendi");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Getconnection().Close();
                }
            }
            
        }

        public void delete(int id)
        {
            using (var connection = Getconnection())
            {
                try
                {
                    connection.Open();
                    string sql = "Delete from products where id=@id ";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    var result = cmd.ExecuteNonQuery();
                    Console.WriteLine(result + " Kayıt Silindi");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Getconnection().Close();
                }
            }
        }

        public List<Product> GetAllProducts()
        {
            {
                List<Product> products = null;

                using (var connection = Getconnection())
                {
                    try
                    {
                        connection.Open();
                        string sql = "select * from products";
                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        products = new List<Product>();

                        while (reader.Read())
                        {

                            products.Add(new Product()
                            {
                                ProductId = Convert.ToInt32(reader["id"]),
                                Name = reader["product_name"].ToString(),
                                Price = double.Parse(reader["list_price"]?.ToString())

                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        Getconnection().Close();
                    }
                }
                return products;
            }
        }

        public Product GetProductById(int id)
        {
            Product products = null;

            using (var connection = Getconnection())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from products where id=@productid";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.Add("@productid", MySqlDbType.Int32).Value = id;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    if (reader.HasRows)
                    {
                        products = new Product()
                        {
                            Name = reader["product_name"]?.ToString(),
                            Price = double.Parse(reader["list_price"].ToString()),
                            ProductId = int.Parse(reader["id"].ToString())
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Getconnection().Close();
                }
            }
            return products;
        }

        public void update(Product p)
        {
            
            using (var connection = Getconnection())
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE products SET product_name=@productname,list_price=@price WHERE id=@id ";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@productname", p.Name);
                    cmd.Parameters.AddWithValue("@price",p.Price);
                    cmd.Parameters.AddWithValue("@id", p.ProductId);

                    var result = cmd.ExecuteNonQuery();
                    Console.WriteLine(result + " Kayıt Güncellendi");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Getconnection().Close();
                }
            }
        }

        public List<Product> FindProduct(string name)
        {
            List<Product>? products = null;

            using (var connection = Getconnection())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from products where product_name LIKE @productname";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.Add("@productname", MySqlDbType.String).Value = $"%{name}%";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    products = new List<Product>();
                    while (reader.Read())
                    {
                        products.Add(new Product()
                        {
                            Name = reader["product_name"].ToString(),
                            Price = double.Parse(reader["list_price"].ToString()),
                        }) ;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Getconnection().Close();
                }
            }
            return products;
        }
    }
    public class ProductManager : IProductDal
    {
        public IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void create(Product p)
        {
            _productDal.create(p);
        }

        public void delete(int id)
        {
            _productDal.delete(id);
        }

        public List<Product> FindProduct(string name)
        {
            return _productDal.FindProduct(name);
        }
        public List<Product> GetAllProducts()
        {
            return _productDal.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productDal.GetProductById(id);
        }

        public void update(Product p)
        {
            _productDal.update(p);
        }
    } 
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }


    }
}
