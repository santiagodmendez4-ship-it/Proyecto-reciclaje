namespace ProyectoReciclaje.Modelos;

public class Venta
{
    public int IdLona { get; }
    public TipoMaterial Tipo { get; }
    public decimal CantidadKg { get; }
    public decimal PrecioVentaPorKg { get; }
    public decimal Total { get; }
    public DateTime Fecha { get; }

    public Venta(int idLona, TipoMaterial tipo, decimal cantidadKg,
                 decimal precioVentaPorKg)
    {
        if (idLona <= 0)
            throw new ArgumentException("El identificador de la lona debe ser positivo.");
        if (cantidadKg <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (precioVentaPorKg <= 0)
            throw new ArgumentException("El precio de venta debe ser mayor que cero.");

        IdLona = idLona;
        Tipo = tipo;
        CantidadKg = cantidadKg;
        PrecioVentaPorKg = precioVentaPorKg;
        Total = cantidadKg * precioVentaPorKg;
        Fecha = DateTime.Now;
    }
}
