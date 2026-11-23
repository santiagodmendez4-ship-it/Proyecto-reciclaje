using ProyectoReciclaje.Modelos;

namespace ProyectoReciclaje.Servicios;

public class GeneradorReportes
{
    private readonly Inventario inventario;

    public GeneradorReportes(Inventario inventario)
    {
        this.inventario = inventario
            ?? throw new ArgumentNullException(nameof(inventario));
    }

    public decimal CalcularTotalCompras()
    {
        return inventario.Compras.Sum(compra => compra.Total);
    }

    public decimal CalcularTotalVentas()
    {
        return inventario.Ventas.Sum(venta => venta.Total);
    }

    // Resultado simple: ventas registradas menos compras registradas.
    // No representa utilidad contable definitiva si queda inventario sin vender.
    public decimal CalcularDiferenciaVentasCompras()
    {
        return CalcularTotalVentas() - CalcularTotalCompras();
    }

    public void GenerarReporteCompras()
    {
        Console.WriteLine("\n========== REPORTE DE COMPRAS ==========");
        if (inventario.Compras.Count == 0)
        {
            Console.WriteLine("Todavía no hay compras registradas.");
            return;
        }

        foreach (Compra compra in inventario.Compras)
        {
            Console.WriteLine(
                $"{compra.Fecha:dd/MM/yyyy HH:mm} | Lona {compra.IdLona} | " +
                $"{compra.Tipo} | {compra.CantidadKg:N2} kg | " +
                $"${compra.PrecioCompraPorKg:N2}/kg | Total: ${compra.Total:N2}");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"TOTAL DE COMPRAS: ${CalcularTotalCompras():N2}");
    }

    public void GenerarReporteVentas()
    {
        Console.WriteLine("\n========== REPORTE DE VENTAS ==========");
        if (inventario.Ventas.Count == 0)
        {
            Console.WriteLine("Todavía no hay ventas registradas.");
            return;
        }

        foreach (Venta venta in inventario.Ventas)
        {
            Console.WriteLine(
                $"{venta.Fecha:dd/MM/yyyy HH:mm} | Lona {venta.IdLona} | " +
                $"{venta.Tipo} | {venta.CantidadKg:N2} kg | " +
                $"${venta.PrecioVentaPorKg:N2}/kg | Total: ${venta.Total:N2}");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"TOTAL DE VENTAS: ${CalcularTotalVentas():N2}");
    }

    public void GenerarReporteGanancias()
    {
        decimal compras = CalcularTotalCompras();
        decimal ventas = CalcularTotalVentas();
        decimal diferencia = ventas - compras;

        Console.WriteLine("\n========== RESUMEN FINANCIERO ==========");
        Console.WriteLine($"Total de compras: ${compras:N2}");
        Console.WriteLine($"Total de ventas:  ${ventas:N2}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Diferencia ventas - compras: ${diferencia:N2}");

        if (diferencia > 0)
            Console.WriteLine("Las ventas registradas superan el valor de las compras registradas.");
        else if (diferencia < 0)
            Console.WriteLine("El valor de las compras registradas supera el valor de las ventas.");
        else
            Console.WriteLine("Los totales de compras y ventas son iguales.");

        Console.WriteLine("\nNOTA: Esta diferencia es un resumen simple, no una utilidad contable.");
        Console.WriteLine("Para calcular utilidad real se debe conocer el costo de los kg vendidos");
        Console.WriteLine("y considerar gastos adicionales e inventario pendiente.");
    }

    public void MostrarTodosLosReportes()
    {
        GenerarReporteCompras();
        GenerarReporteVentas();
        GenerarReporteGanancias();
    }
}
