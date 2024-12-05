using System;

namespace CompuStoreManager
{
    public enum RolUsuario
    {
        ADMIN,
        CLIENTE
    }

    public class Usuario
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public RolUsuario Rol { get; set; }

        public Usuario(string email, string contraseña, string nombre, RolUsuario rol)
        {
            Email = email;
            Contraseña = contraseña;
            Nombre = nombre;
            Rol = rol;
        }
    }
}
