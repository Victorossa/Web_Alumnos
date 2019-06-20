namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Cursos
    {
        
        public Cursos()
        {
            Alumnos = new HashSet<Alumnos>();
        }

        [Key]
        public int Curso_Id { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreCurso { get; set; }

        
        public virtual ICollection<Alumnos> Alumnos { get; set; }

        public List<Cursos> ListarCursos()
        {
            var cursos = new List<Cursos>();
            try
            {
                using (var context = new BaseDeDatosContext { })
                {
                    cursos = context.Cursos.ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return cursos;
        }

        
    }
}
