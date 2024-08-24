
namespace Tareo.Dominio.Entidades
{
    using Core.Dominio.Entidades;
    using System;
    using System.Collections.Generic;

    public class Item : Entity
    {
        #region Atributos

       
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public byte[] RowVersion { get; set; }

        #endregion
    }
}
