using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.SolicitacaoAdiamento, Modelo", Table = "solicitacoes_adiamentos")]
    public partial class SolicitacaoAdiamento: ObjetoBase
    {
        public SolicitacaoAdiamento(int id) { this.Id = id; }
        public SolicitacaoAdiamento(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public SolicitacaoAdiamento() { }

        #region ___________ Atributos ___________

        public const char ACEITA = 'A';
        public const char NEGADA = 'N';

        private DateTime data;
        private string solicitante;
        private string justificativa;
        private char parecer;

        private DateTime prazoPadraoAnterior;
        private DateTime prazoLegalAnterior;
        private DateTime prazoDiretoriaAnterior;          

        private DateTime prazoPadraoNovo;
        private DateTime prazoLegalNovo;
        private DateTime prazoDiretoriaNovo;        

        private DateTime dataResposta;        
        private string usuarioAdiou;
        private string observacoes;
        private OrdemServico ordemServico;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [Property(Column = "solicitante")]
        public virtual string Solicitante
        {
            get { return solicitante; }
            set { solicitante = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "justificativa", SqlType = "text")]
        public virtual string Justificativa
        {
            get { return justificativa; }
            set { justificativa = value; }
        }

        [Property(Column = "parecer")]
        public virtual char Parecer
        {
            get { return parecer; }
            set { parecer = value; }
        }

        [Property(Column = "prazo_padrao_anterior")]
        public virtual DateTime PrazoPadraoAnterior
        {
            get 
            {
                if (prazoPadraoAnterior == null || prazoPadraoAnterior <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoPadraoAnterior;                
            }            
            set { prazoPadraoAnterior = value; }
        }

        [Property(Column = "prazo_legal_anterior")]
        public virtual DateTime PrazoLegalAnterior
        {
            get 
            {
                if (prazoLegalAnterior == null || prazoLegalAnterior <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoLegalAnterior;                
            }            
            set { prazoLegalAnterior = value; }
        }

        [Property(Column = "prazo_diretor_anterior")]
        public virtual DateTime PrazoDiretoriaAnterior
        {
            get 
            {
                if (prazoDiretoriaAnterior == null || prazoDiretoriaAnterior <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoDiretoriaAnterior;                
            }            
            set { prazoDiretoriaAnterior = value; }
        }

        [Property(Column = "prazo_padrao_novo")]
        public virtual DateTime PrazoPadraoNovo
        {
            get 
            {
                if (prazoPadraoNovo == null || prazoPadraoNovo <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoPadraoNovo;                
            }            
            set { prazoPadraoNovo = value; }
        }

        [Property(Column = "prazo_legal_novo")]
        public virtual DateTime PrazoLegalNovo
        {
            get 
            {
                if (prazoLegalNovo == null || prazoLegalNovo <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoLegalNovo;                
            }            
            set { prazoLegalNovo = value; }
        }

        [Property(Column = "prazo_diretoria_novo")]
        public virtual DateTime PrazoDiretoriaNovo
        {
            get 
            {
                if (prazoDiretoriaNovo == null || prazoDiretoriaNovo <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return prazoDiretoriaNovo;                
            }            
            set { prazoDiretoriaNovo = value; }
        }

        [Property(Column = "usuario_adiou")]
        public virtual string UsuarioAdiou
        {
            get { return usuarioAdiou; }
            set { usuarioAdiou = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "observacoes", SqlType = "text")]
        public virtual string Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }

        [ManyToOne(Name = "OrdemServico", Class = "Modelo.OrdemServico, Modelo", Column = "id_ordem_servico")]
        public virtual OrdemServico OrdemServico
        {
            get { return ordemServico; }
            set { ordemServico = value; }
        }        

        [Property(Column = "data_resposta")]
        public virtual DateTime DataResposta
        {
            get
            {
                if (dataResposta == null || dataResposta <= SqlDateTime.MinValue.Value)
                    return SqlDate.MinValue;
                else
                    return dataResposta;
            }
            set { dataResposta = value; }
        }

        #endregion
    }
}
