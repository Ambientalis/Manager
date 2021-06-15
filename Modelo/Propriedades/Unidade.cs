using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Unidade, Modelo", Table = "unidades")]
    public partial class Unidade : ObjetoBase
    {
        public Unidade(int id)
        {
            this.MultiEmpresa = false;
            this.Id = id;
        }
        public Unidade(Object id)
        {
            this.MultiEmpresa = false;
            this.Id = Convert.ToInt32("0" + id.ToString());
        }
        public Unidade()
        {
            this.MultiEmpresa = false;
        }

        #region ___________ Atributos ___________

        private string codigo;
        private string descricao;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "codigo")]
        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        [Property(Column = "descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        #endregion
    }
}
