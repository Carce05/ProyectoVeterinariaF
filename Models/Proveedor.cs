using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria.Models
{
    public class Proveedor
    {
        [Key]
        public int idProveedor { get; set; }

        [Display(Name = "Cédula")]
        [Required]
        [RegularExpression(@"^\(?([0-9]{1})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$", ErrorMessage = "La cédula es invalida")]
        public int cedula { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string nombre { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string telefono { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        public string direccion { get; set; }

        [Display(Name = "Correo")]
        [StringLength(30)]
        [Required(ErrorMessage = "No se puede dejar espacios en blanco")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo es invalido")]
        public string correo { get; set; }



    }
}


