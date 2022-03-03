using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServiceOrder
    {
        public void SaveOrder(Order order, List<Order_Product> Order_Products)
        {
            int regresa = 0;
            try
            {
                using (ProyectoSnacksEntities context = new ProyectoSnacksEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Orders.Add(order);

                    foreach (var item in Order_Products)
                    {
                        Order_Product order_Product = new Order_Product();
                        order_Product.ProductID = item.ProductID;
                        order_Product.Quantity = item.Quantity;
                        order_Product.ID = order.ID;
                        context.Order_Product.Add(order_Product);
                    }
                    regresa = context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Order GetInvoice(int id)
        {
            Order orden = null;
            try
            {
                using (ProyectoSnacksEntities context = new ProyectoSnacksEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    orden = context.Orders.Find(id);
                }

                return orden;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public decimal CalculateSubtotal(List<Order_Product> productInvoices)
        {
            decimal? subtotal = 0;
            foreach (var item in productInvoices)
            {
                subtotal += (decimal?)(item.Product.Price * item.Quantity);
            }
            return (decimal)subtotal;
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
