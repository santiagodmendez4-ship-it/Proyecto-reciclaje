# ProyectoReciclaje consolidado

Esta carpeta integra el código original de ProyectoReciclaje con los archivos y cambios de Epic 69.

## Importante: conservar el repositorio Git existente

Este paquete NO incluye la carpeta `.git`. Para mantener el historial y la conexión con GitHub:

1. Haz una copia de seguridad de tu carpeta local `ProyectoReciclaje`.
2. Extrae el contenido de esta carpeta consolidada.
3. Copia las carpetas y archivos extraídos dentro de tu carpeta local original `D:\Users\ACER\Documents\ProyectoReciclaje`.
4. Cuando Windows pregunte si deseas reemplazar archivos con el mismo nombre, elige reemplazar. No borres ni reemplaces la carpeta `.git` de tu proyecto original.
5. Abre la carpeta original en VS Code Insiders.
6. Ejecuta `git status` para revisar los cambios.
7. Ejecuta `dotnet build` para compilar y corregir cualquier error que aparezca.
8. Si compila correctamente, ejecuta:

```powershell
git add .
git commit -m "Integra funcionalidades del Epic 69"
git push
```

## Contenido

- Se conservan los archivos originales que no fueron modificados por Epic 69.
- Se incorporan los nuevos modelos `Compra`, `Venta` y `Lona`.
- Se incorporan los servicios `RegistroLonas`, `Inventario` y `GeneradorReportes`.
- Se actualizan el menú principal, modelos y servicios existentes según la versión Epic 69.
- Se conserva el `.gitignore` original, que excluye `bin/`, `obj/`, `.vs/`, archivos temporales y archivos del sistema.
- Se corrigen secuencias `\\n` accidentales en cadenas C# para que produzcan saltos de línea reales.

## Limitación de verificación

El entorno de preparación no tiene instalado el SDK de .NET 10, por lo que este paquete no pudo compilarse aquí. Es necesario ejecutar `dotnet build` en tu equipo antes de subirlo a GitHub.
