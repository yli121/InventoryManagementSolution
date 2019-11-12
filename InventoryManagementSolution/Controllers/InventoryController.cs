using InventoryManagementSolution.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagementSolution.Controllers
{
    public class InventoryController : ApiController
    {
        InventoryAdapter adapter = new InventoryAdapter();

        public IEnumerable<Product> GetAllProducts()
        {
            DataTable table = adapter.GetInventory();
            IEnumerable<Product> products = (from p in table.AsEnumerable()
                                             select new Product()
                                             {
                                                 ID = int.Parse(p["product_id"].ToString()),
                                                 Name = p["product_name"].ToString(),
                                                 Quantity = int.Parse(p["quantity"].ToString()),
                                                 Brand = p["brand_name"].ToString(),
                                                 Category = p["category_name"].ToString(),
                                                 Price = double.Parse(p["list_price"].ToString())
                                             }).ToList();
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            DataTable table = adapter.GetProductById(id);
            IEnumerable<Product> products = (from p in table.AsEnumerable()
                                             select new Product()
                                             {
                                                 ID = int.Parse(p["product_id"].ToString()),
                                                 Name = p["product_name"].ToString(),
                                                 Quantity = int.Parse(p["quantity"].ToString()),
                                                 Brand = p["brand_name"].ToString(),
                                                 Category = p["category_name"].ToString(),
                                                 Price = double.Parse(p["list_price"].ToString())
                                             }).ToList();
            Product product = products.FirstOrDefault(p => p.ID == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
