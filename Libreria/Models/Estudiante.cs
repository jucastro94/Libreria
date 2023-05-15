using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Text;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Libreria.Models
{

    public class Estudiante
    {
        public static Enlace enlc = new Enlace();
        public string enlace { get; } = enlc.link + "Estudiante";

        [RegularExpression(@"^\d{6}$", ErrorMessage = "El id debe ser numeros de 6 diagitos")]
        public int idEstudiante { get; set; }


        [RegularExpression(@"^[A-Za-zÁÉÍÓÚÜáéíóúüñÑ]+(?: [A-Za-zÁÉÍÓÚÜáéíóúüñÑ]+)?$", ErrorMessage = "El nombre solo puede contener alfabeto de a-z en minusculas o mayusculas, incluye acentos")]
        public String nombre { get; set; }


        [RegularExpression(@"^[A-Za-zÁÉÍÓÚÜáéíóúüñÑ]+(?: [A-Za-zÁÉÍÓÚÜáéíóúüñÑ]+)?$", ErrorMessage = "El apellido solo puede contener alfabeto de a-z en minusculas o mayusculas, incluye acentos")]
        public String apellidos { get; set; }
        public int estado { get; set; }

        private static List<Estudiante> listaEstudiantes = new List<Estudiante>();

        public async void traerEstudiantes() {

            
            using (HttpClient httpClient = new HttpClient()) {
                httpClient.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await httpClient.GetAsync(enlace);
                if (response.IsSuccessStatusCode)
                {
                    string cuerpo = await response.Content.ReadAsStringAsync();
                    listaEstudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(cuerpo);

                }
                else {
                    Console.WriteLine("ha errado la peticion");
                }
            }
        }
        public List<Estudiante> buscarEstudiantes() {
            traerEstudiantes();
            return listaEstudiantes;
        }
    }
}