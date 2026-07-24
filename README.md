# BibliotecaMVC

Sistema web básico para la gestión de una biblioteca, desarrollado con **ASP.NET Core MVC** y **.NET 8** como parte de la asignatura **Programación Web II**.

La aplicación utiliza el patrón de arquitectura **Modelo-Vista-Controlador (MVC)** para separar la lógica, la presentación de los datos y el control de las solicitudes.

## Funcionalidades

Actualmente, el proyecto permite:

* Mostrar la página principal del sistema.
* Consultar un catálogo de libros.
* Consultar un catálogo de autores.
* Visualizar el estado de disponibilidad de los libros.
* Visualizar el estado activo o inactivo de los autores.
* Navegar hacia los módulos de categorías, usuarios, préstamos y acerca de.

Los módulos de categorías, usuarios, préstamos y acerca de contienen vistas iniciales preparadas para futuras funcionalidades.

## Tecnologías utilizadas

* C#
* .NET 8
* ASP.NET Core MVC
* Razor Views
* HTML5
* CSS3
* Bootstrap
* JavaScript

## Estructura del proyecto

```text
BibliotecaMVC/
├── Controllers/
│   ├── AutoresController.cs
│   ├── HomeController.cs
│   └── LibrosController.cs
├── Models/
│   ├── Autor.cs
│   ├── ErrorViewModel.cs
│   └── Libro.cs
├── Views/
│   ├── Autores/
│   ├── Home/
│   ├── Libros/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
├── Program.cs
└── BibliotecaMVC.csproj
```

## Modelos principales

### Autor

El modelo `Autor` contiene las siguientes propiedades:

* `ID`: identificador del autor.
* `Nombre`: nombre del autor.
* `Apellido`: apellido del autor.
* `Nacionalidad`: nacionalidad del autor.
* `Fecha_de_Nacimiento`: fecha de nacimiento almacenada con `DateOnly`.
* `Activo`: indica si el autor se encuentra activo o inactivo.

### Libro

El modelo `Libro` contiene las siguientes propiedades:

* `ID`: identificador del libro.
* `Titulo`: título del libro.
* `Autor`: nombre del autor.
* `Categoria`: categoría a la que pertenece el libro.
* `Precio`: precio del libro.
* `Disponible`: indica si el libro está disponible o prestado.

## Manejo actual de los datos

Los datos de libros y autores se crean directamente dentro de sus respectivos controladores mediante listas en memoria.

Por esta razón, los datos son únicamente de demostración y se vuelven a crear cada vez que se ejecuta la aplicación. Actualmente, el proyecto no utiliza una base de datos.

## Requisitos

Para ejecutar el proyecto se necesita:

* .NET 8 SDK.
* Visual Studio 2022, Visual Studio Code o JetBrains Rider.
* Un navegador web moderno.

## Ejecución del proyecto

### Desde Visual Studio

1. Abrir el archivo `BibliotecaMVC.slnx`.
2. Seleccionar el proyecto `BibliotecaMVC` como proyecto de inicio.
3. Ejecutar la aplicación con `F5` o con el botón de inicio.

### Desde la terminal

Ubicarse dentro de la carpeta que contiene el archivo `BibliotecaMVC.csproj` y ejecutar:

```bash
dotnet restore
dotnet run
```

La aplicación estará disponible en una de las siguientes direcciones, según el perfil utilizado:

```text
https://localhost:7170
http://localhost:5240
```

## Rutas principales

| Módulo     | Ruta               |
| ---------- | ------------------ |
| Inicio     | `/`                |
| Libros     | `/Libros`          |
| Autores    | `/Autores`         |
| Categorías | `/Home/Categorias` |
| Usuarios   | `/Home/Usuarios`   |
| Préstamos  | `/Home/Prestamos`  |
| Acerca de  | `/Home/AcercaDe`   |

## Posibles mejoras

Como continuación del proyecto se pueden implementar las siguientes funcionalidades:

* Conexión con una base de datos mediante Entity Framework Core.
* Operaciones CRUD para libros, autores, categorías, usuarios y préstamos.
* Validaciones en formularios.
* Relaciones entre libros, autores y categorías.
* Registro y autenticación de usuarios.
* Búsqueda y filtrado de libros.
* Control de préstamos y devoluciones.
* Manejo de inventario y disponibilidad.

## Autor

**Diego Arguera**

Proyecto desarrollado para la asignatura **Programación Web II**.
