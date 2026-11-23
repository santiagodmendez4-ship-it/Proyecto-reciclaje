namespace ProyectoReciclaje.Modelos;

public class Lona
{
    public int Id { get; }
    public decimal CapacidadMaximaKg { get; }
    public decimal PesoActualKg { get; private set; }
    public TipoMaterial? TipoMaterial { get; private set; }
    public decimal CapacidadDisponibleKg => CapacidadMaximaKg - PesoActualKg;

    public Lona(int id, decimal capacidadMaximaKg)
    {
        if (id <= 0) throw new ArgumentException("El identificador debe ser positivo.");
        if (capacidadMaximaKg <= 0) throw new ArgumentException("La capacidad debe ser mayor que cero.");
        Id = id;
        CapacidadMaximaKg = capacidadMaximaKg;
        PesoActualKg = 0;
        TipoMaterial = null;
    }

    public void AgregarMaterial(TipoMaterial tipo, decimal cantidadKg)
    {
        if (cantidadKg <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (TipoMaterial.HasValue && TipoMaterial.Value != tipo)
            throw new InvalidOperationException($"La lona {Id} contiene {TipoMaterial.Value}; no se puede mezclar con {tipo}.");
        if (cantidadKg > CapacidadDisponibleKg)
            throw new InvalidOperationException($"Capacidad insuficiente. Disponible: {CapacidadDisponibleKg:N2} kg.");
        TipoMaterial = tipo;
        PesoActualKg += cantidadKg;
    }

    public void RetirarMaterial(decimal cantidadKg)
    {
        if (cantidadKg <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (!TipoMaterial.HasValue || PesoActualKg == 0)
            throw new InvalidOperationException("La lona no tiene existencias.");
        if (cantidadKg > PesoActualKg)
            throw new InvalidOperationException($"Existencias insuficientes. Disponible: {PesoActualKg:N2} kg.");
        PesoActualKg -= cantidadKg;
        if (PesoActualKg == 0) TipoMaterial = null;
    }
}
