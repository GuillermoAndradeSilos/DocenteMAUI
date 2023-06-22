using DocenteMAUI.Models;
using DocenteMAUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DocenteMAUI.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private readonly LoginService loginservice;
        public ICommand IniciarSesionCommand { get; set; }
        public Usuario UsuarioLogin { get; set; } = new Usuario();
        public string Mensaje { get; set; }
        public LoginViewModel(LoginService loginservice)
        {
            this.loginservice = loginservice;
            IniciarSesionCommand = new Command(IniciarSesion);
        }
        AlumnosService alumnoservice = new AlumnosService();
        private async void IniciarSesion()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    if (!await loginservice.Login(UsuarioLogin))
                    {
                        Mensaje = "Nombre o contraseña incorrecta";
                        PropertyChange("Mensaje");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("//ListaAlumnosView");
                        Shell.Current.BindingContext = new MainViewModel(loginservice, alumnoservice);
                    }
                }
                else
                {
                    Mensaje = "No hay acceso a internet";
                    PropertyChange("Mensaje");
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                PropertyChange("Mensaje");
            }
        }

        public void PropertyChange(string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
