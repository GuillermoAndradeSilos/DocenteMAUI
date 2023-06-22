using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models
{
    public class Tutor
    {
        public int id { get; set; }

        public string nombre { get; set; } = null!;

        public string direccion { get; set; } = null!;

        public string telefono { get; set; } = null!;

        public string? celular { get; set; }

        public string? email { get; set; }

        public int idusuario { get; set; }

        public int ocupacion { get; set; }
    }
}
