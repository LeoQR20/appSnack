using Core.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelOrdenDetalle
    {
        public int IdOrden { get; set; }
        public int IdProduct { get; set; }
        //public byte[] Imagen { get; set; }
        public int Cantidad { get; set; }

        public double PrecioProducto
        {
            get { return product.Price; }

        }
        public virtual Product product { get; set; }
        public double SubTotalProduct
        {
            get
            {
                return calculoSubtotal();
            }
        }

        private double calculoSubtotal()
        {
            return this.PrecioProducto * this.Cantidad;
        }
        public ViewModelOrdenDetalle(int IdProduct)
        {
            IServiceProduct _ServiceCasa = new ServiceProduct();
            //IServiceServicio serviceServicio = new ServiceServicio();
            this.IdProduct = IdProduct;
            //this.idServicio = idServicio;
            this.product = _ServiceCasa.GetProductByID(IdProduct);
            //this.Servicios = serviceServicio.GetServicioByID(idServicio);
        }
    }
}