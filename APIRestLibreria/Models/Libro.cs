using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using APIRestLibreria.Models;

namespace APIRestLibreria.Controllers
{
    public class Libro
    {
        private Conexion conect = new Conexion();
        SqlCommand cmd;
        public int idLibros { get; set; }
        public int existencias { get; set; }
        public String nombre { get; set; }
        public int disponibilidad { get; set; }

        public byte motivo { get; set; }
        private static List<Libro> listaLibros;


        public List<Libro> consultaLibros()
        {
            listaLibros = new List<Libro>();
            String query = "SELECT * FROM  Libros";
            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Libro nvLibro = new Libro();
                            nvLibro.idLibros = Convert.ToInt32(dr["PKidLibros"]);
                            nvLibro.nombre = dr["nombre"].ToString();
                            nvLibro.existencias = Convert.ToInt32(dr["existencias"].ToString());
                            nvLibro.disponibilidad = Convert.ToInt32(dr["disponibilidad"]);
                            listaLibros.Add(nvLibro);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("consulta de libro ha errado.", e);

                }
            }
            return listaLibros;
        }
        public Libro consultaLibro(int id)
        {
            Libro nvLibro;
            String query = "SELECT * FROM Libros WHERE PKidLibros = @id ";

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            nvLibro = new Libro();

                            nvLibro.idLibros = Convert.ToInt32(dr["PKidLibros"]);
                            nvLibro.nombre = dr["nombre"].ToString();
                            nvLibro.existencias = Convert.ToInt32(dr["existencias"].ToString());
                            nvLibro.disponibilidad = Convert.ToInt32(dr["disponibilidad"]);
                            return nvLibro;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("consulta de libro ha errado.", e);
                }
            }
            return null;
        }
        public bool registrarLibro(Libro libro)
        {
            bool result = false;
            String query = "INSERT INTO Libros (existencias, nombre, disponibilidad) VALUES(@existencias, @nombre ,@disponibilidad)";

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
                cmd.Parameters.Add("@existencias", SqlDbType.Int);
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar);
                cmd.Parameters.Add("@disponibilidad", SqlDbType.Int);
                cmd.Parameters["@existencias"].Value = libro.existencias;
                cmd.Parameters["@nombre"].Value = libro.nombre;
                cmd.Parameters["@disponibilidad"].Value = libro.disponibilidad;
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
                    Console.WriteLine("insersion de libro ha errado.", e);
                    result = false;
                }
            }
            return result;
        }

        public bool modificarExistencia(int id, int cant, int motivo)
        {
            if (cant > 0 && motivo == 0)
            {
                cant--;
            }
            else if (cant > 0 && motivo == 1) {
                cant++;
            }

            bool result;
            String query = "UPDATE libros SET existencias = @exist WHERE PKidLibros = @id";

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                cmd.Parameters.Add("@exist", SqlDbType.Int);
                cmd.Parameters["@exist"].Value = cant;
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
                    Console.WriteLine("eliminacion de libro se ha errado.", e);
                    result = false;
                }
            }
            return result;
        }
    }
}