using Core.Entidades;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Data.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        public IConfiguration Configuration { get; }
        private readonly string strCon;

        public ProductoRepositorio(IConfiguration _configuration)
        {
            Configuration = _configuration;
            strCon = Configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            string sp = "ObtenerTodosProductos";

            try
            {
                using (var conn = new SqlConnection(strCon))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand(sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var lstProductos = new List<Producto>();

                            while (await reader.ReadAsync())
                            {
                                Producto producto = new Producto();
                                producto.Id = (int)reader["Id"];
                                producto.Descripcion = reader["Descripcion"].ToString();
                                producto.Stock = (int)reader["Stock"];


                                lstProductos.Add(producto);
                            }

                            return lstProductos;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            string sp = "ObtenerProductoXId";

            try
            {
                using (var conn = new SqlConnection(strCon))
                {
                    conn.Open();
                    Producto producto = null;

                    using (var cmd = new SqlCommand(sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                producto = new Producto();
                                producto.Id = (int)reader["Id"];
                                producto.Descripcion = reader["Descripcion"].ToString();
                                producto.Stock = (int)reader["Stock"];
                            }

                            return producto;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<int?> Insertar(Producto producto)
        {
            string sp = "InsertarProducto";

            try
            {
                using (var conn = new SqlConnection(strCon))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand(sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = producto.Descripcion;
                        cmd.Parameters.Add("@stock", SqlDbType.Int).Value = producto.Stock;
                        var paramId = new SqlParameter("@id", SqlDbType.Int);
                        paramId.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramId);

                        await cmd.ExecuteNonQueryAsync();

                        if (paramId != null)
                        {
                            var newId = (int)paramId.Value;
                            return newId;
                        }
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> Actualizar(Producto producto)
        {
            string sp = "ActualizarProducto";

            try
            {
                using (var conn = new SqlConnection(strCon))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand(sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = producto.Id;
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = producto.Descripcion;
                        cmd.Parameters.Add("@stock", SqlDbType.Int).Value = producto.Stock;

                        var res = await cmd.ExecuteNonQueryAsync();

                        return res > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            string sp = "EliminarProducto";

            try
            {
                var producto = await ObtenerPorId(id);

                using (var conn = new SqlConnection(strCon))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand(sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }  
    }
}
