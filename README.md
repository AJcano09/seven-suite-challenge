# Seven Suite Solution
Breve portal de administración y consulta de clientes construido sobre ASP.NET Web Forms con una capa de datos basada en procedimientos almacenados.

## Requisitos previos
- Visual Studio 2015+ (o MSBuild 14) con soporte para .NET Framework 4.5.2.
- SQL Server 2016/2019 (puede ser local o alojado) con credenciales que permitan crear bases y objetos.
- IIS Express o IIS para hospedar el proyecto WebForms.
  
## Arquitectura
- **SevenSuite.Entities**: define los DTOs compartidos (cliente, estado civil) que se serializan/transportan entre capas.
- **SeventSuite.DAL**: encapsula la comunicación con SQL Server. Contiene un proveedor de conexión, helpers para ejecutar procedimientos y mapea resultados a entidades.
- **SevenSuite.BLL**: implementa la lógica de negocio sobre los objetos del DAL (validaciones, control de errores y conversión a DTOs consumidos por la capa Web).
- **SevenSuite.Web**: UI WebForms + servicios ASMX que exponen interacción (login, CRUD de clientes, reportes). Consume la BLL y muestra datos mediante páginas `.aspx`, scripts AngularJS/vanilla del bundle `scripts/app/` y los reportes RDLC.

## Preparar la base de datos
1. Cree la base `seven-suite` en su servidor SQL si no existe.
2. Ejecute el script `common/Script-db.sql` (desde SQL Server Management Studio, Azure Data Studio o `sqlcmd`) para crear tablas, relaciones, procedimientos y datos semilla.

   ```bash
   sqlcmd -S <servidor> -U <usuario> -P <contraseña> -i common/Script-db.sql
   ```

3. Verifique que los procedimientos como `sp_SEVECLIE_Search`, `sp_SEVECLIE_Upsert`, `sp_EstadoCivil_GetAll` y el catálogo `EstadoCivil` estén creados.

## Configuración del proyecto
1. Abra `SevenSuite.Web/Web.config`.
2. Localice la cadena de conexión `DefaultConnection` y actualice los valores de `Data Source`, `Initial Catalog`, `User ID` y `Password` para apuntar a su servidor, por ejemplo:

   ```xml
   <connectionStrings>
     <add name="DefaultConnection"
          connectionString="Data Source=.;Initial Catalog=seven-suite;User ID=sa;Password=YourStrong@Passw0rd;"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

3. Si usa autenticación integrada, elimine `User ID`/`Password` y añada `Integrated Security=True`.

## Compilar y ejecutar
1. Restaure paquetes NuGet:

   ```bash
   nuget restore SevenSuiteSolution.sln
   ```

2. Compile la solución desde línea de comandos o Visual Studio:

   ```bash
   msbuild SevenSuiteSolution.sln /p:Configuration=Release
   ```

3. Abra la solución en Visual Studio, establezca `SevenSuite.Web` como proyecto de inicio y ejecute (`F5` o `Ctrl+F5`). Usa IIS Express configurado en el `.csproj` (puerto aleatorio).
4. Alternativamente, despliegue la carpeta `SevenSuite.Web` y apúntela desde IIS clásico si se necesita un entorno productivo. Asegúrese de copiar los ensamblados `SqlServerTypes` y habilitar MVC/WebForms si el servidor no los tiene.

## Notas adicionales
- Si necesita consultarlos, los servicios `Services/AuthService.asmx` y `Services/ClienteService.asmx` usan la BLL para exponer datos JSON/WS.
- Los assets principales (`scripts/app/`, `content/site.css`, `Reports/ClientesReporte.rdlc`) están en el propio proyecto Web y se empaquetan con la compilación.
- Cualquier cambio en la estructura de la base debe reflejarse en los procedimientos utilizados por `SeventSuite.DAL`.
