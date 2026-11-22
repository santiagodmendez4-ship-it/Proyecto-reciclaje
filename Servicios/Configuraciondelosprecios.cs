using ProyectoReciclaje.Modelos;

namespace ProyectoReciclaje.Servicios;

public class ConfiguracionPrecios
{
    private Dictionary<TipoMaterial, decimal> precios;

    public ConfiguracionPrecios()
    {
        precios = new Dictionary<TipoMaterial, decimal>
        {
            { TipoMaterial.PET, 1000m },
            { TipoMaterial.Vasija, 800m },
            { TipoMaterial.Soplado, 900m },
            { TipoMaterial.Tapas, 1200m },
            { TipoMaterial.Papel, 500m },
            { TipoMaterial.Carton, 600m },
            { TipoMaterial.Cobre, 20000m },
            { TipoMaterial.Aluminio, 5000m }
        };
    }

    public void MostrarPrecios()
    {
        Console.WriteLine("\n===== PRECIOS POR KILOGRAMO =====");

        foreach (var precio in precios)
        {
            Console.WriteLine(
                $"{precio.Key}: ${precio.Value:N2} por kg"
            );
        }
    }

    public bool ModificarPrecio(TipoMaterial tipo, decimal nuevoPrecio)
    {
        if (nuevoPrecio <= 0)
        {
            return false;
        }

        if (!precios.ContainsKey(tipo))
        {
            return false;
        }

        precios[tipo] = nuevoPrecio;

        return true;
    }

    public decimal ObtenerPrecio(TipoMaterial tipo)
    {
        return precios[tipo];
    }
}