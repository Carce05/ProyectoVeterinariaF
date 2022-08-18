using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVeterinaria.Models
{
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }

        [Display(Name = "Código")]
        [Required]
        public int codigo { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string descripcion { get; set; }

        [Display(Name = "Marca")]
        [StringLength(100)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string marca { get; set; }

        [Display(Name = "Precio de compra")]
        public decimal precioCompra { get; set; }

        [Display(Name = "Precio de venta")]
        public decimal precioVenta { get; set; }

        [Display(Name = "Unidad de medida")]
        [StringLength(20)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string unidadMedida { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public double cantidad { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [Display(Name = "Proveedor")]
        public int idProveedor { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [ForeignKey(nameof(idProveedor))]
        public Proveedor proveedor { get; set; }
    }
}



