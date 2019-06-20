namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
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
        [Required(ErrorMessage = "{0} es Requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Nacimiento")]
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

        //public void Guardar()
        //{
        //    try
        //    {
        //        using (var context = new BaseDeDatosContext())
        //        {
        //            if (this.Alumno_Id == 0)
        //            {
        //                context.Entry(this).State = EntityState.Added;
        //                foreach (var c in this.Cursos)
        //                {
        //                    context.Entry(c).State = EntityState.Unchanged;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}
        public void Guardar()
        {
            try
            {
                using (var context = new BaseDeDatosContext())
                {
                    if (this.Alumno_Id == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                    }
                    else
                    {
                        context.Database.ExecuteSqlCommand(
                            "DELETE FROM AlumnosCursos WHERE Alumno_id = @Alumno_id",
                            new SqlParameter("Alumno_Id", this.Alumno_Id)
                        );

                        var cursoBK = this.Cursos;

                        this.Cursos = null;
                        context.Entry(this).State = EntityState.Modified;
                        this.Cursos = cursoBK;
                    }

                    foreach (var c in this.Cursos)
                        context.Entry(c).State = EntityState.Unchanged;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
