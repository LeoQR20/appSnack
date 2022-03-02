using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServiceProduct : IServiceProduct
    {
        //public List<Product> GetProductsAll()
        //{
        //    List<Product> products = null;
        //    try
        //    {
        //        using (ProyectoSnacksEntities context = new ProyectoSnacksEntities())
        //        {
        //            context.Configuration.LazyLoadingEnabled = false;
        //            products = context.Products.ToList();
        //        }
        //        return products;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public IEnumerable<Product> GetProducts()
        {
            IRepositoryProduct repository = new ReporsitoryProduct();
            return repository.GetProducts();
        }

        public Product GetProductByID(int id)
        {
            Product product = null;
            try
            {

                using (ProyectoSnacksEntities context = new ProyectoSnacksEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    product = context.Products.Find(id);
                }

                return product;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal CalculateTaxes(decimal subtotal)
        {
            return (decimal)0.13 * subtotal;
        }

        public decimal CalculateTotal(decimal subtotal, decimal taxes, decimal discount)
        {
            return (subtotal + taxes) - discount;
        }

        public decimal CalculateDiscount(decimal subtotal, int pDiscount)
        {
            decimal discountPercentage = (decimal)pDiscount / 100;
            return subtotal * discountPercentage;
        }
    }
}
