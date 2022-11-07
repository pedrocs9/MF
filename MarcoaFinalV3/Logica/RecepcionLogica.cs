using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace MarcoaFinalV3.Logica
{
    public class RecepcionLogica
    {
        private static RecepcionLogica instancia = null;

        public RecepcionLogica()
        {

        }

        public static RecepcionLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new RecepcionLogica();
                }

                return instancia;
            }
        }

        public List<Recepcion> Listar()
        {
            List<Recepcion> Lista = new List<Recepcion>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select r.IdRecepcion,h.IdMesa,h.Numero,h.Detalle");
                    query.AppendLine("convert(char(10), r.FechaEntrada, 103)[FechaEntrada], r.PrecioInicial,r.Adelanto,r.PrecioRestante,r.TotalPagado,r.Observacion, r.Estado");
                    query.AppendLine("from RECEPCIONV2 r");
                    query.AppendLine("inner join MESAV2 h on h.IdMesa = r.IdMesa");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Recepcion()
                            {
                                IdRecepcion = Convert.ToInt32(dr["IdRecepcion"]),
                                oUsuario = new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),

                                },
                                oMesa = new Mesa()
                                {
                                    IdMesa = Convert.ToInt32(dr["IdMesa"]),
                                    Numero = dr["Numero"].ToString(),
                                    Detalle = dr["Detalle"].ToString(),
                                    oEstadoMesa = new EstadoMesa() { Descripcion = dr["EstadoMesa"].ToString() },
                                    oCapacidadMesa = new CapacidadMesa() { Descripcion = dr["CapacidadMesa"].ToString() }
                                },
                                FechaEntradaTexto = dr["FechaEntrada"].ToString(),
                                PrecioInicial = Convert.ToDecimal(dr["PrecioInicial"].ToString(), new CultureInfo("es-PE")),
                                Adelanto = Convert.ToDecimal(dr["Adelanto"].ToString(), new CultureInfo("es-PE")),
                                PrecioRestante = Convert.ToDecimal(dr["PrecioRestante"].ToString(), new CultureInfo("es-PE")),
                                Observacion = dr["Observacion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Recepcion>();
                }
            }
            return Lista;
        }

        public bool Registrar(Recepcion objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarRecepcion", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", objeto.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdMesa", objeto.oMesa.IdMesa);
                    cmd.Parameters.AddWithValue("PrecioInicial", Convert.ToDecimal(objeto.PrecioIncialTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("Adelanto", Convert.ToDecimal(objeto.AdelantoTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("PrecioRestante", Convert.ToDecimal(objeto.PrecioRestanteTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("Observacion", objeto.Observacion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool Cerrar(Recepcion objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {

                oConexion.Open();
                SqlTransaction objTransacion = oConexion.BeginTransaction();

                try
                {

                    StringBuilder query = new StringBuilder();

                    query.AppendLine("update RECEPCIONV2 set TotalPagado = @totapagado , Estado = 0");
                    query.AppendLine("where IdRecepcion = @idrecepecion");
                    query.AppendLine("");
                    query.AppendLine("update MESAV2 set IdEstadoMesa = 3");
                    query.AppendLine("where IdMesa = @idmesa");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@totapagado", Convert.ToDecimal(objeto.TotalPagadoTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("@idrecepecion", objeto.IdRecepcion);
                    cmd.Parameters.AddWithValue("@idmesa", objeto.oMesa.IdMesa);
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = objTransacion;

                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    objTransacion.Commit();

                }
                catch (Exception ex)
                {
                    objTransacion.Rollback();
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
}