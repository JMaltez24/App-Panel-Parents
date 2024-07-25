	Create database DB_PanelParents
	go

	use DB_PanelParents
	go

	create table Tbl_Padre
	(
	ID int identity primary key,
	Nombre varchar(200),
	Usuario varchar(200),
	Contraseniaa varchar(max)
	)
	go

	create table Tbl_Alumno
	(
	ID int identity primary key,
	Nombre varchar(200),
	Codigo varchar(200),
	FechaNacimiento date,
	Genero varchar(50)
	)
	go

	Create table Tbl_Padre_Alumno
	(
	ID int identity primary key,
	Fk_Id_Padre int foreign key references Tbl_Padre(ID),
	Fk_Id_Alumno int foreign key references Tbl_Alumno(ID)
	)
	go

	create table Tbl_Maestro
	(
	ID int identity primary key,
	Nombre varchar(200),
	Usuario varchar(200),
	Contraseniaa varchar(max),
	Rol varchar(20)
	)
	go

	Create table Tbl_GrupoClase
	(
	ID int identity primary key,
	Anio int,
	Grado varchar(200),
	Fk_Id_Maestro int foreign key references Tbl_Maestro(ID)
	)
	go

	create table Tbl_Alumno_GrupoClase
	(
	ID int identity primary key,
	Fk_Id_Alumno int foreign key references Tbl_Alumno(ID),
	Fk_Id_GrupoClase int foreign key references Tbl_GrupoClase(ID)
	)
	go

	create table Tbl_Asignatura
	(
	ID int identity primary key,
	Nombre varchar(200),
	Descripcion varchar(max),
	)
	go

	create table Tbl_Alumno_GrupoClase__Asignatura
	(
	ID int identity primary key,
	Fk_Id_Alumno_GrupoClase int foreign key references Tbl_Alumno_GrupoClase(ID),
	Fk_Id_Asignatura int foreign key references Tbl_Asignatura(ID),
	Nota int
	)
	go

	create table Tbl_GrupoClase_Asignatura
	(
	ID int identity primary key,
	Fk_Id_GrupoClase int foreign key references Tbl_GrupoClase(ID),
	Fk_Id_Asignatura int foreign key references Tbl_Asignatura(ID)
	)
	go
	

CREATE TABLE Tbl_Anuncio
(
    ID INT IDENTITY PRIMARY KEY,
    Asunto NVARCHAR(200),
    Fecha DATETIME,
    Contenido NVARCHAR(MAX),
    Fk_Id_Maestro INT FOREIGN KEY REFERENCES Tbl_Maestro(ID)
)
GO


----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------

create proc Sp_Agregar_Maestro
(
@Nombre varchar(255),
@Usuario varchar(255),
@Contrasenia varchar(max),
@rol varchar(20)
)
as
begin
insert into Tbl_Maestro values(@Nombre,@Usuario,@Contrasenia,@rol)
end

go

---------------------------------------
create PROCEDURE Sp_Autenticar_Usuario
    @Usuario NVARCHAR(50),
    @Contrasenia NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Tbl_Maestro WHERE Usuario = @Usuario AND Contraseniaa = @Contrasenia)
    BEGIN
        SELECT 1 AS Resultado
    END
    ELSE
    BEGIN
        SELECT 0 AS Resultado
    END
END
GO
---------------------------------------
CREATE PROCEDURE Sp_Agregar_Alumno
    @Nombre NVARCHAR(200),
    @Codigo NVARCHAR(200),
    @FechaNacimiento DATE,
    @Genero NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tbl_Alumno (Nombre, Codigo, FechaNacimiento, Genero)
    VALUES (@Nombre, @Codigo, @FechaNacimiento, @Genero);
END
GO
----------------------------------------
CREATE PROCEDURE Sp_Agregar_Asignatura
    @Nombre NVARCHAR(200),
    @Descripcion NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tbl_Asignatura (Nombre, Descripcion)
    VALUES (@Nombre, @Descripcion);
END
GO
-----------------------------------------
CREATE PROCEDURE Sp_Agregar_Padre
    @Nombre NVARCHAR(200),
    @Usuario NVARCHAR(200),
    @Contraseniaa NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tbl_Padre (Nombre, Usuario, Contraseniaa)
    VALUES (@Nombre, @Usuario, @Contraseniaa);
END
GO
---------------------------------------------
CREATE PROCEDURE Sp_Agregar_GrupoClase
    @Anio int,
    @Grado NVARCHAR(200),
    @Fk_Id_Maestro int
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tbl_GrupoClase (Anio, Grado, Fk_Id_Maestro)
    VALUES (@Anio, @Grado, @Fk_Id_Maestro);
END
GO
------------------------------------------------
CREATE PROCEDURE Sp_Obtener_Todos_Maestros
AS
BEGIN
    SELECT ID, Nombre, Usuario, Contraseniaa
    FROM Tbl_Maestro
END
go
---------------------------------------------------
CREATE PROCEDURE Sp_Agregar_GrupoClase_Asignatura
    @Fk_Id_GrupoClase int,
    @Fk_Id_Asignatura int
AS
BEGIN
    INSERT INTO Tbl_GrupoClase_Asignatura (Fk_Id_GrupoClase, Fk_Id_Asignatura)
    VALUES (@Fk_Id_GrupoClase, @Fk_Id_Asignatura)
END
go
---------------------------------------------------
CREATE PROCEDURE Sp_Autenticar_Padre
    @Usuario NVARCHAR(200),
    @Contrasenia NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Id INT;
    
    SELECT @Id = ID
    FROM Tbl_Padre
    WHERE Usuario = @Usuario AND Contraseniaa = @Contrasenia;
    
    IF @Id IS NOT NULL
    BEGIN
        SELECT @Id AS Id;
    END
    ELSE
    BEGIN
        SELECT 0 AS Id;
    END
END
go



--------------------------------------------------------
create PROCEDURE Sp_Agregar_PadreHijo
    @Id_Padre INT,
    @Codigo VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Fk_Id_Alumno INT;

    -- Buscar el ID del alumno usando el código
    SELECT @Fk_Id_Alumno = ID
    FROM Tbl_Alumno
    WHERE Codigo = @Codigo;

    -- Verificar si se encontró el alumno
    IF @Fk_Id_Alumno IS NULL
    BEGIN
        SELECT 'Alumno no encontrado.' AS Mensaje;
        RETURN;
    END

    BEGIN TRY
        -- Verificar si ya existe la relación padre-alumno
        IF EXISTS (
            SELECT 1
            FROM Tbl_Padre_Alumno
            WHERE Fk_Id_Padre = @Id_Padre
              AND Fk_Id_Alumno = @Fk_Id_Alumno
        )
        BEGIN
            SELECT 'La relación ya existe.' AS Mensaje;
        END
        ELSE
        BEGIN
            -- Insertar la nueva relación padre-alumno
            INSERT INTO Tbl_Padre_Alumno (Fk_Id_Padre, Fk_Id_Alumno)
            VALUES (@Id_Padre, @Fk_Id_Alumno);

            SELECT 'Relación padre-alumno agregada exitosamente.' AS Mensaje;
        END
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Mensaje;
    END CATCH
END
GO

--------------------------------------------------------------------------
-----hecho 12/07/2024 22:10

alter PROCEDURE Sp_Agregar_Alumno_GrupoClase
    @Fk_Id_Alumno INT,
    @Fk_Id_GrupoClase INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si ya existe la relación alumno-grupo de clase
        IF EXISTS (
            SELECT 1
            FROM Tbl_Alumno_GrupoClase
            WHERE Fk_Id_Alumno = @Fk_Id_Alumno
              AND Fk_Id_GrupoClase = @Fk_Id_GrupoClase
        )
        BEGIN
            -- Devolver mensaje si la relación ya existe
            SELECT 'La relación alumno-grupo de clase ya existe.' AS Mensaje;
        END
        ELSE
        BEGIN
            -- Insertar la nueva relación alumno-grupo de clase
            INSERT INTO Tbl_Alumno_GrupoClase (Fk_Id_Alumno, Fk_Id_GrupoClase)
            VALUES (@Fk_Id_Alumno, @Fk_Id_GrupoClase);

            -- Devolver mensaje de éxito
            SELECT 'Relación alumno-grupo de clase agregada exitosamente.' AS Mensaje;
        END
    END TRY
    BEGIN CATCH
        -- Devolver mensaje de error en caso de excepción
        SELECT ERROR_MESSAGE() AS Mensaje;
    END CATCH
END
GO


-------------------------------------------------------------------------------
--------hecho 16/07/2024

CREATE PROCEDURE Sp_Listar_Alumnos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID, Nombre, Codigo, FechaNacimiento, Genero
    FROM Tbl_Alumno;
END
go

---------------------------------------------------------------------------------
CREATE PROCEDURE Sp_Listar_Asignaturas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID, Nombre, Descripcion
    FROM Tbl_Asignatura;
END
go

-----------------------------------------------------------------------------------
CREATE PROCEDURE Sp_Agregar_Anuncio
    @Asunto NVARCHAR(200),
    @Fecha DATETIME,
    @Contenido NVARCHAR(MAX),
    @Fk_Id_Maestro INT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Tbl_Anuncio (Asunto, Fecha, Contenido, Fk_Id_Maestro)
    VALUES (@Asunto, @Fecha, @Contenido, @Fk_Id_Maestro);
    
    SELECT SCOPE_IDENTITY() AS Id;
END
--------------------------------------------------------------------------------------
CREATE PROCEDURE Sp_Agregar_Nota
    @Fk_Id_Alumno_GrupoClase INT,
    @Fk_Id_Asignatura INT,
    @Nota INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar el registro con la nota
    INSERT INTO Tbl_Alumno_GrupoClase__Asignatura (Fk_Id_Alumno_GrupoClase, Fk_Id_Asignatura, Nota)
    VALUES (@Fk_Id_Alumno_GrupoClase, @Fk_Id_Asignatura, @Nota);
END
GO
--------------------------------------------------------------------------------------+
----CREADO EL 20-07-2024 17:28
CREATE PROCEDURE Sp_Mostrar_Alumnos_Padre
    @Id_Padre INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.ID AS AlumnoID,
        a.Nombre AS AlumnoNombre,
        gc.ID AS GrupoClaseID,
        gc.Anio AS GrupoClaseAnio,
        gc.Grado AS GrupoClaseGrado,
        m.Nombre AS MaestroNombre
    FROM Tbl_Padre_Alumno pa
    INNER JOIN Tbl_Alumno a ON pa.Fk_Id_Alumno = a.ID
    INNER JOIN Tbl_Alumno_GrupoClase agc ON a.ID = agc.Fk_Id_Alumno
    INNER JOIN Tbl_GrupoClase gc ON agc.Fk_Id_GrupoClase = gc.ID
    INNER JOIN Tbl_Maestro m ON gc.Fk_Id_Maestro = m.ID
    WHERE pa.Fk_Id_Padre = @Id_Padre
    AND agc.ID = (
        SELECT TOP 1 ID
        FROM Tbl_Alumno_GrupoClase
        WHERE Fk_Id_Alumno = a.ID
        ORDER BY ID DESC
    )
END
GO


