using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace MarcoaFinalV3.Logica
{
    public class MesaLogica
    {
        private static MesaLogica instancia = null;

        public MesaLogica()
        {

        }

        public static MesaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new MesaLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Mesa oMesa)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMesa", oConexion);
                    cmd.Parameters.AddWithValue("Numero", oMesa.Numero);
                    cmd.Parameters.AddWithValue("Detalle", oMesa.Detalle);
                    cmd.Parameters.AddWithValue("IdEstadoMesa", oMesa.oEstadoMesa.IdEstadoMesa);
                    cmd.Parameters.AddWithValue("IdCapacidad", oMesa.oCapacidadMesa.IdCapacidadMesa);
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

        public bool Modificar(Mesa oMesa)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarMesa", oConexion);
                    cmd.Parameters.AddWithValue("IdMesa", oMesa.IdMesa);
                    cmd.Parameters.AddWithValue("Numero", oMesa.Numero);
                    cmd.Parameters.AddWithValue("Detalle", oMesa.Detalle);
                    cmd.Parameters.AddWithValue("IdEstadoMesa", oMesa.oEstadoMesa.IdEstadoMesa);
                    cmd.Parameters.AddWithValue("IdCategoria", oMesa.oCapacidadMesa.IdCapacidadMesa);
                    cmd.Parameters.AddWithValue("Estado", oMesa.Estado);
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


        //public List<Mesa> Listar()
        //{
        //    List<Mesa> rptListaMesa = new List<Mesa>();
        //    using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        //    {
        //        SqlCommand cmd = new SqlCommand("usp_ObtenerMesa", oConexion);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        try
        //        {
        //            oConexion.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            while (dr.Read())
        //            {
        //                rptListaMesa.Add(new Mesa()
        //                {
        //                    IdMesa = Convert.ToInt32(dr["IdMesa"].ToString()),
        //                    Numero = dr["Numero"].ToString(),
        //                    Detalle = dr["Detalle"].ToString(),
        //                    oEstadoMesa = new EstadoMesa() { IdEstadoMesa = Convert.ToInt32(dr["IdEstadoMesa"]), Descripcion = dr["DescripcionEstadoMesa"].ToString() },
        //                    oCapacidadMesa = new CapacidadMesa() { IdCapacidadMesa = Convert.ToInt32(dr["IdCapacidad"]), Descripcion = dr["DescripcionCapacidadMesa"].ToString() },
        //                    Estado = Convert.ToBoolean(dr["Activo"].ToString()),

        //                });
        //            }
        //            dr.Close();



        //        }
        //        catch (Exception ex)
        //        {
        //            //rptListaMesa = null;
        //            //return rptListaMesa;
        //            rptListaMesa = new List<Mesa>();
        //        }
        //    }
        //    return rptListaMesa;
        //}
        public List<Mesa> Listar()
        {
            List<Mesa> Lista = new List<Mesa>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select m.IdMesa,m.Numero,m.Detalle,em.Descripcion[DescripcionEstadoMesa],cm.Descripcion[DescripcionCapacidadMesa],m.Estado");
                    query.AppendLine("from MESAV2 m");
                    query.AppendLine("inner join ESTADO_MESA em on m.IdEstadoMesa = em.IdEstadoMesa");
                    query.AppendLine("inner join CAPACIDAD_MESA cm on m.IdCapacidad = cm.IdCapacidadMesa");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Mesa()
                            {
                                IdMesa = Convert.ToInt32(dr["IdMesa"].ToString()),
                                Numero = dr["Numero"].ToString(),
                                Detalle = dr["Detalle"].ToString(),
                                oEstadoMesa = new EstadoMesa() { Descripcion = dr["DescripcionEstadoMesa"].ToString() },
                                oCapacidadMesa = new CapacidadMesa() { Descripcion = dr["DescripcionCapacidadMesa"].ToString() },
                                
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Mesa>();
                }
            }
            return Lista;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from MESAV2 where idMesa = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool ActualizarEstado(int idmesa, int idestadomesa)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update MESAV2 set idestadomesa = @idestadomesa where IdMesa = @idMesa ", oConexion);
                    cmd.Parameters.AddWithValue("@idmesa", idmesa);
                    cmd.Parameters.AddWithValue("@idestadomesa", idestadomesa);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
}