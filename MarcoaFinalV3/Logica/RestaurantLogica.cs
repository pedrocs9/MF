using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Logica
{
    public class RestaurantLogica
    {
        public static RestaurantLogica _instancia = null;

        private RestaurantLogica()
        {

        }

        public static RestaurantLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new RestaurantLogica();
                }
                return _instancia;
            }
        }

        public List<Restaurant> ObtenerRestaurantes()
        {
            List<Restaurant> rptListaUsuario = new List<Restaurant>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerRestaurant", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaUsuario.Add(new Restaurant()
                        {
                            IdRestaurant = Convert.ToInt32(dr["IdRestaurant"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            RUC = dr["RUC"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())

                        });
                    }
                    dr.Close();

                    return rptListaUsuario;

                }
                catch (Exception ex)
                {
                    rptListaUsuario = null;
                    return rptListaUsuario;
                }
            }
        }

        public bool RegistrarRestaurant(Restaurant oRestaurant)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarRestaurant", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oRestaurant.Nombre);
                    cmd.Parameters.AddWithValue("Ruc", oRestaurant.RUC);
                    cmd.Parameters.AddWithValue("Direccion", oRestaurant.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", oRestaurant.Telefono);
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


        public bool ModificarRestaurant(Restaurant oRestaurant)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarRestaurant", oConexion);
                    cmd.Parameters.AddWithValue("IdRestaurant", oRestaurant.IdRestaurant);
                    cmd.Parameters.AddWithValue("Nombre", oRestaurant.Nombre);
                    cmd.Parameters.AddWithValue("Ruc", oRestaurant.RUC);
                    cmd.Parameters.AddWithValue("Direccion", oRestaurant.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", oRestaurant.Telefono);
                    cmd.Parameters.AddWithValue("Activo", oRestaurant.Activo);
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

        public bool EliminarRestaurant(int IdRestaurant)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarRestaurant", oConexion);
                    cmd.Parameters.AddWithValue("IdRestaurant", IdRestaurant);
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
    }
}