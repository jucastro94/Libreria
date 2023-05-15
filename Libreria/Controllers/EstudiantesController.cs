using Libreria.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class EstudiantesController : Controller
    {
        private Estudiante estudiante = new Estudiante();
        public List<Estudiante> listEstufiantes = new List<Estudiante>();
        // GET: Estudiantes
        public async Task<ActionResult> Inicio()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(estudiante.enlace);
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    listEstufiantes = JsonConvert.DeserializeObject<List<Estudiante>>(cuerpo);
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return View(listEstufiantes);
            }

        }

        //Get estudiante
        public async Task<ActionResult> buscarestudiante(int id)
        {
            Estudiante est = new Estudiante();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(estudiante.enlace + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    est = JsonConvert.DeserializeObject<Estudiante>(cuerpo);
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return View(est);
            }

        }
        [HttpGet]
        public ActionResult registro() {
            return View();
        }
        //Post Estudiante
        [HttpPost]
        public async Task<ActionResult> registrarEstudiante( Estudiante estudiante)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();

                string data = JsonConvert.SerializeObject(estudiante);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(estudiante.enlace, content);

                if (response.IsSuccessStatusCode)
                {
                    string msg = "";
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    msg = (cuerpo == "true") ?  "se ha registrado satisfactoriamente" : "no se ha podido registrar" ;
                    
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return RedirectToAction("Inicio", "Estudiantes");
            }

        }
        [HttpGet]
        public ActionResult eliminar(int id, string nom, string apell)
        {
            Estudiante est = new Estudiante();
            est.idEstudiante = id;
            est.nombre = nom;
            est.apellidos = apell;
            return View(est);
        }
        //Delete Estudiante
        [HttpPost]
        public async Task<ActionResult> eliminarEstudiante(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.DeleteAsync(estudiante.enlace+"/"+id);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("se ha eliminado correctamente");
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return RedirectToAction("Inicio", "Estudiantes");
            }

        }
    }
}