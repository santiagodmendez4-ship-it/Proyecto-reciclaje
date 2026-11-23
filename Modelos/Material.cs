namespace ProyectoReciclaje.Modelos;

public abstract class Material
{
    public TipoMaterial Tipo { get; }
    public decimal PesoKg { get; }

    protected Material(TipoMaterial tipo, decimal pesoKg)
    {
        if (pesoKg <= 0)
            throw new ArgumentException("El peso debe ser mayor que cero.");

        Tipo = tipo;
        PesoKg = pesoKg;
    }

    public decimal CalcularValor(decimal precioPorKg)
    {
        if (precioPorKg <= 0)
            throw new ArgumentException("El precio debe ser mayor que cero.");

        return PesoKg * precioPorKg;
    }
}
