using Core.Dominio;
using Core.Dominio.Entidades;
using Core.Persistencia;
using Tareo.Persistencia.UnitOfWork;

namespace Tareo.Persistencia.Repositorio
{
    public class Reporte_Repository<TEntity> : RepositoryReporte<TEntity>, IRepositoryReporte<TEntity>
        where TEntity : EntityReporte
    {
        public Reporte_Repository(Tareo_UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
