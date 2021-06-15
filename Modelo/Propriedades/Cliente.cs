using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "clientes", Extends = "Modelo.Pessoa, Modelo", Name = "Modelo.Cliente, Modelo")]
    [Key(Column = "id")]
    public partial class Cliente : Pessoa
    {
        public Cliente(int id) { this.Id = id; }
        public Cliente(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Cliente() { }

        #region ___________ Atributos ___________

        private Boolean ehCliente;
        private Boolean ehFornecedor;

        private string telefone1;
        private string telefone2;
        private string email;
        private bool emailRecebeNotificacoes;
        private bool emailRecebeLoginSenha;

        private string login;
        private string senha;
        private bool exibirSite;

        private IList<Contato> contatos;
        private IList<Pedido> pedidos;
        private IList<ClassificacaoCliente> classificacoes = new List<ClassificacaoCliente>();
        private IList<MovimentacaoFinanceira> movimentacoesFinanceiras;

        #endregion

        #region __________ Propriedades _________

        [Property(Column = "eh_cliente", Type = "TrueFalse")]
        public virtual Boolean EhCliente
        {
            get { return ehCliente; }
            set { ehCliente = value; }
        }

        [Property(Column = "eh_fornecedor", Type = "TrueFalse")]
        public virtual Boolean EhFornecedor
        {
            get { return ehFornecedor; }
            set { ehFornecedor = value; }
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

        [Property(Column = "email_recebe_notificacoes", Type = "TrueFalse")]
        public virtual bool EmailRecebeNotificacoes
        {
            get { return emailRecebeNotificacoes; }
            set { emailRecebeNotificacoes = value; }
        }

        [Property(Column = "email_recebe_login_senha", Type = "TrueFalse")]
        public virtual bool EmailRecebeLoginSenha
        {
            get { return emailRecebeLoginSenha; }
            set { emailRecebeLoginSenha = value; }
        }

        [Property(Column = "login")]
        public virtual string Login
        {
            get { return login; }
            set { login = value; }
        }

        [Property(Column = "senha")]
        public virtual string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        [Property(Column = "exibir_site", Type = "TrueFalse")]
        public virtual bool ExibirSite
        {
            get { return exibirSite; }
            set { exibirSite = value; }
        }

        [Bag(Name = "Contatos", Table = "contatos")]
        [Key(2, Column = "id_cliente")]
        [OneToMany(3, Class = "Modelo.Contato, Modelo")]
        public virtual IList<Contato> Contatos
        {
            get { return contatos; }
            set { contatos = value; }
        }

        [Bag(Name = "Pedidos", Table = "pedidos")]
        [Key(2, Column = "id_cliente")]
        [OneToMany(3, Class = "Modelo.Pedido, Modelo")]
        public virtual IList<Pedido> Pedidos
        {
            get { return pedidos; }
            set { pedidos = value; }
        }

        [Bag(Name = "Classificacoes", Table = "classificacoes_clientes", Cascade = "delete")]
        [Key(2, Column = "id_cliente")]
        [OneToMany(3, Class = "Modelo.ClassificacaoCliente, Modelo")]
        public virtual IList<ClassificacaoCliente> Classificacoes
        {
            get { return classificacoes; }
            set { classificacoes = value; }
        }

        [Bag(Name = "MovimentacoesFinanceiras", Table = "movimentacoes_financeiras")]
        [Key(2, Column = "id_cliente_fornecedor")]
        [OneToMany(3, Class = "Modelo.MovimentacaoFinanceira, Modelo")]
        public virtual IList<MovimentacaoFinanceira> MovimentacoesFinanceiras
        {
            get { return movimentacoesFinanceiras; }
            set { movimentacoesFinanceiras = value; }
        }

        #endregion
    }
}
