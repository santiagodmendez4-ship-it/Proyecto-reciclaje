namespace ProyectoReciclaje.Modelos;

public class Archivo : Material
{
    public Archivo(TipoMaterial tipo, decimal pesoKg) : base(tipo, pesoKg)
    {
        if (tipo != TipoMaterial.Papel && tipo != TipoMaterial.Carton)
            throw new ArgumentException("Tipo de archivo no válido.");
    }
}
