using System;
using System.Collections.Generic;
using System.Linq;

namespace CompuStoreManager
{
    public class Sistema
    {
        public List<Usuario> Usuarios { get; private set; }
        public List<Producto> Productos { get; private set; }
        public List<Compra> Compras { get; private set; }
        public Usuario UsuarioActual { get; private set; }

        private int productoIdCounter = 1;
        private int compraIdCounter = 1;

        public Sistema()
        {
            Usuarios = new List<Usuario>();
            Productos = new List<Producto>();
            Compras = new List<Compra>();
            PrecargarDatos();
        }

        private void PrecargarDatos()
        {
            // Usuarios preestablecidos
            Usuarios.Add(new Usuario("admin@example.com", "admin123", "Administrador", RolUsuario.ADMIN));
            Usuarios.Add(new Usuario("cliente1@example.com", "cliente123", "Cliente Uno", RolUsuario.CLIENTE));

            // Productos iniciales
            Productos.Add(new Producto(productoIdCounter++, "Laptop X", "Laptop de alta performance", 1500.00m, 10));
            Productos.Add(new Producto(productoIdCounter++, "Desktop Y", "PC de escritorio para gaming", 2000.00m, 5));
            Productos.Add(new Producto(productoIdCounter++, "Tablet Z", "Tablet ligera y portátil", 500.00m, 20));
        }

        public bool IniciarSesion(string email, string contraseña)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Email == email && u.Contraseña == contraseña);
            if (usuario != null)
            {
                UsuarioActual = usuario;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
        }

        // Gestión de productos
        public void AgregarProducto(string nombre, string descripcion, decimal precio, int stock)
        {
            Productos.Add(new Producto(productoIdCounter++, nombre, descripcion, precio, stock));
        }

        public void EliminarProducto(int id)
        {
            var producto = Productos.FirstOrDefault(p => p.ID == id);
            if (producto != null)
            {
                Productos.Remove(producto);
            }
        }

        public void ActualizarProducto(int id, string nombre, string descripcion, decimal precio, int stock)
        {
            var producto = Productos.FirstOrDefault(p => p.ID == id);
            if (producto != null)
            {
                producto.Nombre = nombre;
                producto.Descripción = descripcion;
                producto.Precio = precio;
                producto.Stock = stock;
            }
        }

        // Gestión de compras
        public void RealizarCompra(List<Producto> productos)
        {
            foreach (var producto in productos)
            {
                var prod = Productos.FirstOrDefault(p => p.ID == producto.ID);
                if (prod != null && prod.Stock >= 1)
                {
                    prod.Stock--;
                }
                else
                {
                    throw new Exception($"El producto {producto.Nombre} no tiene stock suficiente.");
                }
            }

            Compras.Add(new Compra(compraIdCounter++, UsuarioActual, new List<Producto>(productos)));
        }

        public List<Compra> ObtenerComprasUsuario()
        {
            return Compras.Where(c => c.Usuario.Email == UsuarioActual.Email).ToList();
        }

        public List<Compra> ObtenerTodasLasCompras()
        {
            return Compras;
        }
    }
}
