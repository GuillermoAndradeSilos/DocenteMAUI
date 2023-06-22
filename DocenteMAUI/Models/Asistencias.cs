using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models
{
    public class Asistencias
    {
        public int id { get; set; }

        public int idAlumno { get; set; }

        public DateOnly fecha { get; set; }

        public int? asistio { get; set; } = 0;
    }
}
