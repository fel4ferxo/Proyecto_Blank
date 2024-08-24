using Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareo.Dominio.Entidades.SCode.EntidadesReporte
{
    public class FilterListaEntidad : FilterReporte
    {
        #region Atributos

        public string tipo { get; set; }
        public string valor { get; set; }
        public string tabla { get; set; }
        public string consulta { get; set; }
        public int p_inicio { get; set; }
        public int p_intervalo { get; set; }

        #endregion
    }
}
