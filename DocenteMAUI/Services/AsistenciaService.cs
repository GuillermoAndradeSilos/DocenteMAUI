using DocenteMAUI.Models;
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
    public class AsistenciaService
    {
        HttpClient client;
        public AsistenciaService()
        {
            client= new HttpClient();
            //escuelaescolarlaesquelita.sistemas19.com
            //localhost:44393
            client.BaseAddress = new Uri("https://escuelaescolarlaesquelita.sistemas19.com/");
        }
        public async Task<ObservableCollection<Asistencias>> GetAsistencia(int id)
        {
            var result = await client.GetAsync($"ObtenerAsistencias/{id}");
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                ObservableCollection<Asistencias> asistencias = JsonSerializer.Deserialize<ObservableCollection<Asistencias>>(aja);
                return asistencias;
            }
            else
                return null;
        }
        public async Task AgregarAsistencia(Asistencias alumno)
        {
            var result = await client.PostAsJsonAsync("AgregarAsistencia", alumno);
        }
        public async Task EditarAsistencia(Asistencias alumno)
        {
            var result = await client.PostAsJsonAsync("EditarAsistencia", alumno);
        }
    }
}
