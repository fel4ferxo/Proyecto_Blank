
namespace Core.Dominio.Entidades
{
    using Core.Dominio.Util;
    using System;

    /// <summary>
    /// Clase Base para las entidades
    /// </summary>
    public class Entity
    {
        public Entity() { }
        #region Members

        int? _requestedHashCode;
        Guid _Id;
        public virtual bool ESTADO { get; set; }
        public virtual bool DISPONIBILIDAD { get; set; }

        public virtual long FECHA_CREACION { get; set; }
        public virtual long FECHA_MODIFICACION { get; set; }

        public virtual string USER_CREACION { get; set; }
        public virtual string USER_MODIFICACION { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Get the persisten object identifier
        /// </summary>
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// INICIALIZA LAS VARIABLES DE AUDITORIA PARA UNA NUEVA
        /// ENTIDAD
        /// </summary>
        public void AsNew(string userCreacion)
        {
            ESTADO = true;
            DISPONIBILIDAD = true;
            FECHA_CREACION = Fecha.getFechaActualMilliseconds();
            FECHA_MODIFICACION = Fecha.getFechaActualMilliseconds();
            USER_CREACION = userCreacion;
            USER_MODIFICACION = userCreacion;
        }

        public void AsUpdate(string userModificacion)
        {
            ESTADO = true;
            DISPONIBILIDAD = true;
            FECHA_MODIFICACION = Fecha.getFechaActualMilliseconds();
            USER_MODIFICACION = userModificacion;
        }

        public void AsDelete(string userEliminacion)
        {
            ESTADO = false;
            DISPONIBILIDAD = false;
            FECHA_MODIFICACION = Fecha.getFechaActualMilliseconds();
            USER_MODIFICACION = userEliminacion;
        }
        public void AsDeleteModicado(string userEliminacion)
        {
            ESTADO = false;
            DISPONIBILIDAD = true;
            FECHA_MODIFICACION = Fecha.getFechaActualMilliseconds();
            USER_MODIFICACION = userEliminacion;
        }

        /// <summary>
        /// Check if this entity is transient, ie, without identity at this moment
        /// </summary>
        /// <returns>True if entity is transient, else false</returns>
        public bool IsTransient()
        {
            return this.Id == Guid.Empty;
        }

        /// <summary>
        /// Generate identity for this entity
        /// </summary>
        public void GenerateNewIdentity()
        {
            if (IsTransient())
                this.Id = IdentityGenerator.NewSequentialGuid();
        }

        public void GenerateNewIdentityForce()
        {
            this.Id = IdentityGenerator.NewSequentialGuid();
        }

        /// <summary>
        /// Change current identity for a new non transient identity
        /// </summary>
        /// <param name="identity">the new identity</param>
        public void ChangeCurrentIdentity(Guid identity)
        {
            if (identity != Guid.Empty)
                this.Id = identity;
        }

        #endregion

        #region Overrides Methods

        /// <summary>
        /// <see cref="M:System.Object.Equals"/>
        /// </summary>
        /// <param name="obj"><see cref="M:System.Object.Equals"/></param>
        /// <returns><see cref="M:System.Object.Equals"/></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        /// <summary>
        /// <see cref="M:System.Object.GetHashCode"/>
        /// </summary>
        /// <returns><see cref="M:System.Object.GetHashCode"/></returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        #endregion
    }
}
