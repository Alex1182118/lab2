using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//Para tener acceso a las variables declaradas el en Model Farma
using Homework2.Models;

namespace Homework2.Controllers
{
    public class FarmaController : Controller
    {
        //Es public static para que si se modifica en algun metodo se guarde

        public static List<FarmaModel>  empleados = new List<FarmaModel>();
        public static Stack<FarmaModel> empleadosstack = new Stack<FarmaModel>();
        public static Queue<FarmaModel> empleadosqueue = new Queue<FarmaModel>();

        public IActionResult Index()
        {
            return View();
        }
        //Muestra donde ingresa los datos
        public ActionResult CrearEmpleado()
        {
            return View();  
        }
        //Guarda info del empleado
        [HttpPost]
        public ActionResult GuardarInfoEmpleado(string Nombre, int Codigo, DateTime Hora)
        {
            FarmaModel empleadoNuevo = new FarmaModel();
            try
            {
                empleadoNuevo.Nombre= Nombre;
                empleadoNuevo.Codigo = Codigo;
                empleadoNuevo.Disponible = true;
                //numero aleatorio de citas a realizar entre 1 y 4 
                Random r = new Random();
                empleadoNuevo.Citas = r.Next(0, 4);
                //Calculo las horas en la oficina mas las de visita
                empleadoNuevo.HorasTrabajadas = (3 + (1.5*empleadoNuevo.Citas));
                empleadoNuevo.Sueldo = 0.0;
                if (Hora == null)
                {
                    empleadoNuevo.HoraEntrada = DateTime.Now;
                }
                //Hora a la que entra a la oficina a agendar citas
                empleadoNuevo.HoraEntrada = Hora;
                empleadoNuevo.HoraSalida = Hora;
                empleadoNuevo.HoraSalida.AddHours(3 + (1.5 * empleadoNuevo.Citas));
                empleados.Add(empleadoNuevo);
                empleadosstack.Push(new FarmaModel() { Nombre = Nombre, Codigo= Codigo, Disponible = false, HorasTrabajadas= (3 + (1.5 * empleadoNuevo.Citas)), Sueldo=0.0, Citas = r.Next(0,4), HoraEntrada = Hora , HoraSalida = empleadoNuevo.HoraSalida});

                return RedirectToAction("EmpleadoGuardado", empleadoNuevo);
            }
            catch (Exception)
            {
                return View("CrearEmpleado", empleadoNuevo);
            }
        }
        //Envia los datos del empleado que acabas de guardar y lo muestra
        public ActionResult EmpleadoGuardado(FarmaModel guardado)
        {
            return View(guardado);
        }
        public ActionResult MostrarEmpleados()
        {
            //Cuenta cuantos empleados se han registrado
            return View(empleados);
        }
        public ActionResult BuscarEmpleado()
        {
            return View();
        }
        [HttpPost]//Valida si esta registrado
        public ActionResult EncontrarEmpleado(string Nombre, int Codigo)
        {
            //FALTA MOSTRAR DATOS CUANDO SE ENCUENTRA.
            FarmaModel empleado1 = new FarmaModel();
            if (Nombre != "")
            {
                empleado1 = empleados.Find(x => x.Nombre == Nombre);
                return View(empleado1);
            }
            else 
            if (Codigo != 0)
            {
                empleado1 = empleados.Find(x => x.Codigo == Codigo);
                return View(empleado1);
            }
            else
            {
                return View();

            }





        }
        public ActionResult EntrarACola()
        {
            int totalempleados = empleadosstack.Count();
            for (int i = 0;  i < totalempleados;  i++)
            {
                empleadosqueue.Enqueue(empleadosstack.Pop()) ;
            }
            //empleadosqueue.OrderBy(FarmaModel => FarmaModel.HorasTrabajadas);
            string nombres=""; string codigo = "";
            for (int i = 0; i < totalempleados; i++)
            {
            nombres += empleadosqueue.Peek().Nombre;
                codigo += empleadosqueue.Peek().Codigo;
            }
            return View(empleadosqueue.ToList());
        }
        public ActionResult SacarEmpleado()
        {
            //Saca al ultimo en entrar al parqueo
            return View(empleadosstack.Pop());
        }
        //Aqui mando todo el listado en un viewbag 
        }
}

