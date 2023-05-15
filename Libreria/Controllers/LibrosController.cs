using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Libreria.Models;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Controllers
{
    public class LibrosController : Controller
    {
        private Libro libro = new Libro();
        public List<Libro> lisLibro = new List<Libro>();
        // GET: Libro
        public async Task<ActionResult> InicioLibro()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(libro.enlace);
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    lisLibro = JsonConvert.DeserializeObject<List<Libro>>(cuerpo);
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return View(lisLibro);
            }

        }

        //Get Libro
        public async Task<ActionResult> buscarLibro(int id)
        {
            Libro lib = new Libro();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(libro.enlace + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    lib = JsonConvert.DeserializeObject<Libro>(cuerpo);
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return View(lib);
            }

        }
        [HttpGet]
        public ActionResult registroLibro()
        {
            return View();
        }
        //Post Libro
        [HttpPost]
        public async Task<ActionResult> registrarLibro(Libro libro)
        {
            if (libro.disp == true)
            {
                libro.disponibilidad = 1;
            }
            else {
                libro.disponibilidad = 0;
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();

                string data = JsonConvert.SerializeObject(libro);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(libro.enlace, content);

                if (response.IsSuccessStatusCode)
                {
                    string msg = "";
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    msg = (cuerpo == "true") ? "se ha registrado satisfactoriamente" : "no se ha podido registrar";

                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return RedirectToAction("InicioLibro", "Libros");
            }

        }
        [HttpGet]
        public ActionResult eliminarinicialLib(int id, string nom)
        {
            Libro lib = new Libro();
            lib.idLibros = id;
            lib.nombre = nom;
            return View(lib);
        }
        //Delete Libro
        [HttpPost]
        public async Task<ActionResult> eliminarLibro(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.DeleteAsync(libro.enlace + "/" + id);

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