using DocenteMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocenteMAUI.Services
{
    public class LoginService
    {
        HttpClient client;
        public Docente Docente { get; set; } = new Docente();
        public Grupo Grupo { get; set; } = new Grupo();
        public LoginService()
        {
            client = new HttpClient();
            //escuelaescolarlaesquelita.sistemas19.com
            //localhost:44393
            client.BaseAddress = new Uri("https://escuelaescolarlaesquelita.sistemas19.com/");
        }
        public async Task<bool> Login(Usuario login)
        {
            var result = await client.PostAsJsonAsync("api/login/Login/", login);
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                Docente = JsonSerializer.Deserialize<Docente>(aja);
                await GetGrupo(Docente.id);
                return true;
            }
            else
                return false;
        }
        public async Task GetGrupo(int id)
        {
            var result = await client.GetAsync($"GrupoDocente/{id}");
            if (result.IsSuccessStatusCode)
            {
                var aja = await result.Content.ReadAsStringAsync();
                Grupo = JsonSerializer.Deserialize<Grupo>(aja);
            }
        }
    }
}
