using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProyectoVeterinaria.Models
{
    public class Facturacion
    {
        [Key]
        public int IdFactura { get; set; }

       
        public int ClienteIdCliente { get; set; }
        [ForeignKey(nameof(ClienteIdCliente))]
        public Clientes Cliente { get; set; }

        public int EmpleadoId { get; set; }
        [ForeignKey(nameof(EmpleadoId))]
        public Empleado Empleado { get; set; }


        public string NombreDueño { get; set; }

        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]

        public string Detalle { get; set; }
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        [StringLength(30)]
        public string TipoPago { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El valor debe ser igual o mayor 1")]
        public double Total { get; set; }


    }
}
