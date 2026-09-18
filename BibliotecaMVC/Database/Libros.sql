USE BibliotecaMVC;
GO

CREATE TABLE Libros
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(150) NOT NULL,
    Autor NVARCHAR(100) NOT NULL,
    Categoria NVARCHAR(80) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Disponible BIT NOT NULL,
    Imagen NVARCHAR(150) NULL
);
GO

INSERT INTO Libros (Titulo, Autor, Categoria, Precio, Disponible, Imagen)
VALUES
    ('Clean Code', 'Robert Martin', 'Programación', 35.50, 1, 'clean-code.png'),
    ('Cien años de soledad', 'Gabriel García Márquez', 'Literatura', 18.00, 0, 'cien-anos-soledad.png');
GO
