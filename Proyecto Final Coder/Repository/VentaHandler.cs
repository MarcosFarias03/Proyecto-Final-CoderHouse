using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class VentaHandler
    {
        public const string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Producto> GetVentas()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SELECT Descripciones, Costo, PrecioVenta FROM Venta AS v " +
                    "INNER JOIN ProductoVendido AS pv ON v.Id = pv.IdVenta " +
                    "INNER JOIN Producto AS p ON p.Id = pv.IdProducto", sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Descripcion = dataReader["Descripciones"].ToString();
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);

                                productos.Add(producto);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return productos;
        }
        public static bool CargarVentas(Venta venta)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string queryInsert = "INSERT INTO Venta (Comentarios, IdUsuario) VALUES (@Comentarios, @IdUsuario)";
                    SqlParameter comentariosParamenter = new SqlParameter("Comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };
                    SqlParameter idUsuarioParameter = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int) { Value = venta.IdUsuario };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(comentariosParamenter);
                        sqlCommand.Parameters.Add(idUsuarioParameter);

                        int queryRow = sqlCommand.ExecuteNonQuery();
                        if (queryRow > 0)
                        {
                            resultado = true;
                        }
                    }
                    sqlConnection.Close();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return resultado;
            }
        }
        public static bool EliminarVentas(int id)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string queryDelete = "DELETE FROM ProductoVendido WHERE IdVenta = @id;" +
                        "DELETE FROM Venta WHERE Id = @id";
                    SqlParameter idParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = id };
                    SqlParameter idVentaParameter = new SqlParameter("IdVenta", System.Data.SqlDbType.Int) { Value = id };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.Parameters.Add(idVentaParameter);

                        int queryRow = sqlCommand.ExecuteNonQuery();
                        if (queryRow > 0)
                        {
                            resultado = true;
                        }
                    }
                    sqlConnection.Close();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return resultado;
            }
        }
        
    }
}
