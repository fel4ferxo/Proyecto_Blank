using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorIncidencia.Aplicacion.Dto
{
    public class ProcesoDto
    {
        #region Atributos

        public string Id { get; set; }
        public string idUsuarioFlujo { get; set; }
        public string idUsuarioFlujoDestino { get; set; }
        public string idTicket { get; set; }
        public string idAristaFlujo { get; set; }
        public string comentario { get; set; }

        public string RowVersion { get; set; }

        #endregion
    }
}
