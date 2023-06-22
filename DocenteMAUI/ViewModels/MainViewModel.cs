using DocenteMAUI.Models;
using DocenteMAUI.Models.DTOs;
using DocenteMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace DocenteMAUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly LoginService loginservice;
        private readonly AlumnosService alumnoservice;
        private readonly MateriasService materiaservice;
        private readonly PadresService padreservice;
        private readonly AsistenciaService asistenciaservice;
        public ObservableCollection<Alumno> Alumnos { get; set; } = new ObservableCollection<Alumno>();
        public ObservableCollection<Tutor> Tutores { get; set; } = new ObservableCollection<Tutor>();
        public List<string> Curps { get; set; } = new List<string>();
        public ObservableCollection<Asignatura> Materias { get; set; } = new ObservableCollection<Asignatura>();
        public ObservableCollection<Calificacion> Calificaciones { get; set; } = new ObservableCollection<Calificacion>();
        public ObservableCollection<Asistencias> Asistencias { get; set; } = new ObservableCollection<Asistencias>();
        public ICommand NavegarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand NavegarCalificacionesCommand { get; set; }
        public ICommand GuardarCalificacionesCommand { get; set; }
        public ICommand GuardarPadreCommand { get; set; }
        public ICommand NavegarPadreCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EliminarTutorCommand { get; set; }
        public ICommand GenerarReporteCommand { get; set; }
        public ICommand VerAsistenciasCommand { get; set; }
        public ICommand GuardarAsistenciaCommand { get; set; }
        public string Error { get; set; }
        public string CurpSeleccionada { get; set; }
        public string Comentario { get; set; }
        public Alumno AlumnoSelect { get; set; }
        public Alumno AlumnoCopia { get; set; }
        public Tutor TutorSelect { get; set; }
        public Tutor TutorCopia { get; set; }
        public Calificacion CalificacionSelect { get; set; }
        public Calificacion CalificacionCopia { get; set; }
        public Asignatura MateriaSelect { get; set; }
        public Asignatura MateriaCopia { get; set; }
        public Asistencias AsistenciaSelect { get; set; }
        public Asistencias AsistenciaCopia { get; set; }
        public MainViewModel(LoginService loginservice, AlumnosService alumnoservice)
        {
            this.loginservice = loginservice;
            this.alumnoservice = alumnoservice;
            asistenciaservice = new AsistenciaService();
            materiaservice = new MateriasService();
            padreservice = new PadresService();
            NavegarCommand = new Command<string>(Navegar);
            GuardarCommand = new Command(Guardar);
            NavegarCalificacionesCommand = new Command<string>(NavegarCalificacion);
            GuardarCalificacionesCommand = new Command(GuardarCalificaciones);
            GuardarPadreCommand = new Command(GuardarPadre);
            EliminarCommand = new Command(Eliminar);
            NavegarPadreCommand = new Command<string>(NavegarTutor);
            EliminarTutorCommand = new Command(EliminarTutor);
            GenerarReporteCommand = new Command(GenerarReporte);
            VerAsistenciasCommand = new Command<string>(NavegarAsistencia);
            GuardarAsistenciaCommand = new Command(GuardarAsistencia);
            AlumnoCopia = new Alumno();
            AlumnoSelect = new Alumno();
            TutorSelect = new Tutor();
            TutorCopia = new Tutor();
            ObtenerAlumnos();
            Actualizar();
        }
        private async void GuardarAsistencia(object obj)
        {
            Error = "";
            if (AsistenciaSelect != null)
            {
                if (VistaActual != "EditarAsistencia")
                {
                    if (AsistenciaSelect.asistio == null)
                        Error = "Favor de declarar la asistencia del alumno como 0 o 1.";
                    if (string.IsNullOrWhiteSpace(Error))
                    {
                        Asistencias test = new Asistencias()
                        {
                            asistio = AsistenciaSelect.asistio,
                            fecha = AsistenciaSelect.fecha,
                            idAlumno = AlumnoSelect.id
                        };
                        await AgregarAsistencia(test);
                        AsistenciaSelect = new Asistencias();
                        AsistenciaCopia = new Asistencias();
                        NavegarAsistencia("AsistenciasAlumnoView");
                    }
                    else
                        Error = "Esa calificación ya se encuentra registrado";
                }
                else
                {
                    if (AsistenciaSelect.asistio == null)
                        Error = "Favor de declarar la asistencia del alumno como 0 o 1.";
                    if (string.IsNullOrWhiteSpace(Error))
                    {
                        Asistencias test = new Asistencias()
                        {
                            asistio = AsistenciaSelect.asistio,
                            fecha = AsistenciaSelect.fecha,
                            idAlumno = AlumnoSelect.id
                        };
                        await EditarAsistencia(test);
                        AsistenciaSelect = new Asistencias();
                        AsistenciaCopia = new Asistencias();
                        NavegarAsistencia("AsistenciasAlumnoView");
                    }
                }
            }
            else
                Error = "Esto ni siquiera deberia de salir, revisar bindings x.x";
            Actualizar();
        }

        private async Task EditarAsistencia(Asistencias test)
        {
            await asistenciaservice.EditarAsistencia(test);
        }

        private async Task AgregarAsistencia(Asistencias test)
        {
            await asistenciaservice.AgregarAsistencia(test);
        }

        private async void NavegarAsistencia(string obj)
        {
            if (obj == "AgregarAsistencia")
            {
                AsistenciaSelect = new Asistencias();
                await Shell.Current.GoToAsync("//" + obj);
            }
            else if (obj == "AsistenciasAlumnoView")
            {
                Asistencias.Clear();
                Asistencias = await asistenciaservice.GetAsistencia(AlumnoSelect.id);
                await Shell.Current.GoToAsync("//" + obj);
            }
            else if (obj == "EditarAsistencia" && AsistenciaSelect != null)
            {
                AsistenciaCopia = new Asistencias()
                {
                    asistio = AsistenciaSelect.asistio,
                    fecha = AsistenciaSelect.fecha
                };
                await Shell.Current.GoToAsync("//" + obj);
                VistaActual = obj;
            }
            else
                Error = "Favor de seleccionar el alumno a editar.";
            Actualizar();
        }
        private async void GenerarReporte()
        {
            var aja = await materiaservice.GenerarReporte(loginservice.Grupo.id, loginservice.Docente.id);
        }
        private async void NavegarCalificacion(string obj)
        {
            if (obj == "AgregarCalificacionView")
            {
                CalificacionSelect = new Calificacion();
                await Shell.Current.GoToAsync("//" + obj);
            }
            else if (obj == "CalificacionesView")
            {
                MateriaSelect = Materias.Where(x => x.nombre == obj).FirstOrDefault();
                Calificaciones = await materiaservice.ObtenerCalificaciones(new CalificacionDTO()
                {
                    asignatura = MateriaSelect.id,
                    alumno = AlumnoSelect.id
                });
                await Shell.Current.GoToAsync("//" + obj);
            }
            else
            {
                MateriaSelect = Materias.Where(x => x.nombre == obj).FirstOrDefault();
                Calificaciones = await materiaservice.ObtenerCalificaciones(new CalificacionDTO()
                {
                    asignatura = MateriaSelect.id,
                    alumno = AlumnoSelect.id
                });
                Comentario = await materiaservice.ObtenerComentario(AlumnoSelect.id);
                await Shell.Current.GoToAsync("//VerCalificacionView");
                VistaActual = obj;
            }
            Actualizar();
        }
        private async void EliminarTutor(object obj)
        {
            if (TutorSelect != null)
            {
                var enviar = new TutorDTO() { Tutor = TutorSelect };
                if (!await padreservice.EliminarTutor(enviar))
                    Error = "El tutor no tiene un alumno bajo su responsabilidad";
                GetAllPadres();
            }
            else
                Error = "Favor de seleccionar el tutor a eliminar.";
            Actualizar();
        }
        private void GetAsistencias()
        {
            NavegarCalificacion(VistaActual);
        }
        private async void GuardarCalificaciones()
        {
            Error = "";
            if (CalificacionSelect != null)
            {
                if (CalificacionSelect.calificacion1 < 0 && CalificacionSelect.calificacion1 > 10)
                    Error = "Favor de declarar la calificación del alumno";
                if (CalificacionSelect.unidad < 0)
                    Error = "Favor de declarar la unidad de la calificacion";
                var validar = Calificaciones.FirstOrDefault(x => x.idAlumno == AlumnoSelect.id
                && x.idAsignatura == MateriaSelect.id && x.unidad == CalificacionSelect.unidad);
                if (string.IsNullOrWhiteSpace(Error) && validar == null)
                {
                    CalificacionSelect.idAlumno = AlumnoSelect.id;
                    CalificacionDTO test = new CalificacionDTO()
                    {
                        calificacion = CalificacionSelect,
                        comentario = Comentario
                    };
                    test.calificacion.idDocente = loginservice.Docente.id;
                    test.calificacion.idAsignatura = MateriaSelect.id;
                    await AgregarCalificacion(test);
                    CalificacionSelect = new Calificacion();
                    CalificacionCopia = new Calificacion();
                    NavegarCalificacion("CalificacionesView");
                    GetAsistencias();
                }
                else
                    Error = "Esa calificación ya se encuentra registrado";
            }
            else
                Error = "Esto ni siquiera deberia de salir, revisar bindings x.x";
            Actualizar();
        }
        private async Task AgregarCalificacion(CalificacionDTO test)
        {
            await materiaservice.AgregarCalificacion(test);
        }
        private async void GetGrupoPadres()
        {
            Tutores = await padreservice.GetTutor(loginservice.Grupo.id);
        }
        private async void GetAllPadres()
        {
            Tutores = await padreservice.GetTutor();
            Actualizar();
        }
        private async void GuardarPadre()
        {
            Error = "";
            if (TutorSelect != null)
            {
                if (VistaActual == "AgregarTutorView")
                {
                    if (string.IsNullOrWhiteSpace(TutorSelect.nombre))
                        Error = "Favor de escribir el nombre del tutor.";
                    if (string.IsNullOrWhiteSpace(TutorSelect.direccion))
                        Error = "Favor de escribir la dirección donde reside el tutor.";
                    if (string.IsNullOrWhiteSpace(TutorSelect.telefono))
                        Error = "Favor de escribir el número telefonico del tutor.";
                    var validar = Alumnos.FirstOrDefault(x => x.curp == AlumnoSelect.curp);
                    if (string.IsNullOrWhiteSpace(Error) && validar == null)
                    {
                        TutorDTO test = new TutorDTO()
                        {
                            Usuario = new Usuario() { Usuario1 = TutorSelect.nombre },
                            Tutor = new Tutor()
                            {
                                nombre = TutorSelect.nombre,
                                celular = TutorSelect.celular,
                                ocupacion = 0,
                                telefono = TutorSelect.telefono,
                                direccion = TutorSelect.direccion,
                                email = TutorSelect.email,
                                idusuario = 0
                            }
                        };
                        await AgregarPadre(test);
                        TutorSelect = new Tutor();
                        TutorCopia = new Tutor();
                        NavegarTutor("ListaTutorView");
                        GetAllPadres();
                    }
                    else
                        Error = "Esa tutor ya se encuentra registrado";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(TutorCopia.direccion))
                        Error = "Favor de escribir la dirección donde reside el tutor.";
                    if (string.IsNullOrWhiteSpace(TutorCopia.telefono))
                        Error = "Favor de escribir el número telefonico del tutor.";
                    if (string.IsNullOrWhiteSpace(TutorCopia.celular))
                        Error = "Favor de escribir el número de celular del tutor.";
                    if (string.IsNullOrWhiteSpace(TutorCopia.email))
                        Error = "Favor de escribir el email del tutor.";
                    var original = Tutores.FirstOrDefault(x => x.nombre == TutorCopia.nombre);
                    if (string.IsNullOrWhiteSpace(Error) && original != null)
                    {
                        TutorDTO test = new TutorDTO()
                        {
                            Tutor = new Tutor()
                            {
                                nombre = TutorCopia.nombre,
                                email = TutorCopia.email,
                                celular = TutorCopia.celular,
                                direccion = TutorCopia.direccion,
                                telefono = TutorCopia.telefono,
                                id = original.id,
                                idusuario = original.idusuario
                            },
                            Curp = CurpSeleccionada
                        };
                        await EditarTutor(test);
                        TutorSelect = new Tutor();
                        TutorCopia = new Tutor();
                        NavegarTutor("ListaTutorView");
                        GetAllPadres();
                    }
                }
            }
            else
                Error = "Esto ni siquiera deberia de salir, revisar bindings x.x";
            Actualizar();
        }
        private async Task EditarTutor(TutorDTO test)
        {
            await padreservice.EditarTutor(test);
        }
        public async Task AgregarPadre(TutorDTO dto)
        {
            await padreservice.AgregarPadre(dto);
        }
        private async void Eliminar(object obj)
        {
            if (AlumnoSelect != null)
            {
                var enviar = new AlumnoDTO() { Alumno = AlumnoSelect };
                await alumnoservice.EliminarAlumno(enviar);
                ObtenerAlumnos();
            }
            else
                Error = "Favor de seleccionar el alumno a eliminar.";
            Actualizar();
        }
        private async void ObtenerAlumnos(int aja = 0)
        {
            Alumnos.Clear();
            Alumnos = await alumnoservice.GetAlumnos(new AlumnoDTO { Alumno = new Alumno { idGrupo = loginservice.Grupo.id } });
            if (aja != 0)
                Curps = Alumnos.Select(x => x.curp).ToList();
            Actualizar();
        }
        public string VistaActual { get; set; }
        private async void Guardar(object obj)
        {
            Error = "";
            if (AlumnoSelect != null)
            {
                if (VistaActual == "AgregarAlumnoView")
                {
                    if (string.IsNullOrWhiteSpace(AlumnoSelect.curp))
                        Error = "Favor de escribir la curp del alumno.";
                    if (string.IsNullOrWhiteSpace(AlumnoSelect.direccion))
                        Error = "Favor de escribir la dirección en la que reside el alumno.";
                    if (string.IsNullOrWhiteSpace(AlumnoSelect.matricula))
                        Error = "Favor de escribir la matricula del alumno.";
                    if (string.IsNullOrWhiteSpace(AlumnoSelect.nombre))
                        Error = "Favor de escribir el nombre del alumno.";
                    if (AlumnoSelect.edad < 3)
                        Error = "Favor de escribir la edad del alumno (la edad del alumno debe ser mayor a 3).";
                    if (AlumnoSelect.estatura < 0)
                        Error = "Favor de escribir la estatura del alumno.";
                    if (AlumnoSelect.peso < 0)
                        Error = "Favor de escribir el peso del alumno.";
                    var validar = Alumnos.FirstOrDefault(x => x.curp == AlumnoSelect.curp);
                    if (string.IsNullOrWhiteSpace(Error) && validar == null)
                    {
                        AlumnoDTO test = new AlumnoDTO()
                        {
                            Alumno = new Alumno()
                            {
                                alergico = AlumnoSelect.alergico,
                                curp = AlumnoSelect.curp,
                                direccion = AlumnoSelect.direccion,
                                edad = AlumnoSelect.edad,
                                estatura = AlumnoSelect.estatura,
                                matricula = AlumnoSelect.matricula,
                                nombre = AlumnoSelect.nombre,
                                peso = AlumnoSelect.peso,
                                fechaNacimiento = AlumnoSelect.fechaNacimiento,
                                idGrupo = loginservice.Grupo.id
                            }
                        };
                        await AgregarAlumno(test);
                        AlumnoSelect = new Alumno();
                        AlumnoCopia = new Alumno();
                        Navegar("ListaAlumnosView");
                        ObtenerAlumnos();
                    }
                    else
                        Error = "Esa alumno ya se encuentra registrado";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(AlumnoCopia.curp))
                        Error = "Favor de escribir la curp del alumno.";
                    if (string.IsNullOrWhiteSpace(AlumnoCopia.direccion))
                        Error = "Favor de escribir la dirección en la que reside el alumno.";
                    if (string.IsNullOrWhiteSpace(AlumnoCopia.matricula))
                        Error = "Favor de escribir la matricula del alumno.";
                    if (string.IsNullOrWhiteSpace(AlumnoCopia.nombre))
                        Error = "Favor de escribir el nombre del alumno.";
                    if (AlumnoCopia.edad < 3)
                        Error = "Favor de escribir la edad del alumno (la edad del alumno debe ser mayor a 3).";
                    if (AlumnoCopia.estatura < 0)
                        Error = "Favor de escribir la estatura del alumno.";
                    if (AlumnoCopia.peso < 0)
                        Error = "Favor de escribir el peso del alumno.";
                    var original = Alumnos.FirstOrDefault(x => x.curp == AlumnoCopia.curp);
                    if (string.IsNullOrWhiteSpace(Error) && original != null)
                    {
                        AlumnoDTO test = new AlumnoDTO()
                        {
                            Alumno = new Alumno()
                            {
                                alergico = AlumnoCopia.alergico,
                                curp = AlumnoCopia.curp,
                                direccion = AlumnoCopia.direccion,
                                edad = AlumnoCopia.edad,
                                estatura = AlumnoCopia.estatura,
                                matricula = AlumnoCopia.matricula,
                                nombre = AlumnoCopia.nombre,
                                peso = AlumnoCopia.peso,
                                fechaNacimiento = original.fechaNacimiento,
                                idGrupo = loginservice.Grupo.id,
                                id = original.id
                            }
                        };
                        await EditarAlumno(test);
                        AlumnoSelect = new Alumno();
                        AlumnoCopia = new Alumno();
                        Navegar("ListaAlumnosView");
                        ObtenerAlumnos();
                    }
                }
            }
            else
                Error = "Esto ni siquiera deberia de salir, revisar bindings x.x";
            Actualizar();
        }
        private async Task EditarAlumno(AlumnoDTO test)
        {
            await alumnoservice.EditarAlumno(test);
        }
        private async Task AgregarAlumno(AlumnoDTO test)
        {
            await alumnoservice.AgregarAlumno(test);
        }
        private async void NavegarTutor(string obj)
        {
            GetAllPadres();
            if (obj == "AgregarTutorView" || obj == "ListaTutorView")
            {
                await Shell.Current.GoToAsync("//" + obj);
            }
            else if (TutorSelect != null && !string.IsNullOrWhiteSpace(TutorSelect.nombre))
            {
                ObtenerAlumnos(1);
                TutorCopia = new Tutor()
                {
                    celular = TutorSelect.celular,
                    direccion = TutorSelect.direccion,
                    email = TutorSelect.email,
                    id = TutorSelect.id,
                    idusuario = TutorSelect.idusuario,
                    nombre = TutorSelect.nombre,
                    ocupacion = TutorSelect.ocupacion,
                    telefono = TutorSelect.telefono
                };
                await Shell.Current.GoToAsync("//" + obj);
            }
            else
                Error = "Seleccionar el tutor a editar.";
            VistaActual = obj;
            Actualizar("VistaActual");
            Actualizar("Error");
        }
        private async void Navegar(string obj)
        {
            if (obj == "AgregarAlumnoView" || obj == "ListaAlumnosView" || obj == "AgregarCalificacionView")
                await Shell.Current.GoToAsync("//" + obj);
            else if (obj == "EditarAlumnoView" && AlumnoSelect != null)
            {
                AlumnoCopia = new Alumno
                {
                    alergico = AlumnoSelect.alergico,
                    direccion = AlumnoSelect.direccion,
                    estatura = AlumnoSelect.estatura,
                    edad = AlumnoSelect.edad,
                    peso = AlumnoSelect.peso,
                    nombre = AlumnoSelect.nombre,
                    id = AlumnoSelect.id,
                    curp = AlumnoSelect.curp,
                    matricula = AlumnoSelect.matricula,
                };
                await Shell.Current.GoToAsync("//" + obj);
            }
            else if (obj == "CalificacionesView" && AlumnoSelect != null)
            {
                Materias = await materiaservice.ObtenerMaterias();
                Calificaciones = await materiaservice.ObtenerCalificaciones(new CalificacionDTO() { alumno = AlumnoSelect.id, asignatura = 1 });
                await Shell.Current.GoToAsync("//" + obj);
            }
            else if (obj == "")
            {
                await Shell.Current.GoToAsync("//" + obj);
            }
            else
                Error = "Favor de seleccionar el objeto a editar/eliminar.";
            VistaActual = obj;
            Actualizar();
        }
        private void Actualizar(string holawa = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(holawa));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}