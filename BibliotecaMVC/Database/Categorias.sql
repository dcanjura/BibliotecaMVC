CREATE DATABASE BibliotecaMVC;
GO

USE BibliotecaMVC;
GO

CREATE TABLE Categorias
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(250) NOT NULL
);
GO

INSERT INTO Categorias (Nombre, Descripcion)
VALUES
    ('Literatura', 'Libros de novelas, cuentos y poesía.'),
    ('Programación', 'Libros sobre desarrollo de software.'),
    ('Historia', 'Libros relacionados con hechos históricos.');
GO
