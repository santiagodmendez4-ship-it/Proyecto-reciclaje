namespace ProyectoReciclaje.Modelos;

public class Metal : Material
{
    public Metal(TipoMaterial tipo, decimal pesoKg) : base(tipo, pesoKg)
    {
        if (tipo != TipoMaterial.Cobre && tipo != TipoMaterial.Aluminio)
            throw new ArgumentException("Tipo de metal no válido.");
    }
}
