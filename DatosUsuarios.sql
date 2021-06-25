CREATE DATABASE DatosUsuario
GO

USE DatosUsuario
GO

CREATE TABLE Areas(
IdAreas INT IDENTITY(1,1) NOT NULL,
NombreArea VARCHAR(70) NOT NULL,
Activo BIT
CONSTRAINT PK_Areas PRIMARY KEY (IdAreas),
CONSTRAINT UQ_Areas UNIQUE (NombreArea))
GO

CREATE TABLE Usuarios(
IdUsuario INT IDENTITY(1,1) NOT NULL,
NombreUsuario VARCHAR(70) NOT NULL,
IdAreas INT NOT NULL,
Activo BIT
CONSTRAINT PK_Usuarios PRIMARY KEY (IdUsuario), 
CONSTRAINT FK_Usuarios FOREIGN KEY (IdAreas) REFERENCES Areas (IdAreas))
GO

INSERT INTO Areas VALUES ('Finanzas', 1)
INSERT INTO Areas VALUES ('Juridico', 1)
INSERT INTO Areas VALUES ('TI', 1)
INSERT INTO Areas VALUES ('Recursos Humanos', 1)
INSERT INTO Areas VALUES ('Mantenimiento', 1)
INSERT INTO Areas VALUES ('Consiliacion', 1)
GO
INSERT INTO Usuarios VALUES ('Antonio Jimenez', 1,1)
INSERT INTO Usuarios VALUES ('Camila Gomes', 5,1)
INSERT INTO Usuarios VALUES ('Raul Perez', 2,1)
INSERT INTO Usuarios VALUES ('Angela Villa', 3,1)
INSERT INTO Usuarios VALUES ('Maria Gutierrez', 4,1)
INSERT INTO Usuarios VALUES ('Simon Rodriguez', 6,1)
GO

-- =============================================
-- [14052021][Usuario][[Selecciona todos los registros de la tabla Usuarios]
-- =============================================
CREATE PROCEDURE [dbo].[Usp_SelectUsuarios]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT u.IdUsuario, 
		   u.NombreUsuario, 
		   a.IdAreas,
		   a.NombreArea, 
		   u.Activo 
	FROM Usuarios u
	INNER JOIN Areas a
	ON u.IdAreas = a.IdAreas
END
GO

-- =============================================
-- [14052021][Usuario][Selecciona un registro de la tabla Usuarios mediante su Id]
-- =============================================
CREATE PROCEDURE [dbo].[Usp_SelectUsuarioId]
@IdUsuario INT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT IdUsuario FROM Usuarios WHERE IdUsuario = @IdUsuario AND Activo = 1)
	BEGIN
		SELECT u.IdUsuario, 
			   u.NombreUsuario, 
			   a.IdAreas,
			   a.NombreArea,
			   u.Activo,
			   'Exito' AS Response,
			   200 AS Code
		FROM Usuarios u
		INNER JOIN Areas a
		ON u.IdAreas = a.IdAreas 
		WHERE u.IdUsuario = @IdUsuario
	END 
	ELSE
	BEGIN
		SELECT CONCAT('El registro con id ', @IdUsuario, ' no existe.') AS Response, 404 AS Code
	END
END
GO

-- =============================================
-- [14052021][Usuario][Inserta un nuevo registro en la tabla Usuarios]
-- =============================================
CREATE PROCEDURE Usp_InsertUsuario
@NombreUsuario VARCHAR(70), 
@IdAreas INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
    BEGIN TRANSACTION Usuarios 
		BEGIN 
  			INSERT INTO Usuarios (NombreUsuario, IdAreas, Activo) 
			VALUES (@NombreUsuario, @IdAreas, 1)
			SELECT CONCAT('Registro guardado con id ', SCOPE_IDENTITY()) AS Response, 200 AS Code
		END
    COMMIT TRANSACTION Usuarios
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION Usuarios
		SELECT ERROR_MESSAGE() AS Response, 400 AS Code
	END CATCH
END
GO

-- =============================================
-- [14052021][Usuario][Actualiza un registro de la tabla Usuarios mediante su Id]
-- =============================================
CREATE PROCEDURE [dbo].[Usp_UpdateUsuario]
@IdUsuario INT, 
@NombreUsuario VARCHAR(70), 
@IdAreas INT, 
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY 
		IF EXISTS(SELECT IdUsuario FROM Usuarios WHERE IdUsuario = @IdUsuario AND Activo = 1)
		BEGIN
			UPDATE Usuarios
			SET NombreUsuario = @NombreUsuario, 
				IdAreas = @IdAreas, 
				Activo = @IdAreas
			WHERE IdUsuario = @IdUsuario
			SELECT CONCAT('El registro con id ', @IdUsuario, ' fue actualizado.') AS Response, 200 AS Code
		END 
		ELSE
		BEGIN
			SELECT CONCAT('El registro con id ', @IdUsuario, ' no existe.') AS Response, 404 AS Code
		END		
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION Usuarios
		SELECT ERROR_MESSAGE() AS Response
	END CATCH
END
GO

-- =============================================
-- [23052021][Usuario][[Selecciona todos los registros de la tabla Areas]
-- =============================================
CREATE PROCEDURE [dbo].[Usp_SelectAreas]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT IdAreas, NombreArea
	FROM Areas
	WHERE Activo = 1
END
GO

-- =============================================
-- [14052021][Usuario][Selecciona un registro de la tabla Areas mediante su Id]
-- =============================================
CREATE PROCEDURE [dbo].[Usp_SelectAreaId]
@IdAreas INT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT IdAreas FROM Areas WHERE IdAreas = @IdAreas AND Activo = 1)
	BEGIN
		SELECT IdAreas, 
			   NombreArea,
			   'Exito' AS Response
		FROM Areas
		WHERE IdAreas = @IdAreas AND Activo = 1
	END 
	ELSE
	BEGIN
		SELECT CONCAT('El registro con id ', @IdAreas, ' no existe.') AS Response
	END
END
GO

-- =============================================
-- [23052021][Usuario][Inserta un nuevo registro en la tabla Areas]
-- =============================================
CREATE PROCEDURE [dbo].[Usp_InsertArea]
@NombreArea VARCHAR(70)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
    BEGIN TRANSACTION Areas  
		BEGIN 
  			INSERT INTO Areas (NombreArea, Activo) 
			VALUES (@NombreArea, 1)
			SELECT CONCAT('Registro guardado con id ', SCOPE_IDENTITY()) AS Response, 200 AS Code
		END
    COMMIT TRANSACTION Areas
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION Areas
		SELECT ERROR_MESSAGE() AS Response, 400 AS Code
	END CATCH
END
GO

-- =============================================
-- [23052021][Usuario][Actualiza un registro de la tabla Areas mediante su Id]
-- =============================================
CREATE PROCEDURE [dbo].[Usp_UpdateArea]
@IdAreas INT, 
@NombreArea VARCHAR(70), 
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		IF EXISTS(SELECT IdAreas FROM Areas WHERE IdAreas = @IdAreas AND Activo = 1)
		BEGIN
			UPDATE Areas
			SET NombreArea = @NombreArea, 
				Activo = Activo
			WHERE IdAreas = @IdAreas
			SELECT CONCAT('El registro con id ', @IdAreas, ' fue actualizado.') AS Response
		END 
		ELSE
		BEGIN
			SELECT CONCAT('El registro con id ', @IdAreas, ' no existe.') AS Response
		END		
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION Areas
		SELECT ERROR_MESSAGE() AS Response
	END CATCH
END
