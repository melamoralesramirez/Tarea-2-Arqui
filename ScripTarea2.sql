CREATE DATABASE demo_ncapas_productos;

USE demo_ncapas_productos;

CREATE TABLE productos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255) NULL,
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE()
);

INSERT INTO productos (nombre, descripcion, precio, stock, activo)
VALUES
('Laptop Lenovo IdeaPad', 'Laptop para uso estudiantil y oficina', 325000.00, 10, 1),
('Mouse Logitech M185', 'Mouse inalámbrico USB', 7500.00, 35, 1),
('Teclado Redragon Kumara', 'Teclado mecánico para computadora', 28500.00, 15, 1),
('Monitor Samsung 24 pulgadas', 'Monitor LED Full HD', 89000.00, 8, 1),
('Audífonos JBL Tune 510BT', 'Audífonos inalámbricos Bluetooth', 36000.00, 20, 1);
GO

--Ver todos los productos
SELECT * FROM productos;

--Ver productos activos
SELECT * FROM productos
WHERE activo = TRUE;

--Ver productos con stock bajo
SELECT * FROM productos
WHERE stock <= 5;

--Buscar producto por id
SELECT * FROM productos
WHERE id = 1;