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
    public class PadresService
    {
        HttpClient client;
        public PadresService()
        {
            client = new HttpClient();
            //escuelaescolarlaesquelita.sistemas19.com
            //localhost:44393
            client.BaseAddress = new Uri("https://escuelaescolarlaesquelita.sistemas19.com/");
        }
        public async Task<ObservableCollection<Tutor>> GetTutor()
        {
            var result = await client.GetAsync("/Getpadre");
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                ObservableCollection<Tutor> tutores = JsonSerializer.Deserialize<ObservableCollection<Tutor>>(aja);
                return tutores;
            }
            else
                return null;
        }
        public async Task<ObservableCollection<Tutor>> GetTutor(int id)
        {
            var result = await client.GetAsync($"/Getpadre/{id}");
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                ObservableCollection<Tutor> tutores = JsonSerializer.Deserialize<ObservableCollection<Tutor>>(aja);
                return tutores;
            }
            else
                return null;
        }
        public async Task AgregarPadre(TutorDTO tutor)
        {
            var result = await client.PostAsJsonAsync("/AgregarPadre", tutor);
        }
        public async Task EditarTutor(TutorDTO tutor)
        {
            var result = await client.PostAsJsonAsync("/EditarPadre", tutor);
        }
        public async Task<bool> EliminarTutor(TutorDTO tutor)
        {
            var result = await client.PostAsJsonAsync("/EliminarPadre", tutor);
            if(result.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }
    }
}
