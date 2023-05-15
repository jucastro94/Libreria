using Libreria.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class PrestamosController : Controller
    {

        private Prestamo prestamo = new Prestamo();
        public List<Prestamo> listPrestamo = new List<Prestamo>();
        // GET Prestamo
        public async Task<ActionResult> InicioPrestamo()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(prestamo.enlace);
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    listPrestamo = JsonConvert.DeserializeObject<List<Prestamo>>(cuerpo);
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return View(listPrestamo);
            }

        }

        //Get Prestamo
        public async Task<ActionResult> buscarPrestamo(int id)
        {
            Prestamo prest = new Prestamo();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(prestamo.enlace + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    prest = JsonConvert.DeserializeObject<Prestamo>(cuerpo);
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return View(prest);
            }

        }
        [HttpGet]
        public async Task<ActionResult> registroPrestamo()
        {
            Prestamo lstP = new Prestamo();
            Enlace enl = new Enlace();
            List<Estudiante> listEstudiantes = new List<Estudiante>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(enl.link+ "/Estudiante");
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    listEstudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(cuerpo);
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
            }
            lstP.listEstudiantes = listEstudiantes;

            List<Libro> lisLibro = new List<Libro>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(enl.link + "/Libro");
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    lisLibro = JsonConvert.DeserializeObject<List<Libro>>(cuerpo);
                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
            }
            lstP.listLibros = lisLibro;

            return View(lstP);
        }
        //Post Prestamo
        [HttpPost]
        public async Task<ActionResult> registrarPrestamo(Prestamo prestamo)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();

                string data = JsonConvert.SerializeObject(prestamo);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(prestamo.enlace, content);

                if (response.IsSuccessStatusCode)
                {
                    string msg = "";
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    msg = (cuerpo == "true") ? "se ha registrado satisfactoriamente" : "no se ha podido registrar";
                    Trace.TraceInformation(msg);

                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return RedirectToAction("InicioPrestamo", "Prestamos");
            }

        }
        [HttpGet]
        public ActionResult modificar()
        {
            DateTime fechaHoy = DateTime.Now;
            return View(fechaHoy);
        }
        //Post Prestamo
        [HttpPost]
        public async Task<ActionResult> modificarPrestamo(Prestamo prestamo)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //Trace.TraceInformation(prestamo.fechaDevolucion.ToString());
                httpClient.DefaultRequestHeaders.Clear();

                string data = JsonConvert.SerializeObject(prestamo);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync(prestamo.enlace, content);

                if (response.IsSuccessStatusCode)
                {
                    string msg = "";
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    msg = (cuerpo == "true") ? "se ha modificado satisfactoriamente" : "no se ha podido registrar";

                }
                else
                {
                    Console.WriteLine("ha errado la peticion");
                }
                return RedirectToAction("InicioPrestamo", "Prestamos");
            }

        }

    }
}