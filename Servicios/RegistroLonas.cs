using ProyectoReciclaje.Modelos;

namespace ProyectoReciclaje.Servicios;

public class RegistroLonas
{
    private readonly List<Lona> lonas = new();
    public IReadOnlyList<Lona> Lonas => lonas.AsReadOnly();

    public bool Registrar(Lona lona)
    {
        if (lona == null) throw new ArgumentNullException(nameof(lona));
        if (lonas.Any(x => x.Id == lona.Id)) return false;
        lonas.Add(lona);
        return true;
    }

    public Lona? BuscarPorId(int id) => lonas.FirstOrDefault(x => x.Id == id);

    public void MostrarLonas()
    {
        Console.WriteLine("\n===== LONAS REGISTRADAS =====");
        if (lonas.Count == 0) { Console.WriteLine("No hay lonas registradas."); return; }
        foreach (Lona lona in lonas)
            Console.WriteLine($"Lona {lona.Id} | Material: {(lona.TipoMaterial?.ToString() ?? "Sin asignar")} | Peso: {lona.PesoActualKg:N2} kg | Capacidad: {lona.CapacidadMaximaKg:N2} kg | Disponible: {lona.CapacidadDisponibleKg:N2} kg");
    }
}
