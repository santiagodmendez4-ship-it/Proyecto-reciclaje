using ProyectoReciclaje.Modelos;

namespace ProyectoReciclaje.Modelos;

public class Compra
{
    public int IdLona { get; }
    public TipoMaterial Tipo { get; }
    public decimal CantidadKg { get; }
    public decimal PrecioCompraPorKg { get; }
    public decimal Total { get; }
    public DateTime Fecha { get; }

    public Compra(int idLona, TipoMaterial tipo, decimal cantidadKg,
                  decimal precioCompraPorKg)
    {
        if (idLona <= 0)
            throw new ArgumentException("El identificador de la lona debe ser positivo.");
        if (cantidadKg <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (precioCompraPorKg <= 0)
            throw new ArgumentException("El precio de compra debe ser mayor que cero.");

        IdLona = idLona;
        Tipo = tipo;
        CantidadKg = cantidadKg;
        PrecioCompraPorKg = precioCompraPorKg;
        Total = cantidadKg * precioCompraPorKg;
        Fecha = DateTime.Now;
    }
}
