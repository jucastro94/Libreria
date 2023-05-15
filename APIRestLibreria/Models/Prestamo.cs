using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace APIRestLibreria.Models
{
    public class Prestamo
    {
        private Conexion conect = new Conexion();
        SqlCommand cmd;
        public int idPrestamo { get; set; }
        public int idLibro { get; set; }
        public int idEstudiante { get; set; }
        public String nomLibro { get; set; }
        public String nomEstudiante { get; set; }
        public DateTime fechaPrestamo { get; set; }
        public DateTime fechaDevolucion { get; set; }

       
        private static List<Prestamo> listaPrestamos;

        public List<Prestamo> consultaPrestamos()
        {
            listaPrestamos = new List<Prestamo>();
            String query = "SELECT PKidPrestamo as PKidPrestamo, Libros.PKidLibros as PKidLibros,  Libros.nombre  as FKidLibro, Estudiantes.nombre  FKidEstudiante, Prestamos.fechaPrestamo as fechaPrestamo, Prestamos.fechaDevolucion as fechaDevolucion" +
                "  FROM  Prestamos " +
                "  INNER JOIN Libros ON FKidLibro  = PKidLibros  " +
                "  INNER JOIN Estudiantes ON FKidEstudiante = PKidEstudiante ";
                

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
               try
               {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DateTime temp;
                        String date;
                        while (dr.Read())
                        {
                            Prestamo nvPrestamo = new Prestamo();
                            nvPrestamo.idPrestamo = Convert.ToInt32(dr["PKidPrestamo"]);
                            nvPrestamo.idLibro = Convert.ToInt32(dr["PKidLibros"]);
                            nvPrestamo.nomLibro = dr["FKidLibro"].ToString();
                            nvPrestamo.nomEstudiante = dr["FKidEstudiante"].ToString();

                            date = dr["fechaPrestamo"].ToString();
                            temp = Convert.ToDateTime(date);
                             nvPrestamo.fechaPrestamo = temp.Date;

                            if (string.IsNullOrEmpty(dr["fechaDevolucion"].ToString())){
                                nvPrestamo.fechaDevolucion = DateTime.MinValue;
                            }
                            else{
                                date = dr["fechaDevolucion"].ToString();
                                temp = Convert.ToDateTime(date);
                                nvPrestamo.fechaDevolucion = temp.Date;
                            }

                            listaPrestamos.Add(nvPrestamo);
                        }
                    }
                }
                catch (Exception e)
               {
                   Console.WriteLine("consulta de prestamo ha errado.", e);
                    

               }
            }
            return listaPrestamos;
        }
        public Prestamo consultaPrestamo(int id)
        {
            Prestamo nvPrestamo;
            String query = "SELECT PKidPrestamo as PKidPrestamo, Libros.PKidLibros as PKidLibros, Libros.nombre  as FKidLibro, Estudiantes.nombre  FKidEstudiante, Prestamos.fechaPrestamo as fechaPrestamo, Prestamos.fechaDevolucion as fechaDevolucion" +
                "  FROM  Prestamos" +
                " INNER JOIN Libros ON FKidLibro  = PKidLibros  " +
                " INNER JOIN Estudiantes ON FKidEstudiante = PKidEstudiante"  +
                " WHERE PKidPrestamo = @id";
            //String query = "SELECT * FROM Prestamos WHERE Prestamos.PKidPrestamos = @id";

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DateTime temp;
                        String date;
                        while (dr.Read())
                        {
                            nvPrestamo = new Prestamo();
                            nvPrestamo.idPrestamo = Convert.ToInt32(dr["PKidPrestamo"]);
                            nvPrestamo.idLibro = Convert.ToInt32(dr["PKidLibros"]);
                            nvPrestamo.nomLibro = dr["FKidLibro"].ToString();
                            nvPrestamo.nomEstudiante = dr["FKidEstudiante"].ToString();
                            date = dr["fechaPrestamo"].ToString();
                            temp = Convert.ToDateTime(date);
                            nvPrestamo.fechaPrestamo = temp.Date;

                            if (string.IsNullOrEmpty(dr["fechaDevolucion"].ToString()))
                            {
                                nvPrestamo.fechaDevolucion = DateTime.MinValue;
                            }
                            else
                            {
                                date = dr["fechaDevolucion"].ToString();
                                temp = Convert.ToDateTime(date);
                                nvPrestamo.fechaDevolucion = temp.Date;
                            }
                            return nvPrestamo;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("consulta de estudiante ha errado.", e);
                }
            }
            return null;
        }
        public bool registrarPrestamo(Prestamo prestamo)
        {
            bool result = false;
            String query = "INSERT INTO Prestamos(FKidLibro, FKidEstudiante, fechaPrestamo, fechaDevolucion) VALUES(@idLibro, @idEstudn ,@fechaP, @fechaD)";

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {
                
                cmd.Parameters.Add("@idLibro", SqlDbType.Int);
                cmd.Parameters.Add("@idEstudn", SqlDbType.Int);
                cmd.Parameters.Add("@fechaP", SqlDbType.Date);
                cmd.Parameters.Add("@fechaD", SqlDbType.Date);
                cmd.Parameters["@idLibro"].Value = prestamo.idLibro;
                cmd.Parameters["@idEstudn"].Value = prestamo.idEstudiante;
                cmd.Parameters["@fechaP"].Value = prestamo.fechaPrestamo;
                cmd.Parameters["@fechaD"].Value = prestamo.fechaDevolucion;
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        Trace.TraceInformation("insersion de prestamo hecha.");

                        result = true;
                    }
                    else { result = false; }
                }
                catch (Exception e)
                {
                    Trace.TraceInformation("insersion de prestamo ha errado.", e);
                    result = false;
                }
            }
            return result;
        }

        public bool modificarPrestamo(Prestamo prestamo)
        {
            Trace.TraceInformation(prestamo.idPrestamo.ToString());
            bool result;
            String query = "UPDATE Prestamos SET fechaDevolucion = @fechaD WHERE PKidPrestamo = @id";

            conect.conectar();
            using (cmd = new SqlCommand(query, conect.connection()))
            {

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = prestamo.idPrestamo;
                cmd.Parameters.Add("@fechaD", SqlDbType.DateTime);
                cmd.Parameters["@fechaD"].Value = prestamo.fechaDevolucion;
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
                    Console.WriteLine("modificacion del prestamo ha errado.", e);
                    result = false;
                }
            }
            return result;
        }
    }
}