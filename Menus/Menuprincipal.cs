using ProyectoReciclaje.Modelos;
using ProyectoReciclaje.Servicios;
using System.Globalization;

namespace ProyectoReciclaje.Menus;

public class MenuPrincipal
{
    private readonly ConfiguracionPrecios configuracionPrecios = new();
    private readonly RegistroMateriales registroMateriales = new();
    private readonly RegistroLonas registroLonas = new();
    private readonly Inventario inventario;
    private readonly GeneradorReportes generadorReportes;

    public MenuPrincipal()
    {
        inventario = new Inventario(registroLonas);
        generadorReportes = new GeneradorReportes(inventario);
    }

    public void Mostrar()
    {
        int opcion;
        do
        {
            Console.WriteLine("\n================================");
            Console.WriteLine("       SISTEMA DE RECICLAJE");
            Console.WriteLine("================================");
            Console.WriteLine("1. Registrar material (registro general)");
            Console.WriteLine("2. Configurar precios");
            Console.WriteLine("3. Mostrar materiales registrados");
            Console.WriteLine("4. Registrar lona");
            Console.WriteLine("5. Mostrar lonas");
            Console.WriteLine("6. Registrar compra");
            Console.WriteLine("7. Registrar venta");
            Console.WriteLine("8. Consultar existencias por material");
            Console.WriteLine("9. Consultar existencia por lona");
            Console.WriteLine("10. Reportes de compras, ventas y ganancias");
            Console.WriteLine("11. Salir");

            opcion = LeerEntero("Seleccione una opción: ");

            switch (opcion)
            {
                case 1: RegistrarMaterial(); break;
                case 2: MenuPrecios(); break;
                case 3: registroMateriales.MostrarMateriales(); break;
                case 4: RegistrarLona(); break;
                case 5: registroLonas.MostrarLonas(); break;
                case 6: RegistrarCompra(); break;
                case 7: RegistrarVenta(); break;
                case 8: inventario.MostrarExistenciasPorMaterial(); break;
                case 9: ConsultarLona(); break;
                case 10: MenuReportes(); break;
                case 11: Console.WriteLine("Saliendo del sistema..."); break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        } while (opcion != 11);
    }

    private void MenuPrecios()
    {
        int opcion;
        do
        {
            Console.WriteLine("\n===== CONFIGURACIÓN DE PRECIOS =====");
            Console.WriteLine("1. Mostrar precios");
            Console.WriteLine("2. Modificar precio");
            Console.WriteLine("3. Volver");
            opcion = LeerEntero("Seleccione una opción: ");

            if (opcion == 1) configuracionPrecios.MostrarPrecios();
            else if (opcion == 2) ModificarPrecio();
            else if (opcion != 3) Console.WriteLine("Opción no válida.");
        } while (opcion != 3);
    }

    private void MenuReportes()
    {
        int opcion;
        do
        {
            Console.WriteLine("\n===== REPORTES =====");
            Console.WriteLine("1. Reporte de compras");
            Console.WriteLine("2. Reporte de ventas");
            Console.WriteLine("3. Reporte de ganancias");
            Console.WriteLine("4. Mostrar todos los reportes");
            Console.WriteLine("5. Volver al menú principal");

            opcion = LeerEntero("Seleccione una opción: ");

            switch (opcion)
            {
                case 1: generadorReportes.GenerarReporteCompras(); break;
                case 2: generadorReportes.GenerarReporteVentas(); break;
                case 3: generadorReportes.GenerarReporteGanancias(); break;
                case 4: generadorReportes.MostrarTodosLosReportes(); break;
                case 5: break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        } while (opcion != 5);
    }

    private void RegistrarMaterial()
    {
        TipoMaterial? tipo = LeerTipoMaterial();
        if (!tipo.HasValue) return;

        decimal peso = LeerDecimalPositivo("Ingrese el peso en kg: ");
        try
        {
            registroMateriales.Registrar(CrearMaterial(tipo.Value, peso));
            Console.WriteLine("Material registrado.");
        }
        catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
    }

    private Material CrearMaterial(TipoMaterial tipo, decimal peso)
    {
        if (tipo is TipoMaterial.PET or TipoMaterial.Vasija or
            TipoMaterial.Soplado or TipoMaterial.Tapas)
            return new Plastico(tipo, peso);

        if (tipo is TipoMaterial.Papel or TipoMaterial.Carton)
            return new Archivo(tipo, peso);

        return new Metal(tipo, peso);
    }

    private void RegistrarLona()
    {
        int id = LeerEnteroPositivo("Identificador de la lona: ");
        decimal capacidad = LeerDecimalPositivo("Capacidad máxima en kg: ");

        try
        {
            if (registroLonas.Registrar(new Lona(id, capacidad)))
                Console.WriteLine("Lona registrada correctamente.");
            else
                Console.WriteLine("Ya existe una lona con ese identificador.");
        }
        catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
    }

    private void RegistrarCompra()
    {
        registroLonas.MostrarLonas();
        int id = LeerEnteroPositivo("Identificador de la lona: ");
        TipoMaterial? tipo = LeerTipoMaterial();
        if (!tipo.HasValue) return;

        decimal cantidad = LeerDecimalPositivo("Cantidad comprada en kg: ");
        decimal precio = LeerDecimalPositivo("Costo de compra por kg: ");

        try
        {
            inventario.RegistrarCompra(id, tipo.Value, cantidad, precio);
            Console.WriteLine("Compra registrada. Existencias actualizadas.");
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void RegistrarVenta()
    {
        registroLonas.MostrarLonas();
        int id = LeerEnteroPositivo("Identificador de la lona: ");
        decimal cantidad = LeerDecimalPositivo("Cantidad vendida en kg: ");
        decimal precio = LeerDecimalPositivo("Precio de venta por kg: ");

        try
        {
            inventario.RegistrarVenta(id, cantidad, precio);
            Console.WriteLine("Venta registrada. Existencias actualizadas.");
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ConsultarLona()
    {
        int id = LeerEnteroPositivo("Identificador de la lona: ");
        try
        {
            Lona lona = inventario.ConsultarExistenciaPorLona(id);
            Console.WriteLine(
                $"Lona: {lona.Id}\n" +
                $"Material: {(lona.TipoMaterial?.ToString() ?? "Sin asignar")}\n" +
                $"Peso almacenado: {lona.PesoActualKg:N2} kg\n" +
                $"Capacidad máxima: {lona.CapacidadMaximaKg:N2} kg\n" +
                $"Capacidad disponible: {lona.CapacidadDisponibleKg:N2} kg");
        }
        catch (InvalidOperationException ex) { Console.WriteLine(ex.Message); }
    }

    private void ModificarPrecio()
    {
        TipoMaterial? tipo = LeerTipoMaterial();
        if (!tipo.HasValue) return;

        decimal precio = LeerDecimalPositivo("Nuevo precio por kg: ");
        Console.WriteLine(
            configuracionPrecios.ModificarPrecio(tipo.Value, precio)
            ? "Precio actualizado."
            : "No se pudo modificar el precio.");
    }

    private TipoMaterial? LeerTipoMaterial()
    {
        Console.WriteLine("\n===== TIPOS DE MATERIALES =====");
        foreach (TipoMaterial tipo in Enum.GetValues<TipoMaterial>())
            Console.WriteLine($"{(int)tipo}. {tipo}");

        int opcion = LeerEntero("Seleccione el material (0 para cancelar): ");
        if (opcion == 0) return null;

        if (!Enum.IsDefined(typeof(TipoMaterial), opcion))
        {
            Console.WriteLine("Material no válido.");
            return null;
        }

        return (TipoMaterial)opcion;
    }

    private int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(Console.ReadLine(), out int numero))
                return numero;

            Console.WriteLine("Ingrese un número entero válido.");
        }
    }

    private int LeerEnteroPositivo(string mensaje)
    {
        while (true)
        {
            int numero = LeerEntero(mensaje);
            if (numero > 0) return numero;
            Console.WriteLine("El valor debe ser mayor que cero.");
        }
    }

    private decimal LeerDecimalPositivo(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";

            if (decimal.TryParse(
                entrada,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out decimal numero) && numero > 0)
                return numero;

            Console.WriteLine(
                "Ingrese una cantidad numérica mayor que cero. " +
                "Use el separador decimal de su equipo.");
        }
    }
}
