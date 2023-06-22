using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models
{
    public class Alumno
    {
        public int id { get; set; }

        public string nombre { get; set; } = null!;

        public string direccion { get; set; } = null!;

        public string matricula { get; set; } = null!;

        public DateOnly fechaNacimiento { get; set; }

        public int edad { get; set; }

        public string curp { get; set; } = null!;

        public double peso { get; set; }

        public double estatura { get; set; }

        public string? alergico { get; set; }

        public int idGrupo { get; set; }
    }
}
