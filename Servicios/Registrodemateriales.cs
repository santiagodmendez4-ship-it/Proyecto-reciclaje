using ProyectoReciclaje.Modelos;

namespace ProyectoReciclaje.Servicios;

public class RegistroMateriales
{
    private List<Material> materiales;

    public RegistroMateriales()
    {
        materiales = new List<Material>();
    }

    public void Registrar(Material material)
    {
        materiales.Add(material);
    }

    public void MostrarMateriales()
    {
        Console.WriteLine("\n===== MATERIALES REGISTRADOS =====");

        if (materiales.Count == 0)
        {
            Console.WriteLine("No hay materiales registrados.");
            return;
        }

        foreach (Material material in materiales)
        {
            Console.WriteLine(
                $"Tipo: {material.Tipo} | Peso: {material.PesoKg} kg"
            );
        }
    }
}