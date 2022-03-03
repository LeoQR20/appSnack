using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServicesUser
    {       
        public User GetUser(User pUser)
        {
            User user = null;
            try
            {
                using (ProyectoSnacksEntities context = new ProyectoSnacksEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    user = context.Users.Where(u => u.ID == pUser.ID && u.Password == pUser.Password).FirstOrDefault();
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
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
    }
}
