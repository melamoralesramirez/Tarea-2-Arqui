using DemoNCapasProductos.Domain.Entities;
using DemoNCapasProductos.Domain.Interfaces;
using DemoNCapasProductos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNCapasProductos.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Producto>> ObtenerActivosAsync()
        {
            return await _context.Productos
                .Where(p => p.Activo)
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<List<Producto>> ObtenerConStockBajoAsync()
        {
            return await _context.Productos
                .Where(p => p.Activo && p.Stock <= 5)
                .OrderBy(p => p.Stock)
                .ToListAsync();
        }

        public async Task AgregarAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
        }

        public void Actualizar(Producto producto)
        {
            _context.Productos.Update(producto);
        }

        public void Eliminar(Producto producto)
        {
            _context.Productos.Remove(producto);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
