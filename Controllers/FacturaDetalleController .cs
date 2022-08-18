using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoVeterinaria.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoVeterinaria.Controllers
{

    public class FacturaDetalleController : Controller
    {
       
        //registro de facturas
        public FacturaDetalleController(ApplicationDbContext database)
        {
            Database = database;
        }

        public ApplicationDbContext Database;

        public IActionResult Index()
        {
            return View(); //manda los datos como lista
        }


        //Crear nueva factura
        [HttpGet]

        public IActionResult GetDataTabelData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;

                var _GetGridItem = GetGridItem();

                //Buscar
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.IdFacturaDetalle.ToString().ToLower().Contains(searchValue)
                    || obj.Producto.ToString().Contains(searchValue)
                    || obj.ProductoPrecio.ToString().ToLower().Contains(searchValue)
                    || obj.Cantidad.ToString().Contains(searchValue)
                    || obj.Total.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private IQueryable<FacturacionViewModel> GetGridItem()
        {
            try
            {
                return (from FacturaDetalle in Database.FacturaDetalle
                        select new FacturacionViewModel
                        {
                            IdFacturaDetalle = FacturaDetalle.IdFacturaDetalle,
                            Producto = (IEnumerable<SelectListItem>)FacturaDetalle.Producto,
                            ProductoPrecio = (IEnumerable<SelectListItem>)FacturaDetalle.ProductoPrecio,
                            Cantidad = FacturaDetalle.Cantidad,
                            Total = FacturaDetalle.Total,
                        }).OrderByDescending(x => x.IdFacturaDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
    



