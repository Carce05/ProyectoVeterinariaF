using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVeterinaria.Models
{
    public class FacturaDetalle
    {
        [Key]
        public int IdFacturaDetalle { get; set; }


        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]

        public string NombreDueño { get; set; }

       
        public int ProductoidProducto { get; set; }

        [ForeignKey(nameof(ProductoidProducto))]
        public Producto Producto { get; set; }

        public int ProductoPrecioVenta { get; set; }
        public Producto ProductoPrecio { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser igual o mayor 1")]
        public int Cantidad { get; set; }

        //[Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        // [StringLength(30)]


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El valor debe ser igual o mayor 1")]

        public double Total { get; set; }
       
    }
}
