
namespace Tareo.Persistencia.UnitOfWork
{
    using Core.Persistencia.UnidadDeTrabajo;
    using Tareo.Dominio.Entidades;
    using Tareo.Persistencia.Mapeadores;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;


    public class Tareo_UnitOfWork : UnitOfWork
    {
        public Tareo_UnitOfWork() : base("Tareo_UnitOfWork")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuraciones generales
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Mapeadores
            modelBuilder.Configurations.Add(new Item_TypeConf());


            /**
             * Hay un detalle en el funcionamiento de los RowVersion, cuando se actualiza un registro no se cambia al siguiente
             * Rowversion del registro, si no se actualiza al siguiente Rowversion de todos los registros
             * */
            modelBuilder.Entity<Item>().Property(g => g.RowVersion).IsRowVersion().IsConcurrencyToken(true);
            
            // identity
            //modelBuilder.Entity<Incidencia>().Property(x => x.nro).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
