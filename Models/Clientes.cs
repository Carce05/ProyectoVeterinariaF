using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria.Models
{
    public class Clientes
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{1})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "La Cedula es invalida")]
        public int Cedula { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string NombreDueño { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string NombreMascota { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string RazaMascota { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string TipoMascota { get; set; }
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public int Celular { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo es invalido")]
        public string Correo { get; set; }



    }
}