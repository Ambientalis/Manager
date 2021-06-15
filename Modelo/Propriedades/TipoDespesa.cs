using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.TipoDespesa, Modelo", Table = "tipos_despesas")]
    public partial class TipoDespesa : ObjetoBase
    {
        public TipoDespesa(int id) { this.Id = id; }
        public TipoDespesa(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TipoDespesa() { }

        #region ___________ Atributos ___________

        private string nome;
        private bool preAprovada;
        private ClassificacaoTipoDespesa classificacao;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "pre_aprovada", Type = "TrueFalse")]
        public virtual bool PreAprovada
        {
            get { return preAprovada; }
            set { preAprovada = value; }
        }

        [ManyToOne(Name = "Classificacao", Column = "id_classificacao", Class = "Modelo.ClassificacaoTipoDespesa, Modelo")]
        public virtual ClassificacaoTipoDespesa Classificacao
        {
            get { return classificacao; }
            set { classificacao = value; }
        }

        #endregion
    }
}
