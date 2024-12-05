using System;
using System.Collections.Generic;

namespace CompuStoreManager
{
    public class Compra
    {
        public int ID { get; set; }
        public Usuario Usuario { get; set; }
        public List<Producto> Productos { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public Compra(int id, Usuario usuario, List<Producto> productos)
        {
            ID = id;
            Usuario = usuario;
            Productos = productos;
            Fecha = DateTime.Now;
            Total = CalcularTotal();
        }

        private decimal CalcularTotal()
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
