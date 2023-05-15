using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using APIRestLibreria.Models;
using System.Drawing;

namespace APIRestLibreria.Controllers
{
    public class Estudiante
    {
        private Conexion conect = new Conexion();
        SqlCommand cmd ;

        public int idEstudiante { get; set; }
        public String nombre { get; set; }
        public String apellidos { get; set; }
        public int estado { get; set; }

        private static List<Estudiante> listaEstudiantes;

        public List<Estudiante> consultaEstudintes() {
            listaEstudiantes = new List<Estudiante>();
            String query = "SELECT * FROM  Estudiantes WHERE estado = @est";
            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
                cmd.Parameters.Add("@est", SqlDbType.Int);
                cmd.Parameters["@est"].Value = 1;
                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Estudiante nvEstudiante = new Estudiante();
                            nvEstudiante.idEstudiante = Convert.ToInt32(dr["PKidEstudiante"]);
                            nvEstudiante.nombre = dr["nombre"].ToString();
                            nvEstudiante.apellidos = dr["apellidos"].ToString();
                            nvEstudiante.estado=Convert.ToInt32(dr["estado"]);
                            listaEstudiantes.Add(nvEstudiante);
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("consulta de estudiante ha errado.", e);

                }
            }
            return listaEstudiantes;
        }
        public Estudiante consultaEstudinte(int id)
        {
            Estudiante nvEstudiante;
            String query = "SELECT * FROM  Estudiantes WHERE PKidEstudiante = @id and estado = @est";
            
            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters.Add("@est", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                cmd.Parameters["@est"].Value = 1;
                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            nvEstudiante = new Estudiante();
                            
                            nvEstudiante.idEstudiante = Convert.ToInt32(dr["PKidEstudiante"]);
                            nvEstudiante.nombre = dr["nombre"].ToString();
                            nvEstudiante.apellidos = dr["apellidos"].ToString();
                            nvEstudiante.estado = Convert.ToInt32(dr["estado"]);
                            return nvEstudiante;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("consulta de estudiante ha errado.",e);
                }
            }
            return null;
        }
        public bool registrarEstudiante(Estudiante estudiante)
        {
            bool result=false;
            String query = "INSERT INTO Estudiantes(PKidEstudiante, nombre, apellidos, estado) VALUES(@id ,@nombre, @apellidos,@est)";

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar);
                cmd.Parameters.Add("@apellidos", SqlDbType.VarChar);
                cmd.Parameters.Add("@est", SqlDbType.Int);
                cmd.Parameters["@id"].Value = estudiante.idEstudiante;
                cmd.Parameters["@nombre"].Value = estudiante.nombre;
                cmd.Parameters["@apellidos"].Value = estudiante.apellidos;
                cmd.Parameters["@est"].Value = 1;
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) {
                        result = true;
                    }
                    else{ result = false; }
                }
                catch (Exception e)
                {
                    Console.WriteLine("insersion de estudiante ha errado.", e);
                    result = false;
                }
            }
            return result;
        }
        
        public bool eliminar (int id)
        {
            bool result;
            String query = "UPDATE Estudiantes SET estado = @est WHERE PKidEstudiante = @id";

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                cmd.Parameters.Add("@est", SqlDbType.Int);
                cmd.Parameters["@est"].Value = 0;
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        result = true;
                    }
                    else { result = false; }
                }
                catch (Exception e)
                {
                    Console.WriteLine("eliminacion de estudiante se ha errado.", e);
                    result = false;
                }
            }
            return result;
        }
    }
}