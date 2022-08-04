using Core.Entidades;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Data.Repositorios
{
    public class VentaRepositorio : IVentaRepositorio
    {
        public IConfiguration Configuration { get; }
        private readonly string strCon;

        public VentaRepositorio(IConfiguration _configuration)
        {
            Configuration = _configuration;
            strCon = Configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> Insertar(Venta venta)
        {
            string sp1 = "InsertarVenta";
            string sp2 = "InsertarVentaDetalle";

            bool ventaCompletada = false;
            bool detalleVentaCompletado = false;
            int newIdVenta = 0;

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (var conn = new SqlConnection(strCon))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand(sp1, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = venta.Codigo;
                            cmd.Parameters.Add("@cliente", SqlDbType.VarChar).Value = venta.Cliente;
                            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@total", SqlDbType.Decimal).Value = venta.Total;
                            var paramId = new SqlParameter("@id", SqlDbType.Int);
                            paramId.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(paramId);

                            await cmd.ExecuteNonQueryAsync();

                            if (paramId != null)
                            {
                                newIdVenta = (int)paramId.Value;
                                ventaCompletada = true;
                            }
                        }

                        //throw new Exception("Error en la transacion");

                        foreach (var detalle in venta.Detalle)
                        {
                            using (var cmd = new SqlCommand(sp2, conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@productoId", SqlDbType.Int).Value = detalle.ProductoId;
                                cmd.Parameters.Add("@ventaId", SqlDbType.Int).Value = newIdVenta;
                                cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                                cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = detalle.Precio;
                                cmd.Parameters.Add("@total", SqlDbType.Decimal).Value = detalle.Total;
                                var paramId = new SqlParameter("@id", SqlDbType.Int);
                                paramId.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(paramId);

                                detalleVentaCompletado = await cmd.ExecuteNonQueryAsync() > 0;
                            }
                        }

                        if (ventaCompletada && detalleVentaCompletado) ts.Complete();
                    }

                    return ventaCompletada && detalleVentaCompletado;
                    //ts.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
