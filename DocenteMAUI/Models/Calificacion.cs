using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models
{
    public class Calificacion
    {
        public int id { get; set; }

        public int idAsignatura { get; set; }

        public int idAlumno { get; set; }

        public int idDocente { get; set; }

        public int idPeriodo { get; set; }

        public double calificacion1 { get; set; }

        public int unidad { get; set; }
    }
}
