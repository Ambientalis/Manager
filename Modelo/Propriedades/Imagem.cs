using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Imagem, Modelo", Table = "imagens")]
    public partial class Imagem : ObjetoBase
    {
        #region ________ Construtores ________

        public Imagem(int id) { this.Id = id; }
        public Imagem(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Imagem() { }

        #endregion

        #region ________ Atributos ________

        private string caminho;
        private string host;
        private string nome;        
        private Pessoa pessoa;

        #endregion

        #region ________ Propriedades ________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "host")]
        public virtual string Host
        {
            get { return host; }
            set { host = value; }
        }

        [Property(Column = "caminho")]
        public virtual string Caminho
        {
            get { return caminho; }
            set { caminho = value; }
        }        

        [OneToOne(PropertyRef = "Imagem", Class = "Modelo.Pessoa, Modelo")]
        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }

        #endregion
    }
}
