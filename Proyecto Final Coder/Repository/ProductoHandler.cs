using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class ProductoHandler
    {
        public const string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True";

        public static bool EliminarProducto(int idProducto)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string queryDelete = "DELETE FROM ProductoVendido WHERE IdProducto = @id; DELETE FROM Producto WHERE Id = @idProducto;";
                    SqlParameter idParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = idProducto };
                    SqlParameter idProductoParameter = new SqlParameter("IdProducto", System.Data.SqlDbType.Int) { Value = idProducto };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.Parameters.Add(idProductoParameter);
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
        public static bool InsertarProducto(Producto producto)
        {
            bool resultado = false;
            try
            {
                if (producto.Descripcion == null || producto.IdUsuario == 0 || producto.Descripcion == "")
                {
                    throw new Exception("Datos Invalidos");
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        string queryInsert = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@descripcionParameter, @costoParameter, @precioVentaParameter, @stockParameter, @idUsuarioParameter)";

                        SqlParameter descripcionParameter = new SqlParameter("descripcionParameter", System.Data.SqlDbType.VarChar) { Value = producto.Descripcion };
                        SqlParameter costoParameter = new SqlParameter("costoParameter", System.Data.SqlDbType.Int) { Value = producto.Costo };
                        SqlParameter precioVentaParameter = new SqlParameter("precioVentaParameter", System.Data.SqlDbType.Int) { Value = producto.PrecioVenta };
                        SqlParameter stockParameter = new SqlParameter("stockParameter", System.Data.SqlDbType.Int) { Value = producto.Stock };
                        SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", System.Data.SqlDbType.Int) { Value = producto.IdUsuario };

                        sqlConnection.Open();

                        using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(descripcionParameter);
                            sqlCommand.Parameters.Add(costoParameter);
                            sqlCommand.Parameters.Add(precioVentaParameter);
                            sqlCommand.Parameters.Add(stockParameter);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return resultado;
            }
        }
        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;

            try
            {
                if (producto.Descripcion == null || producto.IdUsuario == 0 || producto.Id == 0 || producto.Descripcion == "")
                {
                    return resultado;
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        string queryUpdate = "UPDATE Producto SET Descripciones = @descripcionParameter, Costo = @costoParameter, PrecioVenta = @precioVentaParameter, Stock = @stockParameter, IdUsuario = @idUsuarioParameter WHERE Id = @idParameter";

                        SqlParameter idParameter = new SqlParameter("idParameter", System.Data.SqlDbType.BigInt) { Value = producto.Id };
                        SqlParameter descripcionParameter = new SqlParameter("descripcionParameter", System.Data.SqlDbType.VarChar) { Value = producto.Descripcion };
                        SqlParameter costoParameter = new SqlParameter("costoParameter", System.Data.SqlDbType.Int) { Value = producto.Costo };
                        SqlParameter precioVentaParameter = new SqlParameter("precioVentaParameter", System.Data.SqlDbType.Int) { Value = producto.PrecioVenta };
                        SqlParameter stockParameter = new SqlParameter("stockParameter", System.Data.SqlDbType.Int) { Value = producto.Stock };
                        SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", System.Data.SqlDbType.Int) { Value = producto.IdUsuario };

                        sqlConnection.Open();

                        using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(idParameter);
                            sqlCommand.Parameters.Add(descripcionParameter);
                            sqlCommand.Parameters.Add(costoParameter);
                            sqlCommand.Parameters.Add(precioVentaParameter);
                            sqlCommand.Parameters.Add(stockParameter);
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
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return resultado;
            }
        }
        public static List<Producto> GetProductos(int idUsuario)
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string querySelect = "SELECT * FROM Producto WHERE IdUsuario = @idUsuario";
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
                                    producto.Id = Convert.ToInt32(dataReader["Id"]);
                                    producto.Descripcion = dataReader["Descripciones"].ToString();
                                    producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                    producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                    producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                    producto.Stock = Convert.ToInt32(dataReader["IdUsuario"]);

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
    }
}
