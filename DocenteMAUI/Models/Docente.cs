using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocenteMAUI.Models
{
    public class Docente
    {
        public int id { get; set; }
        public string nombre { get; set; } = null!;
        public string apellidoPaterno { get; set; } = null!;
        public string apellidoMaterno { get; set; } = null!;
        public string correo { get; set; } = null!;
        public string telefono { get; set; } = null!;
        public int edad { get; set; }
        public int tipoDocente { get; set; }
        public int idUsuario { get; set; }
    }
}
