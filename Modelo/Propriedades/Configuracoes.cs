using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Configuracoes, Modelo", Table = "configuracoes")]
    public partial class Configuracoes : ObjetoBase
    {
        public Configuracoes(int id)
        {
            this.Id = id;
            this.MultiEmpresa = false;
        }
        public Configuracoes(Object id)
        {
            this.Id = Convert.ToInt32("0" + id.ToString());
            this.MultiEmpresa = false;
        }
        public Configuracoes()
        {
            this.MultiEmpresa = false;
        }

        #region ___________ Atributos ___________

        private string emailsAvisoContratacaoAceita;
        private string emailsAvisoOrcamentoAceito;
        private string emailsAvisoPesquisaSatisfacaoRespondida;
        private string emailsSolicitacaoLiberacaoDespesa;
        private string corPadrao;
        private string linkLogomarca;
        private string contatoEmpresa;
        private string favIcon;

        private String caminhoSistemaManager;
        private String caminhoSistemaFinanceiro;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "contato_empresa")]
        public virtual string ContatoEmpresa
        {
            get { return contatoEmpresa; }
            set { contatoEmpresa = value; }
        }

        [Property(Column = "emails_aviso_contratacao_aceita")]
        public virtual string EmailsAvisoContratacaoAceita
        {
            get { return emailsAvisoContratacaoAceita; }
            set { emailsAvisoContratacaoAceita = value; }
        }

        [Property(Column = "emails_aviso_orcamento_aceito")]
        public virtual string EmailsAvisoOrcamentoAceito
        {
            get { return emailsAvisoOrcamentoAceito; }
            set { emailsAvisoOrcamentoAceito = value; }
        }

        [Property(Column = "emails_aviso_pesquisa_satisfacao_respondida")]
        public virtual string EmailsAvisoPesquisaSatisfacaoRespondida
        {
            get { return emailsAvisoPesquisaSatisfacaoRespondida; }
            set { emailsAvisoPesquisaSatisfacaoRespondida = value; }
        }

        [Property(Column = "emails_solicitacao_liberacao_despesa")]
        public virtual string EmailsSolicitacaoLiberacaoDespesa
        {
            get { return emailsSolicitacaoLiberacaoDespesa; }
            set { emailsSolicitacaoLiberacaoDespesa = value; }
        }

        [Property(Column = "caminho_sistema_manager")]
        public virtual String CaminhoSistemaManager
        {
            get { return caminhoSistemaManager; }
            set { caminhoSistemaManager = value; }
        }

        [Property(Column = "caminho_sistema_financeiro")]
        public virtual String CaminhoSistemaFinanceiro
        {
            get { return caminhoSistemaFinanceiro; }
            set { caminhoSistemaFinanceiro = value; }
        }

        [Property(Column = "cor_padrao")]
        public virtual string CorPadrao
        {
            get { return corPadrao; }
            set { corPadrao = value; }
        }

        [Property(Column = "link_logomarca")]
        public virtual string LinkLogomarca
        {
            get { return linkLogomarca; }
            set { linkLogomarca = value; }
        }

        [Property(Column = "fav_icon")]
        public virtual string FavIcon
        {
            get { return favIcon; }
            set { favIcon = value; }
        }

        #endregion
    }
}
