namespace ProyectoReciclaje.Modelos;

public class Plastico : Material
{
    public Plastico(TipoMaterial tipo, decimal pesoKg) : base(tipo, pesoKg)
    {
        if (tipo != TipoMaterial.PET && tipo != TipoMaterial.Vasija &&
            tipo != TipoMaterial.Soplado && tipo != TipoMaterial.Tapas)
            throw new ArgumentException("Tipo de plástico no válido.");
    }
}
