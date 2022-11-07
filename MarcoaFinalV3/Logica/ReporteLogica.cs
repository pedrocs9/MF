using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Logica
{
    public class ReporteLogica
    {
        public static ReporteLogica _instancia = null;

        private ReporteLogica()
        {

        }

        public static ReporteLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ReporteLogica();
                }
                return _instancia;
            }
        }

        public List<ReporteProducto> ReporteProductoTienda(int IdRestaurant, string CodigoProducto)
        {
            List<ReporteProducto> lista = new List<ReporteProducto>();

            NumberFormatInfo formato = new CultureInfo("es-PE").NumberFormat;
            formato.CurrencyGroupSeparator = ".";

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_rptProductoTienda", oConexion);
                cmd.Parameters.AddWithValue("@IdRestaurant", IdRestaurant);
                cmd.Parameters.AddWithValue("@Codigo", CodigoProducto);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteProducto()
                            {
                                RucRestaurant = dr["Ruc Restaurant"].ToString(),
                                NombreRestaurant = dr["Nombre Restaurant"].ToString(),
                                DireccionRestaurant = dr["Direccion Restaurant"].ToString(),
                                CodigoProducto = dr["Codigo Producto"].ToString(),
                                NombreProducto = dr["Nombre Producto"].ToString(),
                                DescripcionProducto = dr["Descripcion Producto"].ToString(),
                                StockenRestaurant = dr["Stock en Restaurant"].ToString(),
                                PrecioCompra = Convert.ToDecimal(dr["Precio Compra"].ToString(), new CultureInfo("es-PE")).ToString("N", formato),
                                PrecioVenta = Convert.ToDecimal(dr["Precio Venta"].ToString(), new CultureInfo("es-PE")).ToString("N", formato)
                            });
                        }

                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ReporteProducto>();
                }
            }

            return lista;
        }

        public List<ReporteVenta> ReporteVenta(DateTime FechaInicio, DateTime FechaFin, int IdRestaurant)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();

            NumberFormatInfo formato = new CultureInfo("es-PE").NumberFormat;
            formato.CurrencyGroupSeparator = ".";

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_rptVenta", oConexion);
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.Parameters.AddWithValue("@IdRestaurant", IdRestaurant);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                FechaVenta = dr["Fecha Venta"].ToString(),
                                NumeroDocumento = dr["Numero Documento"].ToString(),
                                TipoDocumento = dr["Tipo Documento"].ToString(),
                                NombreRestaurant = dr["Nombre Restaurant"].ToString(),
                                RucRestaurant = dr["Ruc Restaurant"].ToString(),
                                NombreEmpleado = dr["Nombre Empleado"].ToString(),
                                CantidadUnidadesVendidas = dr["Cantidad Unidades Vendidas"].ToString(),
                                CantidadProductos = dr["Cantidad Productos"].ToString(),
                                TotalVenta = Convert.ToDecimal(dr["Total Venta"].ToString(), new CultureInfo("es-PE")).ToString("N", formato)
                            });
                        }

                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ReporteVenta>();
                }
            }

            return lista;

        }
    }
}