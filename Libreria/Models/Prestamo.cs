using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Libreria.Models
{
    public class Prestamo
    {
        public static Enlace enlc = new Enlace();
        public string enlace { get; } = enlc.link + "Prestamo";

        public int idPrestamo { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un libro")]
        public int idLibro { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estudiante")]

        public int idEstudiante { get; set; }

        public String nomLibro { get; set; }
        public String nomEstudiante { get; set; }


        [Required(ErrorMessage = "La fecha de préstamo es requerida")]
        public DateTime fechaPrestamo { get; set; }


        [Required(ErrorMessage = "La fecha de devolcion es requerida")]
        public DateTime fechaDevolucion { get; set; }

        public int diasPrestado { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un libro")]
        private static List<Prestamo> listaPrestamos;

        public List<Estudiante> listEstudiantes { get; set; }
        public List<Libro> listLibros { get; set; }
    }
}