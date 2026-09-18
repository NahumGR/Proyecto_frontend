USE BibliotecaDB;

INSERT INTO BibliotecaDB.dbo.Categorias (Nombre, Descripcion, ImagenURL) VALUES
    (N'Literatura', N'', N'literario.jpg'),
    (N'Fantasia', N'Historias de Fantasia', N'fantasia.jpg'),
    (N'Comedia Aventura', N'', N'comedia.jpg'),
    (N'Narrativo', N'', N'narrativo.jpg'),
    (N'Novela', N'Obras narrativas de ficción', NULL),
    (N'Ciencia Ficción', N'Obras relacionadas con ciencia y tecnología', NULL),
    (N'Historia', N'Obras relacionadas com acontecimientos históricos', NULL),
    (N'Programacion', N'', N'programacion.jpg');


INSERT INTO BibliotecaDB.dbo.Autores (Nombre, Apellido, Nacionalidad, FechaNacimiento, Activo) VALUES
    (N'Miguel', N'de Cervantes Saavedra', N'Española', '1547-09-29 00:00:00.0000000', 0),
    (N'Gabriel José', N'García Marquez', N'Colombia', '1927-03-06 00:00:00.0000000', 0),
    (N'Joanne', N'Rowling', N'Británica', '1965-07-31 00:00:00.0000000', 1),
    (N'Isabel', N'Allende', N'Chilena', '1942-08-02 00:00:00.0000000', 1),
    (N'Salvador Efraín', N'Salazar Arrué', N'Salvadoreña', '1899-10-22 00:00:00.0000000', 0),
    (N'Robert', N'Cecil Martin', N'Estadounidense', '1952-12-05 00:00:00.0000000', 1);

INSERT INTO BibliotecaDB.dbo.Libros (Titulo, Autor, Categoria, Precio, Disponible, ImagenUrl) VALUES
    (N'Clean Code', N'Robert Martin', N'Programación', 35.40, 1, N'clean_code.png'),
    (N'Cien años de Soledad', N'Gabriel García Marquez', N'Literatura', 19.00, 0, N'Cien_años_de_soledad.png'),
    (N'Harry Potter', N'Joanne Rowling', N'Fantasia', 25.00, 0, N'Harry_Potter.jpg'),
    (N'Don Quijote', N'Miguel de Cervantes', N'Comedia Aventura', 25.00, 1, N'don_quijote.jpg'),
    (N'La casa de los espíritus', N'Isabel Allende', N'Comedia Aventura', 27.00, 0, N'casa_espiritus.jpg'),
    (N'Cuentos de barro', N'Salvador Arrué', N'narrativo', 27.00, 1, N'cuentos_barro.jpg');
