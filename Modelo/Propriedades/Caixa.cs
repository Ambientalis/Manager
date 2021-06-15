using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Caixa, Modelo", Table = "caixas")]
    public partial class Caixa : ObjetoBase
    {
        public Caixa(int id) { this.Id = id; }
        public Caixa(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Caixa() { }

        #region ___________ Atributos ___________

        private String descricao;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "descricao")]
        public virtual String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        #endregion
    }
}
