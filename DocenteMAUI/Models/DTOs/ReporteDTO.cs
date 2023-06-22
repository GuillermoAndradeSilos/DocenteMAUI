using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models.DTOs
{
    public class ReporteDTO
    {
        public string nombre { get; set; } = null!;
        public string asignatura { get; set; } = null!;
        public Calificacion? calificacion { get; set; }
    }
}
