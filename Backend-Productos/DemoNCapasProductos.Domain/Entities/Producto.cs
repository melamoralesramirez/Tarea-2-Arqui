using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNCapasProductos.Domain.Entities
{
    public class Producto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string? Descripcion { get; private set; }
        public decimal Precio { get; private set; }
        public int Stock { get; private set; }
        public bool Activo { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        protected Producto()
        {
        }

        public Producto(string nombre, string? descripcion, decimal precio, int stock)
        {
            ValidarDatos(nombre, precio, stock);

            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            Activo = true;
            FechaCreacion = DateTime.Now;
        }

        public void Actualizar(string nombre, string? descripcion, decimal precio, int stock)
        {
            ValidarDatos(nombre, precio, stock);

            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
        }

        public void Activar()
        {
            Activo = true;
        }

        public void Desactivar()
        {
            Activo = false;
        }

        public bool TieneStockBajo()
        {
            return Stock <= 5;
        }

        private static void ValidarDatos(string nombre, decimal precio, int stock)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a 0.");

            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
        }

    }
}
