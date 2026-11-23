# Epic 69 - Reportes de compras, ventas y ganancias

## Archivos agregados
- Modelos/Compra.cs
- Modelos/Venta.cs
- Servicios/GeneradorReportes.cs

## Archivos modificados
- Servicios/Inventario.cs: conserva el historial de compras y ventas.
- Menus/Menuprincipal.cs: agrega el menú de reportes y solicita el precio de cada operación.

## Cálculos
- Total de compras = suma de cantidad comprada × costo por kg.
- Total de ventas = suma de cantidad vendida × precio de venta por kg.
- Diferencia simple = total de ventas - total de compras.

La diferencia simple NO es utilidad contable definitiva cuando queda inventario sin vender.
Para una utilidad real se debe calcular el costo de los productos efectivamente vendidos
(por ejemplo, con costo promedio ponderado o FIFO) y considerar otros gastos.

Los historiales se conservan en memoria durante la ejecución. Se pierden al cerrar el programa.
