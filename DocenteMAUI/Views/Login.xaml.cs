using DocenteMAUI.Services;
using DocenteMAUI.ViewModels;

namespace DocenteMAUI.Views;

public partial class Login : ContentPage
{
	LoginViewModel viewModel;
	LoginService loginService;
	public Login()
	{
		InitializeComponent();
		loginService= new LoginService();
		viewModel = new LoginViewModel(loginService);
		this.BindingContext = viewModel;
	}
}