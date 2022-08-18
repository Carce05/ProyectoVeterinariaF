using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria.Models
{
    public class Puesto
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(100)]
        public string puesto { get; set; }


    }
}
