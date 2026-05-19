export interface Producto {
  id: number;
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
  activo: boolean;
  fechaCreacion: string;
}

export interface CrearProducto {
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
}

export interface ActualizarProducto {
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
  activo: boolean;
}