using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Logica
{
    public class CapacidadMesaLogica
    {
        private static CapacidadMesaLogica instancia = null;

        public CapacidadMesaLogica()
        {

        }

        public static CapacidadMesaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new CapacidadMesaLogica();
                }

                return instancia;
            }
        }
        public bool RegistrarCapacidadMesa(CapacidadMesa oCapacidadMesa)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCapacidadMesa", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", oCapacidadMesa.Descripcion);
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

        public bool ModificarCapacidadMesa(CapacidadMesa oCapacidadMesa)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarCapacidadMesa", oConexion);
                    cmd.Parameters.AddWithValue("IdCategoria", oCapacidadMesa.IdCapacidadMesa);
                    cmd.Parameters.AddWithValue("Descripcion", oCapacidadMesa.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", oCapacidadMesa.Estado);
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


        public List<CapacidadMesa> Listar()
        {
            List<CapacidadMesa> Lista = new List<CapacidadMesa>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdCapacidadMesa,Descripcion,Estado from CAPACIDAD_MESA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new CapacidadMesa()
                            {
                                IdCapacidadMesa = Convert.ToInt32(dr["IdCapacidadMesa"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<CapacidadMesa>();
                }
            }
            return Lista;
        }

        public bool EliminarCapacidadMesa(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from CAPACIDAD_MESA where idcapacidadmesa= @id", oConexion);
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
    }
}