using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVeterinaria.Models
{
    public class CorreoElectronico
    {
        [Key]
        public int IdCorreo { get; set; }
        public int IdCliente { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Clientes Cliente { get; set; }
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
    }
}
