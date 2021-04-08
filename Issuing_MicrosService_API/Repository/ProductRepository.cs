using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issuing_MicrosService_API.Model;
using Microsoft.EntityFrameworkCore;
using Issuing_MicrosService_API.DBContexts;

namespace Issuing_MicrosService_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteProduct(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            _dbContext.Products.Remove(product);
            Save();
        }

        public Product GetProductByID(int productId)
        {
            return _dbContext.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public void InsertProduct(Product product)
        {
            _dbContext.Add(product);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            Save();
        }

        //public void DeleteProduct(int ProductId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Product GetProductByID(int ProductId)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Product> GetProducts()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Save()
        //{
        //    throw new NotImplementedException();
        //}
        //public void InsertProduct(Product Product)
        //{
        //    throw new NotImplementedException();
        //}
        //public void UpdateProduct(Product Product)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
