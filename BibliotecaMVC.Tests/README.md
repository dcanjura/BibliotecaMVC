# Pruebas unitarias de Libros

Este proyecto usa **xUnit** y **Moq** para probar `LibrosController` sin utilizar SQL Server.

## Escenarios evaluados

- Consulta de libros cuando existen registros.
- Consulta de libros cuando la colección está vacía.
- Consulta de detalle existente e inexistente.
- Registro de un libro válido e inválido.
- Edición de un libro existente, inexistente y con identificadores diferentes.
- Eliminación de un libro existente e inexistente.
- Verificación de llamadas al servicio mediante `Mock.Verify()`.

## Ejecución

Desde la raíz de la solución:

```powershell
dotnet test
```

En Visual Studio, abra **Test Explorer** y seleccione **Run All**.

## Evidencia

Para la entrega, incluya una captura de Test Explorer o de la consola mostrando las pruebas aprobadas.
