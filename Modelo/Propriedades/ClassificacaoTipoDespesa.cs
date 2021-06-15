using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.ClassificacaoTipoDespesa, Modelo", Table = "classificacoes_tipos_despesas")]
    public partial class ClassificacaoTipoDespesa : ObjetoBase
    {
        public ClassificacaoTipoDespesa(int id)
        {
            this.Id = id;
            this.MultiEmpresa = false;
        }
        public ClassificacaoTipoDespesa(Object id)
        {
            this.Id = Convert.ToInt32("0" + id.ToString());
            this.MultiEmpresa = false;
        }
        public ClassificacaoTipoDespesa()
        {
            this.MultiEmpresa = false;
        }

        #region ___________ Atributos ___________

        private String nome;
        private IList<TipoDespesa> tiposDespesas;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "TiposDespesas", Table = "tipos_despesas")]
        [Cache(1, Region = "Longo", Usage = CacheUsage.NonStrictReadWrite)]
        [Key(2, Column = "id_classificacao")]
        [OneToMany(3, Class = "Modelo.TipoDespesa, Modelo")]
        public virtual IList<TipoDespesa> TiposDespesas
        {
            get { return tiposDespesas; }
            set { tiposDespesas = value; }
        }

        #endregion
    }
}
