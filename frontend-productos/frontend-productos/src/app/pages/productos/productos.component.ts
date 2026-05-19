import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Producto, CrearProducto, ActualizarProducto } from '../../models/producto.model';
import { ProductoService } from '../../core/services/producto.service';

@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.css'
})
export class ProductosComponent implements OnInit {
  productos: Producto[] = [];

  productoForm: CrearProducto & { activo?: boolean } = {
    nombre: '',
    descripcion: '',
    precio: 0,
    stock: 0,
    activo: true
  };

  editando = false;
  productoEditandoId: number | null = null;

  mensaje = '';
  error = '';

  constructor(private productoService: ProductoService) {}

  ngOnInit(): void {
    this.cargarProductos();
  }

  cargarProductos(): void {
    this.limpiarMensajes();

    this.productoService.obtenerTodos().subscribe({
      next: (data) => {
        this.productos = data;
      },
      error: () => {
        this.error = 'Error al cargar los productos.';
      }
    });
  }

  cargarActivos(): void {
    this.limpiarMensajes();

    this.productoService.obtenerActivos().subscribe({
      next: (data) => {
        this.productos = data;
        this.mensaje = 'Mostrando productos activos.';
      },
      error: () => {
        this.error = 'Error al cargar los productos activos.';
      }
    });
  }

  cargarStockBajo(): void {
    this.limpiarMensajes();

    this.productoService.obtenerStockBajo().subscribe({
      next: (data) => {
        this.productos = data;
        this.mensaje = 'Mostrando productos con stock bajo.';
      },
      error: () => {
        this.error = 'Error al cargar los productos con stock bajo.';
      }
    });
  }

  guardarProducto(): void {
    this.limpiarMensajes();

    if (!this.validarFormulario()) {
      return;
    }

    if (this.editando && this.productoEditandoId !== null) {
      const productoActualizado: ActualizarProducto = {
        nombre: this.productoForm.nombre,
        descripcion: this.productoForm.descripcion,
        precio: this.productoForm.precio,
        stock: this.productoForm.stock,
        activo: this.productoForm.activo ?? true
      };

      this.productoService.actualizar(this.productoEditandoId, productoActualizado).subscribe({
        next: () => {
          this.mensaje = 'Producto actualizado correctamente.';
          this.cancelarEdicion();
          this.cargarProductos();
        },
        error: () => {
          this.error = 'Error al actualizar el producto.';
        }
      });

      return;
    }

    const nuevoProducto: CrearProducto = {
      nombre: this.productoForm.nombre,
      descripcion: this.productoForm.descripcion,
      precio: this.productoForm.precio,
      stock: this.productoForm.stock
    };

    this.productoService.crear(nuevoProducto).subscribe({
      next: () => {
        this.mensaje = 'Producto creado correctamente.';
        this.limpiarFormulario();
        this.cargarProductos();
      },
      error: () => {
        this.error = 'Error al crear el producto.';
      }
    });
  }

  editarProducto(producto: Producto): void {
    this.editando = true;
    this.productoEditandoId = producto.id;

    this.productoForm = {
      nombre: producto.nombre,
      descripcion: producto.descripcion ?? '',
      precio: producto.precio,
      stock: producto.stock,
      activo: producto.activo
    };
  }

  eliminarProducto(id: number): void {
    const confirmar = confirm('¿Seguro que desea eliminar este producto?');

    if (!confirmar) {
      return;
    }

    this.limpiarMensajes();

    this.productoService.eliminar(id).subscribe({
      next: () => {
        this.mensaje = 'Producto eliminado correctamente.';
        this.cargarProductos();
      },
      error: () => {
        this.error = 'Error al eliminar el producto.';
      }
    });
  }

  cancelarEdicion(): void {
    this.editando = false;
    this.productoEditandoId = null;
    this.limpiarFormulario();
  }

  limpiarFormulario(): void {
    this.productoForm = {
      nombre: '',
      descripcion: '',
      precio: 0,
      stock: 0,
      activo: true
    };
  }

  limpiarMensajes(): void {
    this.mensaje = '';
    this.error = '';
  }

  validarFormulario(): boolean {
    if (!this.productoForm.nombre.trim()) {
      this.error = 'El nombre es obligatorio.';
      return false;
    }

    if (this.productoForm.precio <= 0) {
      this.error = 'El precio debe ser mayor a 0.';
      return false;
    }

    if (this.productoForm.stock < 0) {
      this.error = 'El stock no puede ser negativo.';
      return false;
    }

    return true;
  }
}