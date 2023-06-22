using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Usuario1 { get; set; } = null!;
        public string Contraseña { get; set; } = "contrasenia";
        public int Rol { get; set; }
    }
}
