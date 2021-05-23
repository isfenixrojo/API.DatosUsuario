using System;
using System.Collections.Generic;
using System.Text;

namespace DatosUsuario.Models.DTO
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int IdAreas { get; set; }
        public string NombreArea { get; set; }
        public int Activo { get; set; }
    }
}
