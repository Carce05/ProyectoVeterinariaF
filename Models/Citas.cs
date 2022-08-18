using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVeterinaria.Models
{
    public class Citas
    {
        [Key]
        public int IdCita { get; set; }

        public int ClienteIdCliente { get; set; }

        [ForeignKey(nameof(ClienteIdCliente))]
        public Clientes Cliente { get; set; }

        public int EmpleadoId { get; set; }

        [ForeignKey(nameof(EmpleadoId))]
        public Empleado Empleado { get; set; }


        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string NombreDueño { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string NombreVeterinario { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string RazaMascota { get; set; }

        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]

        [StringLength(30)]
        public string EstadoMascota { get; set; }

        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string HoraCita { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string MotivoCita { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string NombreServicio { get; set; }

    }
}