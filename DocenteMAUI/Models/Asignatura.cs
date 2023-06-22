using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models
{
    public class Asignatura
    {
        public int id { get; set; }
        public string nombre { get; set; } = null!;
        public int tipoAsignatura { get; set; }
    }
}
