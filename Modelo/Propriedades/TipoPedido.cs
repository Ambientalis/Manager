using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "tipos_pedidos", Name = "Modelo.TipoPedido, Modelo")]
    public partial class TipoPedido: ObjetoBase
    {
        public TipoPedido(int id) { this.Id = id; }
        public TipoPedido(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TipoPedido() { }

        #region ___________ Atributos ___________

        private string nome;
        private IList<Pedido> pedidos;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Pedidos", Table = "pedidos", Cascade = "delete")]
        [Key(2, Column = "id_tipo_pedido")]
        [OneToMany(3, Class = "Modelo.Pedido, Modelo")]
        public virtual IList<Pedido> Pedidos
        {
            get { return pedidos; }
            set { pedidos = value; }
        }

        #endregion
    }
}
