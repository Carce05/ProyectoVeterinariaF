using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoVeterinaria.Models;
using ProyectoVeterinaria.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoVeterinaria.Controllers
{

    public class FacturacionController : Controller
    {



        List<FacturacionViewModel> listaFD = new List<FacturacionViewModel>();
        
        //registro de facturas
        public FacturacionController(ApplicationDbContext database)
        {
            Database = database;
        }

        public ApplicationDbContext Database;
       

        public IActionResult Index()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult ConsultarFacturas(string buscar)
        {

            var facturas = from facturacion  in Database.Facturas select facturacion;
            if (!string.IsNullOrEmpty(buscar))
            {
                facturas = facturas.Where(c => c.NombreDueño.Contains(buscar));
            }

            return View(facturas); //Envia los datos de las facturas como una lista
        }




        [HttpGet]

        public IActionResult ObtenerProductos(int Id)
        {
            Producto result = (from Producto in Database.Producto
                            where Producto.idProducto == Id
                            select new Producto
                            {
                                idProducto = Producto.idProducto,
                                codigo = Producto.codigo,
                                descripcion = Producto.descripcion,
                                marca = Producto.marca,
                                precioCompra = Producto.precioCompra,
                                precioVenta = Producto.precioVenta
                            }).SingleOrDefault();
            return new JsonResult(result);
        }

        public Producto ConsultarProducto(int Id)
        {
            Producto producto = new Producto();
            var result = (from productos in Database.Producto where productos.idProducto == Id select productos).FirstOrDefault();
            producto.idProducto = result.idProducto;
            producto.precioCompra = result.precioCompra;
            producto.precioVenta = result.precioVenta;
            producto.descripcion = result.descripcion;
            producto.idProveedor = result.idProveedor;
            producto.marca = result.marca;
            producto.codigo = result.codigo;
            producto.unidadMedida = result.unidadMedida;

            return producto; //Envia los datos de las facturas como una lista
        }

        [HttpPost]
        public IActionResult LlenarFD(FacturacionViewModel factura)
        {
           
            Producto producto = ConsultarProducto(factura.FacturaDetalle.ProductoidProducto);
            factura.Producto = new SelectList(Database.Producto, "idProducto", "idProducto");
           

            FacturaDetalle facturaD = new FacturaDetalle
            {
                ProductoidProducto = producto.idProducto,
                ProductoPrecioVenta = (int)producto.precioVenta,
                Cantidad = factura.FacturaDetalle.Cantidad,
                Total = (double)(factura.FacturaDetalle.Cantidad * producto.precioVenta)
            };

            factura.FacturaDetalle = facturaD;

            listaFD.Add(factura);
           
           return View("RegistrarF", factura);
            
        }

        [HttpPost]
        public IActionResult AgregarFD(FacturacionViewModel c)
        {

            FacturaDetalle factura =
               c.FacturaDetalle.IdFacturaDetalle > 0
               ? Database.FacturaDetalle.FirstOrDefault(x => x.IdFacturaDetalle == c.FacturaDetalle.IdFacturaDetalle)
               : new FacturaDetalle();
            if (factura == null)
            { return NotFound(); }
            factura.IdFacturaDetalle = c.FacturaDetalle.IdFacturaDetalle;
            factura.ProductoidProducto = c.FacturaDetalle.ProductoidProducto;
            factura.ProductoPrecioVenta = c.FacturaDetalle.ProductoPrecioVenta;
            factura.Cantidad = c.FacturaDetalle.Cantidad;
            factura.Total = c.FacturaDetalle.Total;



            if (factura.IdFacturaDetalle == 0)
            {
                Database.FacturaDetalle.Add(factura);
            }
            Database.SaveChanges();
            return new JsonResult(factura);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EliminarFD(int? idFacturaDetalle)
        {
            FacturacionViewModel model = new FacturacionViewModel();
            model.FacturaDetalle = Database.FacturaDetalle.FirstOrDefault(x => x.IdFacturaDetalle == idFacturaDetalle);
            //Llamados de las listas desplegables
            model.Producto = new SelectList(Database.Clientes, "IdProducto", "Producto");
            model.ProductoPrecio = new SelectList(Database.Empleados, "PrecioVenta", "ProductoPrecio");
            return View(model);
        }

        //eliminar una factura

        [HttpPost]
        public IActionResult EliminarFD(FacturacionViewModel c)
        {


            Database.FacturaDetalle.Remove(c.FacturaDetalle);
            Database.SaveChanges();
            return RedirectToAction(nameof(Index));



        }




        [HttpGet]
        public IActionResult RegistrarF(int idFactura)
        {
            FacturacionViewModel model = new FacturacionViewModel();
            model.Facturacion = Database.Facturas.FirstOrDefault(x => x.IdFactura == idFactura);
            model.Producto = new SelectList(Database.Producto, "idProducto", "idProducto");
            model.Cedula = new SelectList(Database.Clientes, "IdCliente", "Cedula");
            model.CedulaEmpleado = new SelectList(Database.Empleados, "Id", "cedula");


            return View(model);
        }

        [HttpPost]
        public IActionResult RegistrarF(FacturacionViewModel c)
        {

            // Llamar a la Base de datos 
            Facturacion factura =
                     c.Facturacion.IdFactura > 0
                ? Database.Facturas.FirstOrDefault(x => x.IdFactura == c.Facturacion.IdFactura)
                : new Facturacion();
            if (factura == null)
            { return NotFound(); }
            factura.IdFactura = c.Facturacion.IdFactura;
            factura.ClienteIdCliente = c.Facturacion.ClienteIdCliente;
            factura.NombreDueño = c.Facturacion.NombreDueño;
            factura.EmpleadoId = c.Facturacion.EmpleadoId;
            factura.Detalle = c.Facturacion.Detalle;
            factura.TipoPago = c.Facturacion.TipoPago;
            factura.Total = c.Facturacion.Total;



            if (factura.IdFactura == 0)
            {
                Database.Facturas.Add(factura);
            }
            Database.SaveChanges();
            return RedirectToAction(nameof(ConsultarFacturas));
        }


        //Editar Factura
        [HttpGet]
        public IActionResult Editar(int? idFactura)
        {
            FacturacionViewModel model = new FacturacionViewModel();
            model.Facturacion = Database.Facturas.FirstOrDefault(x => x.IdFactura == idFactura);
            //llamado del comobox
            model.Cedula = new SelectList(Database.Clientes, "IdCliente", "Cedula");
            model.CedulaEmpleado = new SelectList(Database.Empleados, "Id", "cedula");

            return View(model);
        }

        //Mod factura completo

        [HttpPost]
        public IActionResult Editar(FacturacionViewModel c)
        {


            Database.Facturas.Update(c.Facturacion);
            Database.SaveChanges();
            TempData["mensaje"] = "Se editó con exito";
            return RedirectToAction(nameof(ConsultarFacturas));



        }

        public IActionResult EliminarF(int? idFactura)
        {
            FacturacionViewModel model = new FacturacionViewModel();
            model.Facturacion = Database.Facturas.FirstOrDefault(x => x.IdFactura == idFactura);
            //Llamados de las listas desplegables
            model.Cedula = new SelectList(Database.Clientes, "IdCliente", "Cedula");
            model.CedulaEmpleado = new SelectList(Database.Empleados, "Id", "cedula");
            return View(model);
        }

        //eliminar una factura

        [HttpPost]
        public IActionResult EliminarF(FacturacionViewModel c)
        {


            Database.Facturas.Remove(c.Facturacion);
            Database.SaveChanges();
            return RedirectToAction(nameof(Index));



        }

        private List<FacturaDetalle> ObtenerListaFD(int idFacturaDetalle)
        {
            List<FacturaDetalle> ListaFD = (from obj in Database.FacturaDetalle
                                            where (obj.IdFacturaDetalle == idFacturaDetalle)
                                            select obj).ToList();
            return ListaFD;
        }
    }
}

