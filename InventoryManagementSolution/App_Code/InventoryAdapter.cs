using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace InventoryManagementSolution
{
    public class InventoryAdapter
    {
        private string connString;
        private DataTable table;

        public InventoryAdapter()
        {
            connString = ConfigurationManager.ConnectionStrings["IMSDBConnectionString"].ConnectionString;
            table = new DataTable();
        }

        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select category_name
                                        from IMSDB.production.categories";

                    try
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categories.Add(reader[0].ToString());
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
            return categories;
        }

        public DataTable GetInventory()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select p.product_name,
                                                       
                                                       s.quantity,
                                                       b.brand_name,
                                                       c.category_name,
                                                       p.list_price,
                                                       p.list_price * s.quantity as total_value
                                                from IMSDB.production.stocks s
                                                JOIN IMSDB.production.products p ON s.product_id = p.product_id
                                                JOIN IMSDB.production.brands b ON p.brand_id = b.brand_id
                                                JOIN IMSDB.production.categories c ON p.category_id = c.category_id";

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        try
                        {
                            cmd.Connection = conn;
                            conn.Open();

                            sda.SelectCommand = cmd;
                            sda.Fill(table);
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
            }
            return table;
        }

        public DataTable FilterInventory(string filter)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select p.product_name,
                                                       
                                                       s.quantity,
                                                       b.brand_name,
                                                       c.category_name,
                                                       p.list_price,
                                                       p.list_price * s.quantity as total_value
                                                from IMSDB.production.stocks s
                                                JOIN IMSDB.production.products p ON s.product_id = p.product_id
                                                JOIN IMSDB.production.brands b ON p.brand_id = b.brand_id
                                                JOIN IMSDB.production.categories c ON p.category_id = c.category_id
                                                where p.product_name LIKE '%" + filter + "%'";

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        try
                        {
                            cmd.Connection = conn;
                            conn.Open();

                            sda.SelectCommand = cmd;
                            sda.Fill(table);
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
            }
            return table;
        }

        public DataTable FilterByCategory(string category)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select p.product_name,
                                                       
                                                       s.quantity,
                                                       b.brand_name,
                                                       c.category_name,
                                                       p.list_price,
                                                       p.list_price * s.quantity as total_value
                                                from IMSDB.production.stocks s
                                                JOIN IMSDB.production.products p ON s.product_id = p.product_id
                                                JOIN IMSDB.production.brands b ON p.brand_id = b.brand_id
                                                JOIN IMSDB.production.categories c ON p.category_id = c.category_id
                                                where c.category_name = '" + category + "'";

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        try
                        {
                            cmd.Connection = conn;
                            conn.Open();

                            sda.SelectCommand = cmd;
                            sda.Fill(table);
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
            }
            return table;
        }
    }
}