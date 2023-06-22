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
using static System.Net.Mime.MediaTypeNames;

namespace DocenteMAUI.Services
{
    public class AlumnosService
    {
        HttpClient client;
        public AlumnosService()
        {
            client = new HttpClient();
            //escuelaescolarlaesquelita.sistemas19.com
            //localhost:44393
            client.BaseAddress = new Uri("https://escuelaescolarlaesquelita.sistemas19.com/");
        }
        public async Task<ObservableCollection<Alumno>> GetAlumnos(AlumnoDTO alumno)
        {
            var result = await client.GetAsync($"AlumnoGrupo/{alumno.Alumno.idGrupo}");
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                ObservableCollection<Alumno> alumnos = JsonSerializer.Deserialize<ObservableCollection<Alumno>>(aja);
                return alumnos;
            }
            else
                return null;
        }
        public async Task AgregarAlumno(AlumnoDTO alumno)
        {
            var result = await client.PostAsJsonAsync("AgregarAlumno", alumno);
        }
        public async Task EditarAlumno(AlumnoDTO alumno)
        {
            var result = await client.PostAsJsonAsync("/EditarAlumno", alumno);
        }
        public async Task EliminarAlumno(AlumnoDTO alumno)
        {
            var result = await client.PostAsJsonAsync("/EliminarAlumno", alumno);
        }
    }
}
