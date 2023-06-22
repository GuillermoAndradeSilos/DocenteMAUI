using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models.DTOs
{
    public class TutorDTO
    {
        public Tutor Tutor { get; set; } = new Tutor();
        public Usuario? Usuario { get; set; }
        public int Alumno { get; set; }
        public string? Curp { get; set; }
    }
}
