using Tareo.Dominio.Entidades;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace Tareo.Persistencia.Mapeadores
{
    public class Item_TypeConf : EntityTypeConfiguration<Item>
    {
        public Item_TypeConf()
        {
            //key and properties
            ToTable("Item");
            this.HasKey(ba => ba.Id);
            this.Property(ba => ba.RowVersion);
        }
    }
}
