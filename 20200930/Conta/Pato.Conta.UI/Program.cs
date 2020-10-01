using System;
using Pato.Conta.Data;
using Pato.Conta.Model;

namespace Pato.Conta.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductD pD = new ProductD();

            var lista = pD.GetList();

            foreach (var p in lista)
            {
                Console.WriteLine($"Id: {p.Id}, Nombre: {p.Nombre}, Precio: {p.Precio}");
            }

            Console.ReadLine();
        }
    }
}
