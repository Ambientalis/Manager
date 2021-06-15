using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Table = "textos_padroes", Name = "Modelo.TextoPadrao, Modelo")]
    public partial class TextoPadrao : ObjetoBase
    {
        public const String MODELOEMAILCONTRATO = "Modelo de e-mail de contrato";
        public const String MODELOEMAILORCAMENTO = "Modelo de e-mail de orçamento";
        public const String MODELOCONTRATO = "Modelo de contrato";
        public const String MODELOPESQUISASATISFACAO = "Modelo de e-mail de pesquisa de satisfação";
        public const String ModeloObservacoesOrcamento = "Observação padrão de orçamentos";

        #region ________ Construtores ________

        public TextoPadrao(int id) { this.Id = id; }
        public TextoPadrao(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TextoPadrao() { }

        #endregion

        #region ___________Atributos_____________

        private String assunto;
        private String texto;
        private String tipo = TextoPadrao.MODELOCONTRATO;

        #endregion

        #region ___________Propriedades_____________

        [Property(Column = "assunto")]
        public virtual String Assunto
        {
            get { return assunto; }
            set { assunto = value; }
        }

        [Property(Type = "StringClob")]
        [Column(1, Name = "texto", SqlType = "text")]
        public virtual String Texto
        {
            get { return texto; }
            set { texto = value; }
        }

        [Property(Column = "tipo")]
        public virtual String Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        #endregion
    }
}
