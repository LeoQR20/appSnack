using Core.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Snack.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Products()
        {
            IEnumerable<Product> lista = null;
            try
            {
                IServiceProduct _serviceProduct = new ServiceProduct();
                lista = _serviceProduct.GetProducts();
                ViewBag.title = "Lista Productos";
                return View(lista);
            }
            catch (Exception )
            {
                throw;
            }
        }
        public ActionResult ordenarProducto(int? idProduct)
        {
            int cantidadCasas = Carrito.Instancia.Product.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idProduct);
            return PartialView("_OrdenCantidad");
        }
        public ActionResult carrito()
        {

            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }

            ViewBag.DetalleOrden = Carrito.Instancia.Product;

            return View();
        }
        public ActionResult Menu()
        {
            IEnumerable<Product> lista = null;
            try
            {
                IServiceProduct _serviceProduct = new ServiceProduct();
                lista = _serviceProduct.GetProducts();
                ViewBag.title = "Lista Productos";
                return View(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AddDiscount(int discount)
        {
            ServiceOrder serviceOrder = new ServiceOrder();
            Order orders = (Order)Session["Order"];
            orders.Discount = (int)serviceOrder.CalculateDiscount((decimal)orders.SubTotal, discount);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveOrder(int payment)
        {
            Order order = (Order)Session["Order"];
            List<Order_Product> OrderProducts = (List<Order_Product>)Session["OrderProducts"];
            ServiceOrder serviceInvoice = new ServiceOrder();
            serviceInvoice.SaveOrder(order, OrderProducts);
            return RedirectToAction("SuccessfulOrder");
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct(Order_Product Order_Product)
        {
            // Get the full product object using the id
            ServiceProduct servicesProduct = new ServiceProduct();
            Product fullProduct = servicesProduct.GetProductByID(Order_Product.ProductID);
            Order_Product.Product = fullProduct;

            // Declare new list
            List<Order_Product> OrderProducts = new List<Order_Product>();

            // Get Product saved in the session
            var orderproductInSession = Session["ProductInvoices"];

            // If there is already Product saved in the session
            if (orderproductInSession != null)
            {
                // Get Product saved in the session
                OrderProducts = (List<Order_Product>)Session["Order_Product"];

                // Check if the product is already in the list
                Order_Product orderProductAlreadyAdded = OrderProducts.FirstOrDefault(p => p.ProductID.Equals(Order_Product.ProductID));

                // If the Product is Already Added
                if (orderProductAlreadyAdded != null)
                {
                    // update the quantity adn update the object
                    orderProductAlreadyAdded.Quantity += Order_Product.Quantity;
                    OrderProducts.Remove(orderProductAlreadyAdded);
                    OrderProducts.Add(orderProductAlreadyAdded);
                }
                // If the order Product is NOT Already Added
                else
                {
                    // add the order Product to the list
                    OrderProducts.Add(Order_Product);
                }
            }
            // If there is NOT already Product saved in the session
            else
            {
                // add the Product to the list
                OrderProducts.Add(Order_Product);
            }

            // Order Service instance to perform operations
            ServiceOrder servicesOrder = new ServiceOrder();

            Order order = new Order();
            order.SubTotal = (int)servicesOrder.CalculateSubtotal(OrderProducts);
            order.Taxes = (int)servicesOrder.CalculateTaxes((decimal)order.SubTotal);

            // Check if the order is already created
            var orderInSession = Session["Order"];

            if (orderInSession != null)
            {
                var auxOrder = (Order)Session["Order"];
                order.Discount = auxOrder.Discount;
                order.Total = (int)servicesOrder.CalculateTotal((decimal)order.SubTotal, (decimal)order.Taxes, (decimal)order.Discount);
            }
            else
            {
                order.Discount = 0;
                order.Total = (int)servicesOrder.CalculateTotal((decimal)order.SubTotal, (decimal)order.Taxes, (decimal)order.Discount);
            }
            Session["Invoice"] = order;
            Session["ProductInvoices"] = OrderProducts;
            return RedirectToAction("Index");
        }
    }
}
