using DocenteMAUI.Models;
using DocenteMAUI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocenteMAUI.Services
{
    public class MateriasService
    {
        HttpClient client;
        public MateriasService()
        {
            client = new HttpClient();
            //escuelaescolarlaesquelita.sistemas19.com
            //localhost:44393
            client.BaseAddress = new Uri("https://escuelaescolarlaesquelita.sistemas19.com/");
        }
        public async Task<ObservableCollection<Asignatura>> ObtenerMaterias()
        {
            var result = await client.GetAsync("/ObtenerMaterias");
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                ObservableCollection<Asignatura> materias = JsonSerializer.Deserialize<ObservableCollection<Asignatura>>(aja);
                return materias;
            }
            else
                return null;
        }
        public async Task<string> ObtenerComentario(int id)
        {
            var result = await client.GetAsync($"/ObtenerComentario/{id}");
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                return aja;
            }
            else
                return null;
        }
        public async Task<ObservableCollection<Calificacion>> ObtenerCalificaciones(CalificacionDTO calif)
        {
            var result = await client.PostAsJsonAsync("/ObtenerCalificaciones", calif);
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                ObservableCollection<Calificacion> calificaciones = JsonSerializer.Deserialize<ObservableCollection<Calificacion>>(aja);
                return calificaciones;
            }
            else
                return null;
        }
        public async Task<ObservableCollection<ReporteDTO>> GenerarReporte(int idgrupo, int iddocente)
        {
            var result = await client.GetAsync($"/GenerarReporte/{idgrupo}/{iddocente}");
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                ObservableCollection<ReporteDTO> reporte = JsonSerializer.Deserialize<ObservableCollection<ReporteDTO>>(aja);
                return reporte;
            }
            else
                return null;
        }
        public async Task AgregarCalificacion(CalificacionDTO calif)
        {
            var result = await client.PostAsJsonAsync("/AgegarCalificacion", calif);
        }
        public async Task EditarCalificacion(CalificacionDTO calif)
        {
            var result = await client.PostAsJsonAsync("/EditarCalificacion", calif);
        }
    }
}
