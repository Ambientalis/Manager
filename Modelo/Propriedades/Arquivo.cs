using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Arquivo, Modelo", Table = "arquivos")]
    public partial class Arquivo : ObjetoBase
    {
        #region ________ Construtores ________

        public Arquivo(int id) { this.Id = id; }
        public Arquivo(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Arquivo() { }

        #endregion

        #region ________ Atributos ________

        private string caminho;
        private string host;
        private string nome;        
        
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

        #endregion

        public virtual string UrlArquivo
        {
            get { return ("http://" + this.Host + "/" + this.Caminho); }
        }
    }
}
