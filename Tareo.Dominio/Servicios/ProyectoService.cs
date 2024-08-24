using Core.Dominio;
using Tareo.Dominio.Entidades;
using Tareo.Dominio.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tareo.Dominio.Servicios
{
    public class ProyectoService : ServiceAbstract<Item>, IProyectoService
    {
        public ProyectoService(IRepository<Item> repository) : base(repository)
        {
        }

        public IEnumerable<Item> servicioPrueba(Item filtro)
        {
            throw new NotImplementedException();
        }
    }
}
