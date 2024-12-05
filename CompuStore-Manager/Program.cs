using System;

namespace CompuStoreManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Sistema sistema = new Sistema();
            MenuManager menuManager = new MenuManager(sistema);
            menuManager.MostrarMenuInicial();
        }
    }
}
