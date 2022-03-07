namespace CBR_Conecta_API.Models.Requests
{
    public class UsuarioRequests
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Privilegios { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Puesto { get; set; }
        public string Departamento { get; set; }
        public string Email { get; set; }
    }
}
