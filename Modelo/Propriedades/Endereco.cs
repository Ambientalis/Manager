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
    [Class(Name = "Modelo.Endereco, Modelo", Table = "enderecos")]
    public partial class Endereco : ObjetoBase
    {
        #region ________ Construtores ________

        public Endereco(int id) { this.Id = id; }
        public Endereco(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Endereco() { }

        #endregion

        #region ___________Atributos_____________
                
        private string descricao;
        private string logradouro;
        private string numero;
        private string bairro;
        private string referencia;
        private string complemento;        
        private string cep;        
        private Cidade cidade;
        private Pessoa pessoa;        

        #endregion

        #region ___________Propriedades_____________
        
        [Property(Column = "descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Column = "logradouro")]
        public virtual string Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }

        [Property(Length = 8, Column = "numero")]
        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [Property(Column = "bairro")]
        public virtual string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        [Property(Column = "referencia")]
        public virtual string Referencia
        {
            get { return referencia; }
            set { referencia = value; }
        }

        [Property(Length = 100, Column = "complemento")]
        public virtual string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }        

        [Property(Length = 10, Column = "cep")]
        public virtual string Cep
        {
            get { return cep; }
            set { cep = value; }
        }        

        [ManyToOne(Name = "Cidade", Column = "id_cidade", Class = "Modelo.Cidade, Modelo")]
        public virtual Cidade Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        [ManyToOne(Name = "Pessoa", Column = "id_pessoa", Class = "Modelo.Pessoa, Modelo")]
        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }        

        #endregion
    }
}
