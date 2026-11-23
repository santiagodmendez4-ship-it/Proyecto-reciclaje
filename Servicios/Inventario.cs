using ProyectoReciclaje.Modelos;

namespace ProyectoReciclaje.Servicios;

public class Inventario
{
    private readonly RegistroLonas registroLonas;
    private readonly List<Compra> compras = new();
    private readonly List<Venta> ventas = new();

    public IReadOnlyList<Compra> Compras => compras.AsReadOnly();
    public IReadOnlyList<Venta> Ventas => ventas.AsReadOnly();

    public Inventario(RegistroLonas registroLonas)
    {
        this.registroLonas = registroLonas
            ?? throw new ArgumentNullException(nameof(registroLonas));
    }

    public void RegistrarCompra(int idLona, TipoMaterial tipo,
                                decimal cantidadKg, decimal precioCompraPorKg)
    {
        Lona lona = ObtenerLona(idLona);

        // Validamos los datos de la compra antes de modificar el inventario.
        Compra compra = new(idLona, tipo, cantidadKg, precioCompraPorKg);

        // Si la lona rechaza la operación, la compra no se agrega al historial.
        lona.AgregarMaterial(tipo, cantidadKg);
        compras.Add(compra);
    }

    public void RegistrarVenta(int idLona, decimal cantidadKg,
                               decimal precioVentaPorKg)
    {
        Lona lona = ObtenerLona(idLona);

        if (!lona.TipoMaterial.HasValue)
            throw new InvalidOperationException("La lona no tiene material para vender.");

        TipoMaterial tipo = lona.TipoMaterial.Value;

        // Validamos los datos de la venta antes de modificar el inventario.
        Venta venta = new(idLona, tipo, cantidadKg, precioVentaPorKg);

        // Si la lona rechaza la operación, la venta no se agrega al historial.
        lona.RetirarMaterial(cantidadKg);
        ventas.Add(venta);
    }

    public decimal ConsultarExistenciaPorMaterial(TipoMaterial tipo)
    {
        return registroLonas.Lonas
            .Where(l => l.TipoMaterial == tipo)
            .Sum(l => l.PesoActualKg);
    }

    public Lona ConsultarExistenciaPorLona(int idLona) => ObtenerLona(idLona);

    public void MostrarExistenciasPorMaterial()
    {
        Console.WriteLine("\n===== EXISTENCIAS POR MATERIAL =====");
        foreach (TipoMaterial tipo in Enum.GetValues<TipoMaterial>())
            Console.WriteLine($"{tipo}: {ConsultarExistenciaPorMaterial(tipo):N2} kg");
    }

    private Lona ObtenerLona(int id)
    {
        Lona? lona = registroLonas.BuscarPorId(id);
        return lona ?? throw new InvalidOperationException(
            $"No existe una lona con el identificador {id}.");
    }
}
