using DemoNCapasProductos.Application.DTOs;
using DemoNCapasProductos.Application.Interfaces;
using DemoNCapasProductos.Domain.Entities;
using DemoNCapasProductos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNCapasProductos.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<ProductoDto>> ObtenerTodosAsync()
        {
            var productos = await _productoRepository.ObtenerTodosAsync();

            return productos.Select(MapearADto).ToList();
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);

            if (producto == null)
                return null;

            return MapearADto(producto);
        }

        public async Task<List<ProductoDto>> ObtenerActivosAsync()
        {
            var productos = await _productoRepository.ObtenerActivosAsync();

            return productos.Select(MapearADto).ToList();
        }

        public async Task<List<ProductoDto>> ObtenerConStockBajoAsync()
        {
            var productos = await _productoRepository.ObtenerConStockBajoAsync();

            return productos.Select(MapearADto).ToList();
        }

        public async Task<ProductoDto> CrearAsync(CrearProductoDto dto)
        {
            var producto = new Producto(
                dto.Nombre,
                dto.Descripcion,
                dto.Precio,
                dto.Stock
            );

            await _productoRepository.AgregarAsync(producto);
            await _productoRepository.GuardarCambiosAsync();

            return MapearADto(producto);
        }

        public async Task<bool> ActualizarAsync(int id, ActualizarProductoDto dto)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);

            if (producto == null)
                return false;

            producto.Actualizar(
                dto.Nombre,
                dto.Descripcion,
                dto.Precio,
                dto.Stock
            );

            if (dto.Activo)
                producto.Activar();
            else
                producto.Desactivar();

            _productoRepository.Actualizar(producto);
            await _productoRepository.GuardarCambiosAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);

            if (producto == null)
                return false;

            _productoRepository.Eliminar(producto);
            await _productoRepository.GuardarCambiosAsync();

            return true;
        }

        private static ProductoDto MapearADto(Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Activo = producto.Activo,
                FechaCreacion = producto.FechaCreacion
            };
        }
    }
}
