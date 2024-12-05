using System;
using System.Collections.Generic;

namespace CompuStoreManager
{
    public class MenuManager
    {
        private Sistema sistema;
        private Carrito carrito;

        public MenuManager(Sistema sistema)
        {
            this.sistema = sistema;
            this.carrito = new Carrito();
        }

        public void MostrarMenuInicial()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bienvenido a CompuStore-Manager");
                Console.WriteLine("1. Iniciar Sesión");
                Console.WriteLine("2. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        IniciarSesion();
                        break;
                    case "2":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presione Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void IniciarSesion()
        {
            Console.Clear();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Contraseña: ");
            string contraseña = Console.ReadLine();

            if (sistema.IniciarSesion(email, contraseña))
            {
                if (sistema.UsuarioActual.Rol == RolUsuario.CLIENTE)
                {
                    MostrarMenuCliente();
                }
                else if (sistema.UsuarioActual.Rol == RolUsuario.ADMIN)
                {
                    MostrarMenuAdministrador();
                }
            }
            else
            {
                Console.WriteLine("Credenciales incorrectas. Presione Enter para continuar.");
                Console.ReadLine();
            }
        }

        private void MostrarMenuCliente()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido {sistema.UsuarioActual.Nombre}");
                Console.WriteLine("1. Ver catálogo");
                Console.WriteLine("2. Comprar producto");
                Console.WriteLine("3. Ver mis compras");
                Console.WriteLine("4. Cerrar sesión");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        VerCatalogo();
                        break;
                    case "2":
                        ComprarProducto();
                        break;
                    case "3":
                        VerMisCompras();
                        break;
                    case "4":
                        sistema.CerrarSesion();
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Presione Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void MostrarMenuAdministrador()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido {sistema.UsuarioActual.Nombre}");
                Console.WriteLine("1. Gestionar productos");
                Console.WriteLine("2. Ver todas las ventas");
                Console.WriteLine("3. Cerrar sesión");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        GestionarProductos();
                        break;
                    case "2":
                        VerTodasLasVentas();
                        break;
                    case "3":
                        sistema.CerrarSesion();
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Presione Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void VerCatalogo()
        {
            Console.Clear();
            Console.WriteLine("Catálogo de Productos:");
            foreach (var producto in sistema.Productos)
            {
                Console.WriteLine($"ID: {producto.ID}, Nombre: {producto.Nombre}, Precio: {producto.Precio:C}, Stock: {producto.Stock}");
                Console.WriteLine($"Descripción: {producto.Descripción}");
                Console.WriteLine("-----------------------------------");
            }
            Console.WriteLine("Presione Enter para continuar.");
            Console.ReadLine();
        }

        private void ComprarProducto()
        {
            carrito.VaciarCarrito();
            while (true)
            {
                VerCatalogo();
                Console.Write("Ingrese el ID del producto que desea agregar al carrito (0 para finalizar): ");
                int idProducto;
                if (!int.TryParse(Console.ReadLine(), out idProducto))
                {
                    Console.WriteLine("Entrada no válida. Intente nuevamente.");
                    continue;
                }

                if (idProducto == 0)
                {
                    break;
                }

                var producto = sistema.Productos.Find(p => p.ID == idProducto);

                if (producto != null && producto.Stock > 0)
                {
                    carrito.AgregarProducto(producto);
                    Console.WriteLine($"Producto {producto.Nombre} agregado al carrito.");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado o sin stock disponible.");
                }
            }

            if (carrito.Productos.Count > 0)
            {
                Console.WriteLine($"Total a pagar: {carrito.CalcularTotal():C}");
                Console.Write("¿Desea confirmar la compra? (S/N): ");
                string confirmar = Console.ReadLine().ToUpper();
                if (confirmar == "S")
                {
                    try
                    {
                        sistema.RealizarCompra(carrito.Productos);
                        Console.WriteLine("Compra realizada con éxito.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al realizar la compra: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Compra cancelada.");
                }
            }
            else
            {
                Console.WriteLine("No se han agregado productos al carrito.");
            }
            Console.WriteLine("Presione Enter para continuar.");
            Console.ReadLine();
        }

        private void VerMisCompras()
        {
            Console.Clear();
            var compras = sistema.ObtenerComprasUsuario();
            if (compras.Count > 0)
            {
                foreach (var compra in compras)
                {
                    Console.WriteLine($"Compra ID: {compra.ID}, Fecha: {compra.Fecha}, Total: {compra.Total:C}");
                    Console.WriteLine("Productos:");
                    foreach (var producto in compra.Productos)
                    {
                        Console.WriteLine($"- {producto.Nombre} ({producto.Precio:C})");
                    }
                    Console.WriteLine("-----------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No ha realizado ninguna compra.");
            }
            Console.WriteLine("Presione Enter para continuar.");
            Console.ReadLine();
        }

        private void GestionarProductos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gestión de Productos:");
                Console.WriteLine("1. Ver productos");
                Console.WriteLine("2. Agregar producto");
                Console.WriteLine("3. Modificar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        VerCatalogo();
                        break;
                    case "2":
                        AgregarProducto();
                        break;
                    case "3":
                        ModificarProducto();
                        break;
                    case "4":
                        EliminarProducto();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Presione Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void AgregarProducto()
        {
            Console.Clear();
            Console.Write("Nombre del producto: ");
            string nombre = Console.ReadLine();
            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine();
            Console.Write("Precio: ");
            decimal precio;
            while (!decimal.TryParse(Console.ReadLine(), out precio))
            {
                Console.Write("Precio no válido. Ingrese nuevamente: ");
            }
            Console.Write("Stock: ");
            int stock;
            while (!int.TryParse(Console.ReadLine(), out stock))
            {
                Console.Write("Stock no válido. Ingrese nuevamente: ");
            }

            sistema.AgregarProducto(nombre, descripcion, precio, stock);
            Console.WriteLine("Producto agregado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }

        private void ModificarProducto()
        {
            Console.Clear();
            VerCatalogo();
            Console.Write("Ingrese el ID del producto a modificar: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var producto = sistema.Productos.Find(p => p.ID == id);
                if (producto != null)
                {
                    Console.Write("Nuevo nombre (dejar vacío para no modificar): ");
                    string nombre = Console.ReadLine();
                    Console.Write("Nueva descripción (dejar vacío para no modificar): ");
                    string descripcion = Console.ReadLine();
                    Console.Write("Nuevo precio (dejar vacío para no modificar): ");
                    string precioInput = Console.ReadLine();
                    Console.Write("Nuevo stock (dejar vacío para no modificar): ");
                    string stockInput = Console.ReadLine();

                    if (!string.IsNullOrEmpty(nombre))
                        producto.Nombre = nombre;
                    if (!string.IsNullOrEmpty(descripcion))
                        producto.Descripción = descripcion;
                    if (decimal.TryParse(precioInput, out decimal precio))
                        producto.Precio = precio;
                    if (int.TryParse(stockInput, out int stock))
                        producto.Stock = stock;

                    Console.WriteLine("Producto modificado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
            Console.WriteLine("Presione Enter para continuar.");
            Console.ReadLine();
        }

        private void EliminarProducto()
        {
            Console.Clear();
            VerCatalogo();
            Console.Write("Ingrese el ID del producto a eliminar: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                sistema.EliminarProducto(id);
                Console.WriteLine("Producto eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
            Console.WriteLine("Presione Enter para continuar.");
            Console.ReadLine();
        }

        private void VerTodasLasVentas()
        {
            Console.Clear();
            var compras = sistema.ObtenerTodasLasCompras();
            if (compras.Count > 0)
            {
                foreach (var compra in compras)
                {
                    Console.WriteLine($"Compra ID: {compra.ID}, Cliente: {compra.Usuario.Nombre}, Fecha: {compra.Fecha}, Total: {compra.Total:C}");
                    Console.WriteLine("Productos:");
                    foreach (var producto in compra.Productos)
                    {
                        Console.WriteLine($"- {producto.Nombre} ({producto.Precio:C})");
                    }
                    Console.WriteLine("-----------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No se han realizado compras.");
            }
            Console.WriteLine("Presione Enter para continuar.");
            Console.ReadLine();
        }
    }
}
