using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "contatos", Name = "Modelo.Contato, Modelo")]
    public partial class Contato : ObjetoBase
    {
        public Contato(int id) { this.Id = id; }
        public Contato(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Contato() { }

        #region ___________ Atributos ___________

        private string nome;
        private string funcao;
        private string telefone1;
        private string telefone2;
        private string email;
        private bool recebeNotificacoes;
        private bool emailRecebeLoginSenha;
        private DateTime dataNascimento;
        private Cliente cliente;       
        
        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Property(Column = "funcao")]
        public virtual string Funcao
        {
            get { return funcao; }
            set { funcao = value; }
        }

        [Property(Column = "telefone1")]
        public virtual string Telefone1
        {
            get { return telefone1; }
            set { telefone1 = value; }
        }

        [Property(Column = "telefone2")]
        public virtual string Telefone2
        {
            get { return telefone2; }
            set { telefone2 = value; }
        }

        [Property(Column = "email")]
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Property(Column = "recebe_notificacoes", Type = "TrueFalse")]
        public virtual bool RecebeNotificacoes
        {
            get { return recebeNotificacoes; }
            set { recebeNotificacoes = value; }
        }

        [Property(Column = "email_recebe_login_senha", Type = "TrueFalse")]
        public virtual bool EmailRecebeLoginSenha
        {
            get { return emailRecebeLoginSenha; }
            set { emailRecebeLoginSenha = value; }
        }

        [Property(Column = "data_nascimento")]
        public virtual DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }

        [ManyToOne(Name = "Cliente", Class = "Modelo.Cliente, Modelo", Column = "id_cliente")]
        public virtual Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        #endregion
    }
}
