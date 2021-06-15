using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.RespostaPesquisaSatisfacao, Modelo", Table = "respostas_pesquisa_satisfacao")]
    public partial class RespostaPesquisaSatisfacao : ObjetoBase
    {
        public RespostaPesquisaSatisfacao(int id) { this.Id = id; }
        public RespostaPesquisaSatisfacao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public RespostaPesquisaSatisfacao() { }

        #region ___________ Atributos ___________

        private int resposta = 1;
        private PerguntaPesquisaSatisfacao pergunta;
        private PesquisaSatisfacao pesquisaSatisfacao;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "resposta")]
        public virtual int Resposta
        {
            get { return resposta; }
            set { resposta = value; }
        }

        [ManyToOne(Class = "Modelo.PerguntaPesquisaSatisfacao, Modelo", Name = "Pergunta", Column = "id_pergunta")]
        public virtual PerguntaPesquisaSatisfacao Pergunta
        {
            get { return pergunta; }
            set { pergunta = value; }
        }

        [ManyToOne(Class = "Modelo.PesquisaSatisfacao, Modelo", Name = "PesquisaSatisfacao", Column = "id_pesquisa_satisfacao")]
        public virtual PesquisaSatisfacao PesquisaSatisfacao
        {
            get { return pesquisaSatisfacao; }
            set { pesquisaSatisfacao = value; }
        }

        #endregion
    }
}
