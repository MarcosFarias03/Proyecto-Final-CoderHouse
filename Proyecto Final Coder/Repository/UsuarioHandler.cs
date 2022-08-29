using ProyectoFinal.Models;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class UsuarioHandler
    {
        public const string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True";

        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                if (usuario.Id == 0 || usuario.Nombre == "" || usuario.Apellido == "" || 
                    usuario.NombreUsuario == "" || usuario.Contraseña.Length < 8 || 
                    usuario.Contraseña == "" || usuario.Mail == "")
                {
                    throw new Exception("Datos invalidos.");
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        string queryUpdate = "UPDATE Usuario SET Nombre = @nameParameter, Apellido = @apellidoParameter,NombreUsuario = @nombreUsuarioParameter, Contraseña = @contraseñaParameter, Mail = @mailParameter WHERE Id = @idParameter";
                        SqlParameter idParameter = new SqlParameter("idParameter", System.Data.SqlDbType.BigInt) { Value = usuario.Id };
                        SqlParameter nameParameter = new SqlParameter("nameParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                        SqlParameter apellidoParameter = new SqlParameter("apellidoParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                        SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuarioParameter", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                        SqlParameter contraseñaParameter = new SqlParameter("contraseñaParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                        SqlParameter mailParameter = new SqlParameter("mailParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };

                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(idParameter);
                            sqlCommand.Parameters.Add(nameParameter);
                            sqlCommand.Parameters.Add(apellidoParameter);
                            sqlCommand.Parameters.Add(nombreUsuarioParameter);
                            sqlCommand.Parameters.Add(contraseñaParameter);
                            sqlCommand.Parameters.Add(mailParameter);

                            int queryRows = sqlCommand.ExecuteNonQuery();

                            if (queryRows > 0)
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
        public static bool InsertarUsuario(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                if (usuario.Contraseña.Length < 8)
                {
                    throw new Exception("La contraseña debe tener mas de  8 caracteres.");
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        string queryInsert = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                            "VALUES (@nameParameter, @apellidoParameter, @nombreUsuarioParameter, @contraseñaParameter, @mailParameter)";
                        SqlParameter nameParameter = new SqlParameter("nameParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                        SqlParameter apellidoParameter = new SqlParameter("apellidoParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                        SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuarioParameter", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                        SqlParameter contraseñaParameter = new SqlParameter("contraseñaParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                        SqlParameter mailParameter = new SqlParameter("mailParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };
                        sqlConnection.Open();

                        using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(nameParameter);
                            sqlCommand.Parameters.Add(apellidoParameter);
                            sqlCommand.Parameters.Add(nombreUsuarioParameter);
                            sqlCommand.Parameters.Add(contraseñaParameter);
                            sqlCommand.Parameters.Add(mailParameter);

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
        public static Usuario GetUsuario(string NombreUsuario)
        {
            Usuario resultado = new Usuario();
            try
            {
                if (NombreUsuario == "" || NombreUsuario == null)
                {
                    throw new Exception("Datos invalidos");
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        string querySelect = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario";
                        SqlParameter sqlParameter = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = NombreUsuario };

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
                                        Usuario usuario = new Usuario();
                                        usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                        usuario.Nombre = dataReader["Nombre"].ToString();
                                        usuario.Apellido = dataReader["Apellido"].ToString();
                                        usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                        usuario.Contraseña = dataReader["Contraseña"].ToString();
                                        usuario.Mail = dataReader["Mail"].ToString();

                                        resultado = usuario;
                                    }
                                }
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
        public static bool DeleteUsuario(int id)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string queryDelete = "DELETE FROM Usuario WHERE Id = @id";
                    SqlParameter sqlParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = id };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
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
        public static Usuario GetUsuarioByContraseña(string nombreUsuario, string contraseña)
        {
            Usuario resultado = new Usuario();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string querySelect = "SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña";
                    SqlParameter nombreUsuarioParameter = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = nombreUsuario};
                    SqlParameter contraseñaParameter = new SqlParameter("Contraseña", System.Data.SqlDbType.VarChar) { Value = contraseña };
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(nombreUsuarioParameter);
                        sqlCommand.Parameters.Add(contraseñaParameter);
                        sqlCommand.ExecuteScalar();
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    Usuario usuario = new Usuario();
                                    usuario.Id= Convert.ToInt32(dataReader["Id"]);
                                    usuario.Nombre=dataReader["Nombre"].ToString();
                                    usuario.Apellido = dataReader["Apellido"].ToString();
                                    usuario.NombreUsuario=dataReader["NombreUsuario"].ToString();
                                    usuario.Contraseña = dataReader["Contraseña"].ToString();
                                    usuario.Mail= dataReader["Mail"].ToString();

                                    resultado = usuario;
                                }
                            }
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
