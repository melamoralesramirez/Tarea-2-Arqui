using DemoNCapasProductos.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNCapasProductos.Application.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> ObtenerTodosAsync();
        Task<ProductoDto?> ObtenerPorIdAsync(int id);
        Task<List<ProductoDto>> ObtenerActivosAsync();
        Task<List<ProductoDto>> ObtenerConStockBajoAsync();

        Task<ProductoDto> CrearAsync(CrearProductoDto dto);
        Task<bool> ActualizarAsync(int id, ActualizarProductoDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
