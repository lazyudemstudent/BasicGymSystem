# Sistema de Gimnasio - ASP.NET Core MVC .NET 9

Proyecto académico construido con la misma organización general del proyecto base del docente:

- `SistemaGimnasio.Data`: entidades, `ApplicationDbContext`, Fluent API y migraciones.
- `SistemaGimnasio.Models`: modelos de presentación (View Models).
- `SistemaGimnasio.Logic`: interfaces y clases de servicios.
- `SistemaGimnasio.Web`: controladores MVC, vistas Razor, Bootstrap, Identity y configuración.

## Funcionalidades

- CRUD de clientes.
- CRUD de entrenadores.
- CRUD de tipos o planes de membresía.
- Asignación y control de membresías.
- Registro y consulta de pagos.
- Registro de entradas y salidas de clientes.
- Dashboard con datos resumidos.
- Registro e inicio de sesión mediante ASP.NET Core Identity.
- Protección de los módulos administrativos mediante `[Authorize]`.
- Dashboard y menú de módulos visibles únicamente para usuarios autenticados.

## Requisitos

- Visual Studio 2022 con soporte para ASP.NET y desarrollo web.
- .NET 9 SDK.
- SQL Server Developer/Express.

## Crear la base de datos local

1. Abra `SistemaGimnasio.sln` en Visual Studio.
2. Establezca `SistemaGimnasio.Web` como proyecto de inicio.
3. Abra **Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes**.
4. Ejecute:

```powershell
Update-Database -Project SistemaGimnasio.Data -StartupProject SistemaGimnasio.Web
```

La migración inicial ya está incluida en `SistemaGimnasio.Data/Migrations` y creará `SistemaGimnasioDB`. En la primera ejecución solo se debe usar `Update-Database`; no se debe crear otra migración.

## Crear una migración únicamente después de modificar las entidades

```powershell
Add-Migration NombreDeLaMigracion -Project SistemaGimnasio.Data -StartupProject SistemaGimnasio.Web
Update-Database -Project SistemaGimnasio.Data -StartupProject SistemaGimnasio.Web
```

Para retirar la última migración antes de aplicarla:

```powershell
Remove-Migration -Project SistemaGimnasio.Data -StartupProject SistemaGimnasio.Web
```

## Orden recomendado para probar el sistema

1. Registrarse o iniciar sesión.
2. Crear un entrenador.
3. Crear un cliente y asignarle el entrenador.
4. Crear un plan de membresía.
5. Asignar la membresía al cliente.
6. Registrar un pago.
7. Registrar una asistencia.

## Conexión local

La cadena se encuentra en `SistemaGimnasio.Web/appsettings.json`:

```text
Server=MARIODEVIT;Database=SistemaGimnasioDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```
