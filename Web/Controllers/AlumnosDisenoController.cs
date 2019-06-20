using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AlumnosDisenoController : Controller
    {
        private BaseDeDatosContext db = new BaseDeDatosContext();
        private Alumnos alumno = new Alumnos();
        private Cursos curso = new Cursos();
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }

        public ActionResult Ver(int id)
        {
            var detalle = alumno.Obtener(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        public ActionResult Crud(int id = 0)
        {
            ViewBag.Ciudad_Id = new SelectList(db.Ciudades, "Ciudad_Id", "NombreCiudad", alumno.Ciudad_Id);
            ViewBag.ListaCursos = curso.ListarCursos();
            return View(
                id > 0 ? alumno.Obtener(id)
                       : alumno
            );
        }

        public ActionResult Guardar(Alumnos modeloAlumno, int[] cursos = null)
        {
            if (cursos != null)
            {
                foreach (var c in cursos)
                {
                    modeloAlumno.Cursos.Add(new Cursos { Curso_Id = c });
                }
            }
            modeloAlumno.Guardar();

            return Redirect("~/AlumnosDiseno/Crud/" + modeloAlumno.Alumno_Id);
        }

    }
}