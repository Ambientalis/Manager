using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "funcionarios", Extends = "Modelo.Pessoa, Modelo", Name = "Modelo.Funcionario, Modelo")]
    [Key(Column = "id")]
    public partial class Funcionario : Pessoa
    {
        #region __________ Construtores _____________

        public Funcionario(int id)
        {
            this.MultiEmpresa = false;
            this.Id = id;
        }
        public Funcionario(Object id)
        {
            this.MultiEmpresa = false;
            this.Id = Convert.ToInt32("0" + id.ToString());
        }
        public Funcionario()
        {
            this.MultiEmpresa = false;
        }

        #endregion

        #region ___________ Atributos ___________

        private string celularCorporativo;
        private string celularPessoal;
        private string telefoneResidencial;
        private string nomeContatoEmergencia;
        private string telefoneContatoEmergencia;
        private string login;
        private string senha;
        private IList<Funcao> funcoes;
        private IList<OrdemServico> ordensServico;
        private IList<OrdemServico> ordensSobSuaResponsabilidade;

        private IList<Visita> visitas;
        private IList<Atividade> atividades;
        private IList<Permissao> permissoes;
        private IList<Veiculo> veiculos;


        public static Funcionario GetConfiguracoesSistema()
        {

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();

            Funcionario configx = new Funcionario();


            var xxx = fabrica.GetDAOBase().ConsultarTodos<Funcionario>(configx);


            Funcionario config = new Funcionario();
            var x = new FabricaDAONHibernateBase().GetDAOBase().ConsultarTodos<Funcionario>(config);
            if (config == null)
                return new Funcionario().Salvar();
            return config;
        }


        private IList<Reserva> reservas;

        private string emailCorporativo;
        private string emailPessoal;

        private IList<Pedido> pedidosVendidos;

        private bool vendedor;

        #endregion

        #region ___________ Propriedades ___________

        [Property(Column = "celular_corporativo")]
        public virtual string CelularCorporativo
        {
            get { return celularCorporativo; }
            set { celularCorporativo = value; }
        }

        [Property(Column = "celular_pessoal")]
        public virtual string CelularPessoal
        {
            get { return celularPessoal; }
            set { celularPessoal = value; }
        }

        [Property(Column = "telefone_residencial")]
        public virtual string TelefoneResidencial
        {
            get { return telefoneResidencial; }
            set { telefoneResidencial = value; }
        }

        [Property(Column = "nome_contato_emergencia")]
        public virtual string NomeContatoEmergencia
        {
            get { return nomeContatoEmergencia; }
            set { nomeContatoEmergencia = value; }
        }

        [Property(Column = "telefone_contato_emergencia")]
        public virtual string TelefoneContatoEmergencia
        {
            get { return telefoneContatoEmergencia; }
            set { telefoneContatoEmergencia = value; }
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

        [Bag(Table = "funcionarios_funcoes", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_funcionario")]
        [ManyToMany(3, Class = "Modelo.Funcao, Modelo", Column = "id_funcao")]
        public virtual IList<Funcao> Funcoes
        {
            get { return funcoes; }
            set { funcoes = value; }
        }

        [Bag(Table = "ordens_servico_funcionarios")]
        [Key(2, Column = "id_funcionario")]
        [ManyToMany(3, Class = "Modelo.OrdemServico, Modelo", Column = "id_ordem_servico")]
        public virtual IList<OrdemServico> OrdensServico
        {
            get { return ordensServico; }
            set { ordensServico = value; }
        }

        [Bag(Name = "OrdensSobSuaResponsabilidade", Table = "ordens_servico", Cascade = "delete")]
        [Key(2, Column = "id_responsavel")]
        [OneToMany(3, Class = "Modelo.OrdemServico, Modelo")]
        public virtual IList<OrdemServico> OrdensSobSuaResponsabilidade
        {
            get { return ordensSobSuaResponsabilidade; }
            set { ordensSobSuaResponsabilidade = value; }
        }

        [Bag(Name = "Visitas", Table = "visitas", Cascade = "delete")]
        [Key(2, Column = "id_visitante")]
        [OneToMany(3, Class = "Modelo.Visita, Modelo")]
        public virtual IList<Visita> Visitas
        {
            get { return visitas; }
            set { visitas = value; }
        }

        [Bag(Name = "Atividades", Table = "atividades", Cascade = "delete")]
        [Key(2, Column = "id_executor")]
        [OneToMany(3, Class = "Modelo.Atividade, Modelo")]
        public virtual IList<Atividade> Atividades
        {
            get { return atividades; }
            set { atividades = value; }
        }

        [Bag(Name = "Permissoes", Table = "permissoes", Cascade = "delete")]
        [Key(2, Column = "id_funcionario")]
        [OneToMany(3, Class = "Modelo.Permissao, Modelo")]
        public virtual IList<Permissao> Permissoes
        {
            get { return permissoes; }
            set { permissoes = value; }
        }

        [Bag(Name = "Veiculos", Table = "veiculos")]
        [Key(2, Column = "id_gestor")]
        [OneToMany(3, Class = "Modelo.Veiculo, Modelo")]
        public virtual IList<Veiculo> Veiculos
        {
            get { return veiculos; }
            set { veiculos = value; }
        }

        [Bag(Name = "Reservas", Table = "reservas")]
        [Key(2, Column = "id_responsavel")]
        [OneToMany(3, Class = "Modelo.Reserva, Modelo")]
        public virtual IList<Reserva> Reservas
        {
            get { return reservas; }
            set { reservas = value; }
        }

        [Property(Column = "email_corporativo")]
        public virtual string EmailCorporativo
        {
            get { return emailCorporativo; }
            set { emailCorporativo = value; }
        }

        [Property(Column = "email_pessoal")]
        public virtual string EmailPessoal
        {
            get { return emailPessoal; }
            set { emailPessoal = value; }
        }

        [Bag(Name = "PedidosVendidos", Table = "pedidos")]
        [Key(2, Column = "id_vendedor")]
        [OneToMany(3, Class = "Modelo.Pedido, Modelo")]
        public virtual IList<Pedido> PedidosVendidos
        {
            get { return pedidosVendidos; }
            set { pedidosVendidos = value; }
        }

        [Property(Column = "vendedor", Type = "TrueFalse")]
        public virtual bool Vendedor
        {
            get { return vendedor; }
            set { vendedor = value; }
        }

        #endregion
    }
}
