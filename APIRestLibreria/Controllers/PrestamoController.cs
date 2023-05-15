using APIRestLibreria.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace APIRestLibreria.Controllers
{
    public class PrestamoController : ApiController
    {

        private Prestamo prestamo = new Prestamo();
        //Get api/prestamo
        public IEnumerable<Prestamo> Get()
        {
            return prestamo.consultaPrestamos();

        }

        //Get api/prestamo/1
        public Prestamo Get(int id)
        {
            return prestamo.consultaPrestamo(id);
        }

        //POST api/prestamo
        public bool Post([FromBody] Prestamo prest)
        {
            bool result;
            Prestamo encontrado = prestamo.consultaPrestamo(prest.idPrestamo);
            if (encontrado == null)
            {
                result = prestamo.registrarPrestamo(prest) ? true : false;
            }
            else
            {
                result = false;
            }
            return result;
        }
        //Modificar api/prestamo
        public bool Put([FromBody] Prestamo lib)
        {
            
            return prestamo.modificarPrestamo(lib);
        }
    }
}