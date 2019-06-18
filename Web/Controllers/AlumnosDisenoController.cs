using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AlumnosDisenoController : Controller
    {
        private BaseDeDatosContext db = new BaseDeDatosContext();
        private Alumnos alumno = new Alumnos();
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }
    }
}