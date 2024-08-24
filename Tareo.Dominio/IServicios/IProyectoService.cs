using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareo.Dominio.Entidades;

namespace Tareo.Dominio.IServicios
{
    public interface IProyectoService : IService<Item>
    {
        IEnumerable<Item> servicioPrueba(Item filtro);
    }
}
