using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models.DTOs
{
    public class CalificacionDTO
    {
        public Calificacion calificacion { get; set; } = null!;
        public string comentario { get; set; } = "Sin comentarios";
        public int alumno { get; set; }
        public int asignatura { get; set; }
    }
}
