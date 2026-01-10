/* =========================================================
   Base de Datos: seven-suite
   Script inicial – Tablas, Índices, SPs y Seeders
   Compatible: SQL Server 2019
   ========================================================= */

USE [seven-suite];
GO

/* =========================================================
   LIMPIEZA (ejecutable múltiples veces)
   ========================================================= */
IF OBJECT_ID('dbo.sp_SEVECLIE_Search', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_SEVECLIE_Search;
GO

IF OBJECT_ID('dbo.sp_SEVECLIE_Upsert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_SEVECLIE_Upsert;
GO

IF OBJECT_ID('dbo.SEVECLIE', 'U') IS NOT NULL
    DROP TABLE dbo.SEVECLIE;
GO

IF OBJECT_ID('dbo.EstadoCivil', 'U') IS NOT NULL
    DROP TABLE dbo.EstadoCivil;
GO

/* =========================================================
   TABLA: EstadoCivil (Catálogo)
   ========================================================= */
CREATE TABLE dbo.EstadoCivil (
                                 Id INT IDENTITY(1,1) PRIMARY KEY,
                                 Nombre NVARCHAR(50) NOT NULL
);
GO

/* =========================================================
   SEEDER: EstadoCivil
   ========================================================= */
INSERT INTO dbo.EstadoCivil (Nombre)
VALUES
    ('Soltero(a)'),
    ('Casado(a)'),
    ('Unión Libre'),
    ('Divorciado(a)'),
    ('Viudo(a)');
GO

/* =========================================================
   TABLA: SEVECLIE (Clientes)
   ========================================================= */
CREATE TABLE dbo.SEVECLIE (
                              Id INT IDENTITY(1,1) PRIMARY KEY,
                              Cedula NVARCHAR(25) NOT NULL,
                              Nombre NVARCHAR(120) NOT NULL,
                              Genero CHAR(1) NOT NULL,
                              FechaNac DATE NOT NULL,
                              EstadoCivilId INT NOT NULL,
                              CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

                              CONSTRAINT UQ_SEVECLIE_Cedula UNIQUE (Cedula),
                              CONSTRAINT CK_SEVECLIE_Genero CHECK (Genero IN ('M', 'F')),
                              CONSTRAINT FK_SEVECLIE_EstadoCivil
                                  FOREIGN KEY (EstadoCivilId)
                                      REFERENCES dbo.EstadoCivil(Id)
);
GO

/* =========================================================
   ÍNDICES (Performance)
   ========================================================= */
CREATE UNIQUE INDEX UX_SEVECLIE_Cedula
    ON dbo.SEVECLIE (Cedula);
GO

CREATE INDEX IX_SEVECLIE_Nombre
    ON dbo.SEVECLIE (Nombre);
GO

CREATE INDEX IX_SEVECLIE_EstadoCivil
    ON dbo.SEVECLIE (EstadoCivilId);
GO

/* =========================================================
   STORED PROCEDURE: Upsert Cliente
   Inserta o actualiza registros
   ========================================================= */
CREATE PROCEDURE dbo.sp_SEVECLIE_Upsert
    @Id INT = NULL,
    @Cedula NVARCHAR(25),
    @Nombre NVARCHAR(120),
    @Genero CHAR(1),
    @FechaNac DATE,
    @EstadoCivilId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validación de cédula duplicada
    IF EXISTS (
        SELECT 1
        FROM dbo.SEVECLIE
        WHERE Cedula = @Cedula
          AND (@Id IS NULL OR Id <> @Id)
    )
        BEGIN
            RAISERROR ('La cédula ya existe.', 16, 1);
            RETURN;
        END

    IF (@Id IS NULL OR @Id = 0)
        BEGIN
            INSERT INTO dbo.SEVECLIE (
                Cedula, Nombre, Genero, FechaNac, EstadoCivilId
            )
            VALUES (
                       @Cedula, @Nombre, @Genero, @FechaNac, @EstadoCivilId
                   );

            SELECT SCOPE_IDENTITY() AS NewId;
            RETURN;
        END

    UPDATE dbo.SEVECLIE
    SET Cedula = @Cedula,
        Nombre = @Nombre,
        Genero = @Genero,
        FechaNac = @FechaNac,
        EstadoCivilId = @EstadoCivilId
    WHERE Id = @Id;

    SELECT @Id AS UpdatedId;
END
GO

/* =========================================================
   STORED PROCEDURE: Búsqueda con filtros
   ========================================================= */
CREATE PROCEDURE dbo.sp_SEVECLIE_Search
    @Cedula NVARCHAR(25) = NULL,
    @Nombre NVARCHAR(120) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id,
        c.Cedula,
        c.Nombre,
        c.Genero,
        c.FechaNac,
        c.EstadoCivilId,
        ec.Nombre AS EstadoCivil,
        c.CreatedAt
    FROM dbo.SEVECLIE c
             INNER JOIN dbo.EstadoCivil ec
                        ON ec.Id = c.EstadoCivilId
    WHERE (@Cedula IS NULL OR c.Cedula LIKE '%' + @Cedula + '%')
      AND (@Nombre IS NULL OR c.Nombre LIKE '%' + @Nombre + '%')
    ORDER BY c.Id DESC;
END
GO

/* =========================================================
   SEEDER: Datos de prueba SEVECLIE
   ========================================================= */
INSERT INTO dbo.SEVECLIE (Cedula, Nombre, Genero, FechaNac, EstadoCivilId)
VALUES
    ('001-010190-0001A', 'Juan Pérez López', 'M', '1990-01-01', 1),
    ('002-020285-0002B', 'María González Ruiz', 'F', '1985-02-02', 2),
    ('003-150392-0003C', 'Carlos Mendoza Torres', 'M', '1992-03-15', 3),
    ('004-221178-0004D', 'Ana Rodríguez Silva', 'F', '1978-11-22', 4),
    ('005-300670-0005E', 'Luis Hernández Castro', 'M', '1970-06-30', 5);
GO

/* =========================================================
   FIN DEL SCRIPT
   ========================================================= */
