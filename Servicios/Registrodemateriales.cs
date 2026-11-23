using ProyectoReciclaje.Modelos;

namespace ProyectoReciclaje.Servicios;

public class RegistroMateriales
{
    private readonly List<Material> materiales = new();

    public void Registrar(Material material)
    {
        if (material == null) throw new ArgumentNullException(nameof(material));
        materiales.Add(material);
    }

    public void MostrarMateriales()
    {
        Console.WriteLine("\n===== MATERIALES REGISTRADOS =====");
        if (materiales.Count == 0) { Console.WriteLine("No hay materiales registrados."); return; }
        foreach (Material material in materiales)
            Console.WriteLine($"Tipo: {material.Tipo} | Peso: {material.PesoKg:N2} kg");
    }
}
