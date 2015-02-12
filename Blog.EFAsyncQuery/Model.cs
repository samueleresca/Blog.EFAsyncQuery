using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Data.Entity;



namespace Blog.EFAsyncQuery
{
    /// <summary>
    /// My repository, extends DBContext
    /// </summary>
    public class ProductRepository : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    /// <summary>
    /// Describe  product categories
    /// </summary>
    public class ProductCategory 
    {
        [Key()]
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    /// <summary>
    /// Describe products
    /// </summary>
    public class Product 
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }


}
