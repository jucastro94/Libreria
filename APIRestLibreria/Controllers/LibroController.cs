using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace APIRestLibreria.Controllers
{
    public class LibroController : ApiController
    {
        private Libro libro = new Libro();
        //Get api/libro
        public IEnumerable<Libro> Get()
        {
            return libro.consultaLibros();

        }

        //Get api/libro/1
        public Libro Get(int id)
        {
            return libro.consultaLibro(id);
        }

        //POST api/libro
        public bool Post([FromBody] Libro lib)
        {
            bool result;
            Libro encontrado = libro.consultaLibro(lib.idLibros);
            if (encontrado == null)
            {
                result = libro.registrarLibro(lib) ? true : false;
            }
            else
            {
                result = false;
            }
            return result;
        }
        //Modificar api/libro
        public bool Put( [FromBody] Libro lib)
        {
            return libro.modificarExistencia(lib.idLibros, lib.existencias, lib.motivo);
        }
    }
}
