
INSERT INTO BibliotecaDB.dbo.Autores (Nombre,Apellido,Nacionalidad,FechaNacimiento,Activo,ImagenUrl) VALUES
     (N'Miguel',N'de Cervantes Saavedra',N'Española','1547-09-29 00:00:00.0000000',0,N'cervantes.jpg'),
     (N'Gabriel José',N'García Marquez',N'Colombia','1927-03-06 00:00:00.0000000',0,N'marquez.jpg'),
     (N'Joanne',N'Rowling',N'Británica','1965-07-31 00:00:00.0000000',1,N'jk.jpg'),
     (N'Isabel',N'Allende',N'Chilena','1942-08-02 00:00:00.0000000',1,N'allende.JPG'),
     (N'Salvador Efraín',N'Salazar Arrué',N'Salvadoreña','1899-10-22 00:00:00.0000000',0,N'salarrue.jpg'),
     (N'Robert',N'Cecil Martin',N'Estadounidense','1952-12-05 00:00:00.0000000',1,N'robert.jpg');

     

INSERT INTO BibliotecaDB.dbo.Categoria (Nombre,Descripcion,ImagenUrl) VALUES
     (N'Literatura',N'Obras maestras que han resistido el paso del tiempo, explorando la condición humana a través de historias universales',N'literario.jpg'),
     (N'Fantasia',N'Historias de Fantasias Mundos mágicos, profecías antiguas y héroes enfrentando amenazas extraordinarias',N'fantasia.jpg'),
     (N'Comedia Aventura',N'Viajes disparatados, peligros absurdos y héroes que sobreviven más por suerte que por destreza',N'comedia.jpg'),
     (N'Narrativo',N'Relatos que atrapan desde la primera línea para sumergirte en mundos, vidas y conflictos inolvidables',N'narrativo.jpg'),
     (N'Novela',N'Obras narrativas de ficción',N'novela.jpg'),
     (N'Ciencia Ficción',N'Obras relacionadas con ciencia y tecnología',N'ciencia_ficcion.jpg'),
     (N'Historia',N'Obras relacionadas com acontecimientos históricos',N'historia.jpg'),
     (N'Programacion',N'Fundamentos, algoritmos y código para dominar el arte de crear software, automatizar tareas y resolver problemas complejos',N'programacion.jpg');

INSERT INTO BibliotecaDB.dbo.Libros (Titulo,Autor,Categoria,Precio,Disponible,ImagenUrl) VALUES
     (N'Clean Code',N'Robert Cecil Martin',N'Programacion',35.40,1,N'clean_code.png'),
     (N'Cien años de Soledad',N'Gabriel José García Marquez',N'Literatura',19.00,0,N'Cien_años_de_soledad.png'),
     (N'Harry Potter',N'Joanne Rowling',N'Fantasia',25.00,0,N'Harry_Potter.jpg'),
     (N'Don Quijote',N'Miguel de Cervantes Saavedra',N'Comedia Aventura',25.00,1,N'don_quijote.jpg'),
     (N'La casa de los espíritus',N'Isabel Allende',N'Comedia Aventura',27.00,0,N'casa_espiritus.jpg'),
     (N'Cuentos de barro',N'Salvador Efraín Salazar Arrué',N'Narrativo',27.00,1,N'cuentos_barro.jpg');