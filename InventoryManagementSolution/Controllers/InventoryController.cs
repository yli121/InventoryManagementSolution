using InventoryManagementSolution.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace InventoryManagementSystem
{
    public class InventoryController
    {
        private string connString;
        private DataTable table;

        public InventoryController()
        {
            connString = ConfigurationManager.ConnectionStrings["IMSDBConnectionString"].ConnectionString;
            table = new DataTable();
        }

        public List<Product> GetProductDetails(int prod_id)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select p.product_id,
                                               p.product_name,                                                
                                               s.quantity,
                                               b.brand_name,
                                               c.category_name,
                                               p.list_price
                                        from IMSDB.production.stocks s
                                        JOIN IMSDB.production.products p ON s.product_id = p.product_id
                                        JOIN IMSDB.production.brands b ON p.brand_id = b.brand_id
                                        JOIN IMSDB.production.categories c ON p.category_id = c.category_id
                                        where p.product_id = '" + prod_id + "'";

                    try
                    {
                        cmd.Connection = conn;
                        conn.Open();


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                table.Load(reader);

                                var results = (from p in table.AsEnumerable()
                                               select p).ToList();
                                products = (from p in results
                                            select new Product()).ToList();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("", e);
                    }
                    finally
                    {
                        if (cmd.Connection != null)
                        {
                            if (cmd.Connection.State != ConnectionState.Closed)
                                cmd.Connection.Close();
                            cmd.Connection = null;
                        }
                    }
                }
            }
            return products;
        }
    }
}