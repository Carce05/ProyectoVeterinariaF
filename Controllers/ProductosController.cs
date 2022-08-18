using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinaria.Models;

namespace ProyectoVeterinaria.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
      
        //Registro de los productos
        public ProductosController(ApplicationDbContext database)
        {
            Database = database;
        }

        public ApplicationDbContext Database;

        [HttpGet]
        public IActionResult BuscarProductos(string buscar)
        {
            IQueryable<Producto> productosEncontrados;

            var productos = from producto in Database.Producto select producto;
            if (!string.IsNullOrEmpty(buscar))
            {
                productosEncontrados = productos.Where(c => c.descripcion.Contains(buscar) || c.codigo.ToString().Contains(buscar));

                if (productosEncontrados.Count() > 0)
                {
                    productos = productosEncontrados;
                    TempData["EncontroProductos"] = true;
                }
                else
                {
                    @TempData["Mensaje"] = "No se encontrarón productos para \"" + buscar + "\"";
                }
            }

            return View("ConsultarProductos",productos); //Envia los datos de los productos como una lista
        }

        [Authorize(Roles = "Administrador, Veterinario,Inventario")] //para el acceso de seguridad
        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            IEnumerable<Producto> productos;
            productos = Database.Producto.ToList().Select(x => new Producto()
            {
                cantidad = x.cantidad,
                codigo = x.codigo,
                descripcion = x.descripcion,
                idProducto = x.idProducto,
                idProveedor = x.idProveedor,
                marca = x.marca,
                precioCompra = x.precioCompra,
                precioVenta = x.precioVenta,
                unidadMedida = x.unidadMedida,
                proveedor = Database.Proveedor.FirstOrDefault(y => y.idProveedor == x.idProveedor)
            }).ToList();
            return View(productos); //Envia los datos de los productos como una lista
        }

        [Authorize(Roles = "Administrador,Inventario")] //para el acceso de seguridad
        //Registrar un Producto
        [HttpGet]
        public IActionResult RegistrarProductos(int idProducto)
        {
            ProductoViewModel model = new ProductoViewModel();
            model.Producto = Database.Producto.FirstOrDefault(x => x.idProducto == idProducto);
            //llamado del comobox
            model.Proveedores = new SelectList(Database.Proveedor, "idProveedor", "nombre");
            return View(model);
        }
        [Authorize(Roles = "Administrador,Inventario")] //para el acceso de seguridad
        [HttpGet]
        public IActionResult EditarProductos(int idProducto)
        {
            ProductoViewModel model = new ProductoViewModel();
            model.Producto = Database.Producto.FirstOrDefault(x => x.idProducto == idProducto);
            //llamado del comobox
            model.Proveedores = new SelectList(Database.Proveedor, "idProveedor", "nombre");
            return View(model);

            var producto = Database.Producto.FirstOrDefault(x => x.idProducto == idProducto);

            return View(producto);
        }
        [Authorize(Roles = "Administrador,Inventario")] //para el acceso de seguridad
        [HttpGet]
        public IActionResult EliminarProductos(int idProducto)
        {
            ProductoViewModel model = new ProductoViewModel();
            model.Producto = Database.Producto.FirstOrDefault(x => x.idProducto == idProducto);
            //llamado del comobox
            model.Proveedores = new SelectList(Database.Proveedor, "idProveedor", "nombre");
            return View(model);

            var producto = Database.Producto.FirstOrDefault(x => x.idProducto == idProducto);

            return View(producto);
        }

        //Registrar un producto
        [HttpPost]
        public IActionResult RegistrarProducto(ProductoViewModel c)
        {
            Producto producto =
                c.Producto.idProducto > 0
                ? Database.Producto.FirstOrDefault(x => x.idProducto == c.Producto.idProducto)
                : new Producto();
            if (producto == null)
            { return NotFound(); }
            producto.idProducto = c.Producto.idProducto;
            producto.codigo = c.Producto.codigo;
            producto.descripcion = c.Producto.descripcion;
            producto.marca = c.Producto.marca;
            producto.precioCompra = c.Producto.precioCompra;
            producto.precioVenta = c.Producto.precioVenta;
            producto.unidadMedida = c.Producto.unidadMedida;
            producto.idProveedor = c.Producto.idProveedor;
            producto.cantidad = c.Producto.cantidad;
            {
                Database.Producto.Add(producto);
            }
            Database.SaveChanges();
            TempData["Mensaje"] = "El producto se registro con éxito";
            return RedirectToAction(nameof(ConsultarProductos));
        }

        [HttpPost]
        public IActionResult EditarProducto(ProductoViewModel c)
        {

            Database.Producto.Update(c.Producto);
            Database.SaveChanges();
            TempData["mensaje"] = "El producto se modifico con éxito";
            return RedirectToAction(nameof(ConsultarProductos));
        }

        [HttpPost]
        public IActionResult EliminarProducto(ProductoViewModel c)
        {
            Database.Attach(c.Producto);
            Database.Entry(c.Producto).State = EntityState.Deleted;
            Database.SaveChanges();
            TempData["Mensaje"] = "El producto se elimino con éxito";
            return RedirectToAction(nameof(ConsultarProductos));
        }
    }
}