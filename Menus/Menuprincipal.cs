using ProyectoReciclaje.Modelos;
using ProyectoReciclaje.Servicios;
using System.Globalization;

namespace ProyectoReciclaje.Menus;

public class MenuPrincipal
{
    private ConfiguracionPrecios configuracionPrecios;
    private RegistroMateriales registroMateriales;

    public MenuPrincipal()
    {
        configuracionPrecios = new ConfiguracionPrecios();
        registroMateriales = new RegistroMateriales();
    }

    public void Mostrar()
    {
        int opcion;

        do
        {
            Console.WriteLine("\n================================");
            Console.WriteLine("       SISTEMA DE RECICLAJE");
            Console.WriteLine("================================");
            Console.WriteLine("1. Registrar material");
            Console.WriteLine("2. Configurar precios");
            Console.WriteLine("3. Mostrar materiales");
            Console.WriteLine("4. Salir");

            opcion = LeerEntero("Seleccione una opción: ");

            switch (opcion)
            {
                case 1:
                    RegistrarMaterial();
                    break;

                case 2:
                    MenuPrecios();
                    break;

                case 3:
                    registroMateriales.MostrarMateriales();
                    break;

                case 4:
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

        } while (opcion != 4);
    }

    private void MenuPrecios()
    {
        int opcion;

        do
        {
            Console.WriteLine("\n===== CONFIGURACIÓN DE PRECIOS =====");
            Console.WriteLine("1. Mostrar precios");
            Console.WriteLine("2. Modificar precio");
            Console.WriteLine("3. Volver al menú principal");

            opcion = LeerEntero("Seleccione una opción: ");

            switch (opcion)
            {
                case 1:
                    configuracionPrecios.MostrarPrecios();
                    break;

                case 2:
                    ModificarPrecio();
                    break;

                case 3:
                    Console.WriteLine("Volviendo al menú principal...");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

        } while (opcion != 3);
    }

    private void ModificarPrecio()
    {
        MostrarTiposMateriales();

        int opcion = LeerEntero("Seleccione el material: ");

        if (!Enum.IsDefined(typeof(TipoMaterial), opcion))
        {
            Console.WriteLine("Material no válido.");
            return;
        }

        TipoMaterial tipo = (TipoMaterial)opcion;

        Console.Write("Ingrese el nuevo precio por kilogramo: ");

        if (!decimal.TryParse(
            Console.ReadLine(),
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out decimal nuevoPrecio))
        {
            Console.WriteLine("Debe ingresar un número válido.");
            return;
        }

        bool resultado = configuracionPrecios.ModificarPrecio(
            tipo,
            nuevoPrecio
        );

        if (resultado)
        {
            Console.WriteLine("Precio modificado correctamente.");
        }
        else
        {
            Console.WriteLine("El precio debe ser mayor que cero.");
        }
    }

    private void RegistrarMaterial()
    {
        MostrarTiposMateriales();

        int opcion = LeerEntero("Seleccione el tipo de material: ");

        if (!Enum.IsDefined(typeof(TipoMaterial), opcion))
        {
            Console.WriteLine("Material no válido.");
            return;
        }

        TipoMaterial tipo = (TipoMaterial)opcion;

        Console.Write("Ingrese el peso en kilogramos: ");

        if (!decimal.TryParse(
            Console.ReadLine(),
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out decimal peso))
        {
            Console.WriteLine("Debe ingresar un peso válido.");
            return;
        }

        try
        {
            Material nuevoMaterial;

            if (tipo == TipoMaterial.PET ||
                tipo == TipoMaterial.Vasija ||
                tipo == TipoMaterial.Soplado ||
                tipo == TipoMaterial.Tapas)
            {
                nuevoMaterial = new Plastico(tipo, peso);
            }
            else if (tipo == TipoMaterial.Papel ||
                     tipo == TipoMaterial.Carton)
            {
                nuevoMaterial = new Archivo(tipo, peso);
            }
            else
            {
                nuevoMaterial = new Metal(tipo, peso);
            }

            registroMateriales.Registrar(nuevoMaterial);

            Console.WriteLine("Material registrado correctamente.");
        }
        catch (ArgumentException error)
        {
            Console.WriteLine(error.Message);
        }
    }

    private void MostrarTiposMateriales()
    {
        Console.WriteLine("\n===== TIPOS DE MATERIALES =====");

        foreach (TipoMaterial tipo in Enum.GetValues(typeof(TipoMaterial)))
        {
            Console.WriteLine($"{(int)tipo}. {tipo}");
        }
    }

    private int LeerEntero(string mensaje)
    {
        int numero;

        while (true)
        {
            Console.Write(mensaje);

            if (int.TryParse(Console.ReadLine(), out numero))
            {
                return numero;
            }

            Console.WriteLine("Ingrese un número entero válido.");
        }
    }
}