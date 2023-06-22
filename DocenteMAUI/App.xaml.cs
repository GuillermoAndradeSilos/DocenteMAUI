using DocenteMAUI.Views;

namespace DocenteMAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Routing.RegisterRoute("AgregarAlumnoView", typeof(AgregarAlumnoView));
		Routing.RegisterRoute("AgregarCalificacionView", typeof(AgregarCalificacionView));
		Routing.RegisterRoute("EditarAlumnoView", typeof(EditarAlumnoView));
		Routing.RegisterRoute("EditarCalificacionView", typeof(EditarCalificacionView));
		Routing.RegisterRoute("ListaAlumnosView", typeof(ListaAlumnosView));
		Routing.RegisterRoute("Login", typeof(Login));
		Routing.RegisterRoute("CalificacionesView", typeof(CalificacionesView));
		Routing.RegisterRoute("AgregarTutorView", typeof(AgregarTutorView));
		Routing.RegisterRoute("EditarTutorView", typeof(EditarTutorView));
		Routing.RegisterRoute("ListaTutorView", typeof(ListaTutorView));
		Routing.RegisterRoute("VerCalificacionView", typeof(VerCalificacionView));
        Routing.RegisterRoute("AsistenciasAlumnoView", typeof(AsistenciasAlumnoView));
        Routing.RegisterRoute("AgregarAsistencia", typeof(AgregarAsistencia));
        Routing.RegisterRoute("EditarAsistencia", typeof(EditarAsistencia));
        MainPage = new AppShell();
	}
}
