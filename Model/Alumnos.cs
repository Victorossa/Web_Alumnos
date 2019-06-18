namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Alumnos
    {
        private BaseDeDatosContext db = new BaseDeDatosContext();

   
        public Alumnos()
        {
            Cursos = new List<Cursos>();
        }

        [Key]
        public int Alumno_Id { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellido1 { get; set; }

        [Required]
        public string Apellido2 { get; set; }

        public int Edad { get; set; }

        [StringLength(200)]
        public string Correo { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int? Ciudad_Id { get; set; }

        public virtual Ciudades Ciudades { get; set; }

   
        public virtual ICollection<Cursos> Cursos { get; set; }

        public List<Alumnos> Listar()
        {
            var alumnos = new List<Alumnos>();
            try
            {
                using (var context = new BaseDeDatosContext { })
                {
                    alumnos = context.Alumnos.ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return alumnos;
        }

        //public Alumnos Obtener(int id)
        //{
        //    var alumno = new Alumnos();
        //    try
        //    {
        //        using (var context = new BaseDeDatosContext())
        //        {
        //            alumno = context.Alumnos
        //                            .Include("Ciudades")
        //                            .Where(x => x.Alumno_Id == id)
        //                            .Single();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //    return alumno;
        //}

        public Alumnos Obtener(int id)
        {
            var alumno = new Alumnos();
            try
            {
                using (var context = new BaseDeDatosContext())
                {
                    alumno = context.Alumnos
                                    .Include("Ciudades").Include("Cursos")
                                    .Where(x => x.Alumno_Id == id)
                                    .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return alumno;
        }

    }
}
