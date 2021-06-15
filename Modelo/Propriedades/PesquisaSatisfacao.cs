using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.PesquisaSatisfacao, Modelo", Table = "pesquisas_satisfacao")]
    public partial class PesquisaSatisfacao : ObjetoBase
    {
        public PesquisaSatisfacao(int id) { this.Id = id; }
        public PesquisaSatisfacao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public PesquisaSatisfacao() { }

        #region ___________ Atributos ___________

        private DateTime dataCriacao = DateTime.Now;
        private DateTime dataResposta = SqlDate.MinValue;
        private String nomeRespondente;
        private String telefoneRespondente;
        private String sugestoes;
        private IList<RespostaPesquisaSatisfacao> respostas = new List<RespostaPesquisaSatisfacao>();
        private OrdemServico ordemServico;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "data_criacao")]
        public virtual DateTime DataCriacao
        {
            get { return dataCriacao; }
            set { dataCriacao = value; }
        }

        [Property(Column = "data_resposta")]
        public virtual DateTime DataResposta
        {
            get { return dataResposta; }
            set { dataResposta = value; }
        }

        [Property(Column = "nome_respondente")]
        public virtual String NomeRespondente
        {
            get { return nomeRespondente; }
            set { nomeRespondente = value; }
        }

        [Property(Column = "telefone_respondente")]
        public virtual String TelefoneRespondente
        {
            get { return telefoneRespondente; }
            set { telefoneRespondente = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "sugestoes", SqlType = "text")]
        public virtual String Sugestoes
        {
            get { return sugestoes; }
            set { sugestoes = value; }
        }

        [Bag(Name = "Respostas", Table = "respostas_pesquisa_satisfacao")]
        [Key(2, Column = "id_pesquisa_satisfacao")]
        [OneToMany(3, Class = "Modelo.RespostaPesquisaSatisfacao, Modelo")]
        public virtual IList<RespostaPesquisaSatisfacao> Respostas
        {
            get { return respostas; }
            set { respostas = value; }
        }

        [ManyToOne(Class = "Modelo.OrdemServico, Modelo", Name = "OrdemServico", Column = "id_ordem_servico")]
        public virtual OrdemServico OrdemServico
        {
            get { return ordemServico; }
            set { ordemServico = value; }
        }

        #endregion
    }
}
