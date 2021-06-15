using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.TipoOrdemServico, Modelo", Table = "tipos_ordem_servico")]
    public partial class TipoOrdemServico : ObjetoBase
    {
        public TipoOrdemServico(int id)
        {
            this.MultiEmpresa = false;
            this.Id = id;
        }
        public TipoOrdemServico(Object id)
        {
            this.MultiEmpresa = false;
            this.Id = Convert.ToInt32("0" + id.ToString());
        }
        public TipoOrdemServico()
        {
            this.MultiEmpresa = false;
        }

        #region ___________ Atributos ___________

        private string nome;
        private int prazoPadrao;
        private IList<OrdemServico> ordensServico;

        #endregion

        #region ___________ Propriedades ___________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "prazo_padrao")]
        public virtual int PrazoPadrao
        {
            get { return prazoPadrao; }
            set { prazoPadrao = value; }
        }

        [Bag(Name = "OrdensServico", Table = "ordens_servico")]
        [Key(2, Column = "id_tipo_ordem_servico")]
        [OneToMany(3, Class = "Modelo.OrdemServico, Modelo")]
        public virtual IList<OrdemServico> OrdensServico
        {
            get { return ordensServico; }
            set { ordensServico = value; }
        }

        #endregion
    }
}
