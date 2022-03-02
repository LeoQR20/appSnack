using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ReporsitoryProduct : IRepositoryProduct
    {
        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Products.ToList();
            }
            return lista;
        }
    }
}
