using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIRestLibreria.Models
{
    public class Conexion
    {
        private SqlConnection connecion;
        private string cadenaConexion;
        public void conectar() {
            try
            {
                cadenaConexion = "Data Source=DESKTOP-CJTATOQ\\SQLEXPRESS;Initial Catalog=Libreria;Integrated Security=True";
                connecion= new SqlConnection(cadenaConexion);
                connecion.Open();
                Console.WriteLine("conexion hecha");
            }
            catch (Exception e){
                Console.WriteLine("conexion errada, ",e);
            }
        }
        public void cerrarConeccion() {
           connecion.Close();
        }
        public SqlConnection connection() {
            return connecion;
        }
    }
}