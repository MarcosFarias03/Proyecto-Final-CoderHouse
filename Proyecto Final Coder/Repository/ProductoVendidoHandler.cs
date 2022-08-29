using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True";
        public static List<Producto> GetProductosVendidos(int idUsuario)
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string querySelect = "SELECT DISTINCT Descripciones, PrecioVenta FROM Producto AS p INNER JOIN ProductoVendido AS pv ON p.Id = pv.Id WHERE IdUsuario = @idUsuario";
                    SqlParameter sqlParameter = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int) { Value = idUsuario };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                        sqlCommand.ExecuteNonQuery();
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    Producto producto = new Producto();
                                    producto.Descripcion = dataReader["Descripciones"].ToString();
                                    producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                    producto.Stock = Convert.ToInt32(dataReader["Stock"]);

                                    productos.Add(producto);
                                }
                            }
                        }
                    }

                    sqlConnection.Close();
                }
                return productos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return productos;
            }
        }
        public static bool CargarProductosVendidos(ProductoVendido producto)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string queryInsertAndUpdate = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@Stock, @IdProducto, @IdVenta);" +
                        "UPDATE Producto SET Stock = Stock - @Stock WHERE Id = @IdProducto";
                    SqlParameter stockParameter = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = producto.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("IdProducto", System.Data.SqlDbType.Int) { Value = producto.IdProducto };
                    SqlParameter idVentaParameter = new SqlParameter("IdVenta", System.Data.SqlDbType.Int) { Value = producto.IdVenta };
                    SqlParameter idParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = producto.IdProducto };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryInsertAndUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(idProductoParameter);
                        sqlCommand.Parameters.Add(idVentaParameter);
                        sqlCommand.Parameters.Add(idParameter);

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
