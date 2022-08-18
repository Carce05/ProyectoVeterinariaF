namespace ProyectoVeterinaria.Models
{
    public class VerificarUsuario
    {

        public ApplicationDbContext Database;
        public Empleado ValidarUsu(string email, string contra)
        {

            return Database.Empleados.FirstOrDefault(item => item.Email == email && item.contra == contra);

        }
    }
}
