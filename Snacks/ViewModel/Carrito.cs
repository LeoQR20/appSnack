using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Util;

namespace Web.ViewModel
{
    public class Carrito
    {
        public List<ViewModelOrdenDetalle> Product { get; private set; }
        //Implementación Singleton
        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        public static readonly Carrito Instancia;
        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        static Carrito()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Product = new List<ViewModelOrdenDetalle>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }
        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected Carrito() { }

        /**
         * AgregarItem (): agrega un artículo a la compra
         */
        public String AgregarItem(int ProductId)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelOrdenDetalle nuevoItem = new ViewModelOrdenDetalle(ProductId);
            // Si este artículo ya existe en lista de libros, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (Product.Exists(x => x.IdProduct == ProductId))
                {
                    ViewModelOrdenDetalle item = Product.Find(x => x.IdProduct == ProductId);
                    item.Cantidad++;
                }
                else
                {
                    nuevoItem.Cantidad = 1;
                    Product.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Reserva Producto", "Producto agregado al carrito", Util.SweetAlertMessageType.success);
            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Reserva Producto", "El producto no existe", Util.SweetAlertMessageType.warning);
            }
            return mensaje;
        }
        /**
         * SetItemCantidad(): cambia la Cantidad de un artículo en el carrito
         */
        public String SetItemCantidad(int ProductId, int Cantidad)
        {
            String mensaje = "";
            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
            if (Cantidad == 0)
            {
                EliminarItem(ProductId);
                mensaje = SweetAlertHelper.Mensaje("Reserva Producto", "Producto eliminado", Util.SweetAlertMessageType.success);
            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad
                ViewModelOrdenDetalle actualizarItem = new ViewModelOrdenDetalle(ProductId);
                if (Product.Exists(x => x.IdProduct == ProductId))
                {
                    ViewModelOrdenDetalle item = Product.Find(x => x.IdProduct == ProductId);
                    item.Cantidad = Cantidad;
                    mensaje = SweetAlertHelper.Mensaje("Reserva Casa", "Cantidad actualizada", Util.SweetAlertMessageType.success);
                }
            }
            return mensaje;
        }
        /**
         * EliminarItem (): elimina un artículo del carrito de compras
         */
        public String EliminarItem(int ProductId)
        {
            String mensaje = "La casa no existe";
            if (Product.Exists(x => x.IdProduct == ProductId))
            {
                var itemEliminar = Product.Single(x => x.IdProduct == ProductId);
                Product.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Reserva Casa", "Casa eliminada", Util.SweetAlertMessageType.success);
            }
            return mensaje;
        }
        public double AllSubTotal()
        {
            double stotal = 0;
            stotal = (double)Product.Sum(x => x.SubTotalProduct);
            return stotal;
        }
        public double AllSubTotalAndIVA()
        {
            double stotal = 0;
            stotal = (double)Product.Sum(x => x.SubTotalProduct);

            return stotal * 0.30;
        }
        public int GetCountItems()
        {
            int total = 0;
            total = Product.Sum(x => x.Cantidad);

            return total;
        }
        public void eliminarCarrito()
        {
            Product.Clear();
        }
    }
}