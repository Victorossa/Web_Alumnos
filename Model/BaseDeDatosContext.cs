namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BaseDeDatosContext : DbContext
    {
        public BaseDeDatosContext()
            : base("name=BaseDeDatosContext")
        {
        }

        public virtual DbSet<Alumnos> Alumnos { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Cursos> Cursos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Para Cuando hay tablas de Relacion
            modelBuilder.Entity<Alumnos>()
                .HasMany(e => e.Cursos)
                .WithMany(e => e.Alumnos)
                .Map(m => m.ToTable("AlumnosCursos").MapLeftKey("Alumno_Id").MapRightKey("Curso_Id"));
            //Para cuando se tenga una tabla con un id de seleccion de otra tabla
            modelBuilder.Entity<Ciudades>()
                .Property(e => e.NombreCiudad)
                .IsUnicode(false);
        }
    }
}
