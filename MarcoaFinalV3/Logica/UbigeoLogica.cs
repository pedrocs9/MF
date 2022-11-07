using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Logica
{
    public class UbigeoLogica
    {
        private static UbigeoLogica _instancia = null;

        public UbigeoLogica()
        {

        }

        public static UbigeoLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new UbigeoLogica();
                }
                return _instancia;
            }
        }
        public List<Region> ObtenerRegion()
        {
            List<Region> lst = new List<Region>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from REGION", oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new Region()
                            {
                                IdRegion = Convert.ToInt32(dr["IdRegion"].ToString()),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lst = new List<Region>();
                }
            }
            return lst;
        }
        public List<Provincia> ObtenerProvincia(string _idregion)
        {
            List<Provincia> lst = new List<Provincia>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from provincia where IdRegion = @idregion", oConexion);
                    cmd.Parameters.AddWithValue("@idregion", _idregion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new Provincia()
                            {
                                IdProvincia = Convert.ToInt32(dr["IdProvincia"].ToString()),
                                IdRegion = Convert.ToInt32(dr["IdRegion"].ToString()),
                                Descripcion = dr["Descripcion"].ToString()
                                
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lst = new List<Provincia>();
                }
            }
            return lst;
        }
        public List<Comuna> ObtenerComuna(string _idprovincia, string _idregion)
        {
            List<Comuna> lst = new List<Comuna>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from COMUNA where IdProvincia = @idprovincia and IdRegion = @idregion", oConexion);
                    cmd.Parameters.AddWithValue("@idprovincia", _idprovincia);
                    cmd.Parameters.AddWithValue("@idregion", _idregion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new Comuna()
                            {
                                IdComuna = Convert.ToInt32(dr["IdComuna"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdProvincia = Convert.ToInt32(dr["IdProvincia"].ToString()),
                                IdRegion = Convert.ToInt32(dr["IdDepartamento"].ToString())
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lst = new List<Comuna>();
                }
            }
            return lst;
        }


    }
}