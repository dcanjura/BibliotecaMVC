using BibliotecaMVC.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BibliotecaMVC.Data
{
    public class CategoriaRepository
    {
        private readonly string _cadenaConexion;

        public CategoriaRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("BibliotecaConexion")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión BibliotecaConexion.");
        }

        public List<Categoria> ObtenerCategorias()
        {
            var categorias = new List<Categoria>();
            const string consulta = "SELECT Id, Nombre, Descripcion FROM Categorias ORDER BY Nombre";

            using var conexion = new SqlConnection(_cadenaConexion);
            using var comando = new SqlCommand(consulta, conexion);
            conexion.Open();

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                categorias.Add(new Categoria
                {
                    Id = Convert.ToInt32(lector["Id"]),
                    Nombre = lector["Nombre"].ToString() ?? string.Empty,
                    Descripcion = lector["Descripcion"].ToString() ?? string.Empty
                });
            }

            return categorias;
        }

        public Categoria? ObtenerPorId(int id)
        {
            const string consulta = "SELECT Id, Nombre, Descripcion FROM Categorias WHERE Id = @Id";

            using var conexion = new SqlConnection(_cadenaConexion);
            using var comando = new SqlCommand(consulta, conexion);
            comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            conexion.Open();

            using var lector = comando.ExecuteReader();
            if (!lector.Read()) return null;

            return new Categoria
            {
                Id = Convert.ToInt32(lector["Id"]),
                Nombre = lector["Nombre"].ToString() ?? string.Empty,
                Descripcion = lector["Descripcion"].ToString() ?? string.Empty
            };
        }

        public void Agregar(Categoria categoria)
        {
            const string consulta = "INSERT INTO Categorias (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";

            using var conexion = new SqlConnection(_cadenaConexion);
            using var comando = new SqlCommand(consulta, conexion);
            comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = categoria.Nombre;
            comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 250).Value = categoria.Descripcion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }

        public bool Actualizar(Categoria categoria)
        {
            const string consulta = @"UPDATE Categorias
                                      SET Nombre = @Nombre, Descripcion = @Descripcion
                                      WHERE Id = @Id";

            using var conexion = new SqlConnection(_cadenaConexion);
            using var comando = new SqlCommand(consulta, conexion);
            comando.Parameters.Add("@Id", SqlDbType.Int).Value = categoria.Id;
            comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = categoria.Nombre;
            comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 250).Value = categoria.Descripcion;
            conexion.Open();
            return comando.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int id)
        {
            const string consulta = "DELETE FROM Categorias WHERE Id = @Id";

            using var conexion = new SqlConnection(_cadenaConexion);
            using var comando = new SqlCommand(consulta, conexion);
            comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            conexion.Open();
            return comando.ExecuteNonQuery() > 0;
        }
    }
}
