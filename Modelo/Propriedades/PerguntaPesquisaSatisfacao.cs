using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.PerguntaPesquisaSatisfacao, Modelo", Table = "perguntas_pesquisa_satisfacao")]
    public partial class PerguntaPesquisaSatisfacao : ObjetoBase
    {
        public PerguntaPesquisaSatisfacao(int id) { this.Id = id; }
        public PerguntaPesquisaSatisfacao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public PerguntaPesquisaSatisfacao() { }

        #region ___________ Atributos ___________

        private int prioridade;
        private String pergunta;
        private IList<RespostaPesquisaSatisfacao> respostas = new List<RespostaPesquisaSatisfacao>();

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "prioridade")]
        public virtual int Prioridade
        {
            get { return prioridade; }
            set { prioridade = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "pergunta", SqlType = "text")]
        public virtual String Pergunta
        {
            get { return pergunta; }
            set { pergunta = value; }
        }

        [Bag(Name = "Respostas", Table = "respostas_pesquisa_satisfacao")]
        [Key(2, Column = "id_pergunta")]
        [OneToMany(3, Class = "Modelo.RespostaPesquisaSatisfacao, Modelo")]
        public virtual IList<RespostaPesquisaSatisfacao> Respostas
        {
            get { return respostas; }
            set { respostas = value; }
        }

        #endregion
    }
}
