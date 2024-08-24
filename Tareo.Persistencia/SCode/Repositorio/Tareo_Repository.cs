using Core.Dominio;
using Core.Dominio.Entidades;
using Core.Persistencia;
using Tareo.Persistencia.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareo.Persistencia.Repositorio
{
    public class Tareo_Repository<TEntity> : Repository<TEntity>, IRepository<TEntity>
        where TEntity : Entity
    {
        public Tareo_Repository(Tareo_UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
