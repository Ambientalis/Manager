using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.SolicitacaoAprovacao, Modelo", Table = "solicitacoes_aprovacoes")]
    public partial class SolicitacaoAprovacao: ObjetoBase
    {
        public SolicitacaoAprovacao(int id) { this.Id = id; }
        public SolicitacaoAprovacao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public SolicitacaoAprovacao() { }

        #region ___________ Atributos ___________

        private DateTime data;
        private string justificativa;
        private string solicitante;
        private OrdemServico ordemServico;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "justificativa", SqlType = "text")]
        public virtual string Justificativa
        {
            get { return justificativa; }
            set { justificativa = value; }
        }

        [Property(Column = "solicitante")]
        public virtual string Solicitante
        {
            get { return solicitante; }
            set { solicitante = value; }
        }

        [ManyToOne(Name = "OrdemServico", Class = "Modelo.OrdemServico, Modelo", Column = "id_ordem_servico")]
        public virtual OrdemServico OrdemServico
        {
            get { return ordemServico; }
            set { ordemServico = value; }
        }

        #endregion
    }
}
