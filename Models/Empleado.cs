using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVeterinaria.Models
{
    public class Empleado
    {
        internal string Correo;

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [RegularExpression(@"^\(?([0-9]{1})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "El formato de la cedula no es permitIdempleadoempleadoo")]
        public int cedula { get; set; }
        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [StringLength(50)]
        public string ApellIdempleadoos { get; set; }
        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [StringLength(8)]
        public string contra { get; set; }
        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Este email no es permitIdempleadoo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [StringLength(20)]

        public string Estado { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [StringLength(20)]

        public string Roles { get; set; }
        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [Display(Name = "Puesto")]
        public int PuestoID { get; set; }
        [Required(ErrorMessage = "Este campo no puede estar en blanco")]
        [ForeignKey(nameof(PuestoID))]
        public Puesto Puesto { get; set; }
    }
}
