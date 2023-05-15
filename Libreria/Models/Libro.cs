using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Libreria.Models
{
    public class Libro
    {

        public static Enlace enlc = new Enlace();
        public string enlace { get; } = enlc.link + "Libro";

        public int idLibros { get; set; }


        [RegularExpression(@"^\d{1,7}$", ErrorMessage = "el valor no puede superar los 7 digitos")]
        public int existencias { get; set; }


        [RegularExpression(@"^(?:[A-Za-zÁÉÍÓÚÜáéíóúüñÑ]+\s*)+$", ErrorMessage = "el valor no puede superar los 7 digitos")]
        public String nombre { get; set; }
        public int disponibilidad { get; set; }
        private static List<Libro> listaLibros;
        public bool disp { get; set; }
        
        public byte motivo { get; set; }

    }
}