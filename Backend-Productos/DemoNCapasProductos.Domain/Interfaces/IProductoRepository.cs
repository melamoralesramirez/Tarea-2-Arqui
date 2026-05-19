using DemoNCapasProductos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNCapasProductos.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<List<Producto>> ObtenerActivosAsync();
        Task<List<Producto>> ObtenerConStockBajoAsync();

        Task AgregarAsync(Producto producto);
        void Actualizar(Producto producto);
        void Eliminar(Producto producto);

        Task GuardarCambiosAsync();
    }
}
