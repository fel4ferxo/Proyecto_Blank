namespace Tareo.Aplicacion.Dto
{
    public class AbstractDto
    {
        #region Atributos

        public string Id { get; set; }
        public long FECHA_CREACION { get; set; }
        public string RowVersion { get; set; }

        #endregion
    }
}