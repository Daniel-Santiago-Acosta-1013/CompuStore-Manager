using System;

namespace CompuStoreManager
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public Producto(int id, string nombre, string descripción, decimal precio, int stock)
        {
            ID = id;
            Nombre = nombre;
            Descripción = descripción;
            Precio = precio;
            Stock = stock;
        }
    }
}
