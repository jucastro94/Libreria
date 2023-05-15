using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Net.WebRequestMethods;

namespace Libreria.Models
{
    public class Enlace
    {
        public string link { get; }
        public Enlace()
        {
            link = "https://localhost:44370/api/";
        }
        
    }
}