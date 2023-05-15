using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace APIRestLibreria.Controllers
{
    public class EstudianteController : ApiController
    {
        private Estudiante estudiante = new Estudiante();
        //Get api/estudiante
        public IEnumerable<Estudiante> Get()
        {
            return estudiante.consultaEstudintes();
            
        }

        //Get api/estudiante/1
        public Estudiante Get(int id)
        {
            return estudiante.consultaEstudinte(id);
        }

        //POST api/estudiante
        public bool Post([FromBody] Estudiante est)
        {
            bool result;
            Estudiante encontrado = estudiante.consultaEstudinte(est.idEstudiante);
            if (encontrado == null)
            {
                result = estudiante.registrarEstudiante(est)?  true :  false ;
            }
            else
            {
                result = false;
            }
            return result;
        }
        //DELETE api/estudiante
        public bool Delete(int id)
        {
            return estudiante.eliminar(id);
        }

    }
}