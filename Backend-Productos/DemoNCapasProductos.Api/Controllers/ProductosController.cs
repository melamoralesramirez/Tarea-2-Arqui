using DemoNCapasProductos.Application.DTOs;
using DemoNCapasProductos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoNCapasProductos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);

            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            return Ok(producto);
        }

        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerActivos()
        {
            var productos = await _productoService.ObtenerActivosAsync();
            return Ok(productos);
        }

        [HttpGet("stock-bajo")]
        public async Task<IActionResult> ObtenerConStockBajo()
        {
            var productos = await _productoService.ObtenerConStockBajoAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearProductoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var productoCreado = await _productoService.CrearAsync(dto);

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = productoCreado.Id },
                    productoCreado
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var actualizado = await _productoService.ActualizarAsync(id, dto);

                if (!actualizado)
                    return NotFound(new { mensaje = "Producto no encontrado." });

                return Ok(new { mensaje = "Producto actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _productoService.EliminarAsync(id);

            if (!eliminado)
                return NotFound(new { mensaje = "Producto no encontrado." });

            return Ok(new { mensaje = "Producto eliminado correctamente." });
        }
    }
}
