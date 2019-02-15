using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework2.Models
{
    public class FarmaModel
    {
        public string Nombre { get; set; }
        public int Codigo { get; set; }
        public double HorasTrabajadas { get; set; }
        public bool Disponible { get; set; }
        public double Sueldo { get; set; }
        public double Citas { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set;  }
        public static List<FarmaModel> empleados { get; set; }
        public static Stack<FarmaModel> empleadosstack { get; set; }
        public static Queue<FarmaModel> empleadosqueue { get; set; }
    }
}
