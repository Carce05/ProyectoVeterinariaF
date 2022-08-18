namespace ProyectoVeterinaria.Models
{
    public class ConfiguracionSmtp
    {
        public string Remitente { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Servidor { get; set; }
        public int Puerto { get; set; }
    }
}
