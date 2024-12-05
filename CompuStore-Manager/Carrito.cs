// Carrito.cs
using System.Collections.Generic;

namespace CompuStoreManager
{
    public class Carrito
    {
        public List<Producto> Productos { get; set; }

        public Carrito()
        {
            Productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            Productos.Add(producto);
        }

        public void EliminarProducto(Producto producto)
        {
            Productos.Remove(producto);
        }

        public void VaciarCarrito()
        {
            Productos.Clear();
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var producto in Productos)
            {
                total += producto.Precio;
            }
            return total;
        }
    }
}
