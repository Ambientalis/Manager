using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using System.Collections;

namespace Modelo
{
    public partial class Cliente : Pessoa
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Cliente ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Cliente classe = new Cliente();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Cliente>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Cliente ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Cliente>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Cliente Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Cliente>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Cliente SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Cliente>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Cliente> SalvarTodos(IList<Cliente> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Cliente>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Cliente> SalvarTodos(params Cliente[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Cliente>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Cliente>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Cliente>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Cliente> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Cliente obj = Activator.CreateInstance<Cliente>();
            return fabrica.GetDAOBase().ConsultarTodos<Cliente>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Cliente> ConsultarOrdemAcendente(int qtd)
        {
            Cliente ee = new Cliente();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Cliente> ConsultarOrdemDescendente(int qtd)
        {
            Cliente ee = new Cliente();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("NomeRazaoSocial"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cliente
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Cliente> Filtrar(int qtd)
        {
            Cliente estado = new Cliente();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cliente Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cliente</returns>
        public virtual Cliente UltimoInserido()
        {
            Cliente estado = new Cliente();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Cliente>(estado);
        }

        /// <summary>
        /// Consulta todos os clientes armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os clientes armazenados ordenados pelo Nome</returns>
        public static IList<Cliente> ConsultarTodosOrdemAlfabetica()
        {
            Cliente aux = new Cliente();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(aux);
        }

        public static IList<Cliente> Filtrar(string codigo, string nomeRazaoApelidoNomeFantasia, string cpfCnpj, string status, int tipo, int idOrigem)
        {
            Cliente cliente = new Cliente();
            cliente.MultiEmpresa = false;
            cliente.AdicionarFiltro(Filtros.Distinct());
            cliente.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            if (!string.IsNullOrEmpty(codigo))
                cliente.AdicionarFiltro(Filtros.Like("Codigo", codigo));

            if (!string.IsNullOrEmpty(nomeRazaoApelidoNomeFantasia))
            {
                Filtro[] filtros = new Filtro[2];

                filtros[0] = Filtros.Like("ApelidoNomeFantasia", nomeRazaoApelidoNomeFantasia);
                filtros[1] = Filtros.Like("NomeRazaoSocial", nomeRazaoApelidoNomeFantasia);

                cliente.AdicionarFiltro(Filtros.Ou(filtros));
            }

            if (!string.IsNullOrEmpty(cpfCnpj))
                cliente.AdicionarFiltro(Filtros.Like("CpfCnpj", cpfCnpj));

            if (status != "N")
                cliente.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            if (tipo > 0)
            {
                cliente.AdicionarFiltro(Filtros.Eq("EhCliente", tipo != 2));
                cliente.AdicionarFiltro(Filtros.Eq("EhFornecedor", tipo != 1));
            }
            if (idOrigem > 0)
                cliente.AdicionarFiltro(Filtros.Eq("Origem.Id", idOrigem));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(cliente);
        }

        public static IList<Cliente> Filtrar(string nome)
        {
            Cliente aux = new Cliente();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            aux.AdicionarFiltro(Filtros.Like("NomeRazaoSocial", nome));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(aux);
        }

        public static IList<Cliente> FiltrarClientesAtivos(string nome)
        {
            Cliente aux = new Cliente();
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            aux.AdicionarFiltro(Filtros.Like("NomeRazaoSocial", nome));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(aux);
        }

        public static IList<Cliente> FiltrarPorLoginEq(string login)
        {
            Cliente user = new Cliente();
            user.AdicionarFiltro(Filtros.Eq("Login", login));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(user);
        }

        public static bool ValidarUsuario(ref Cliente user)
        {
            Cliente a = new Cliente();
            a.AdicionarFiltro(Filtros.Eq("Login", user.Login));
            a.AdicionarFiltro(Filtros.Eq("Senha", user.Senha));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Cliente> funcs = fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(a);
            if (funcs != null && funcs.Count > 0)
            {
                user = funcs[0];
                return true;
            }
            return false;
        }

        public virtual string GetDescricaoEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Descricao;

                return "";
            }
        }

        public virtual string GetLogradouroEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Logradouro;

                return "";
            }
        }

        public virtual string GetNumeroEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Numero;

                return "";
            }
        }

        public virtual string GetBairroEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Bairro;

                return "";
            }
        }

        public virtual string GetComplementoEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Complemento;

                return "";
            }
        }

        public virtual string GetReferenciaEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Referencia;

                return "";
            }
        }

        public virtual string GetCepEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Cep;

                return "";
            }
        }

        public virtual Estado GetEstadoEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Cidade != null ? this.Enderecos[0].Cidade.Estado : null;

                return null;
            }
        }

        public virtual Cidade GetCidadeEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Cidade != null ? this.Enderecos[0].Cidade : null;

                return null;
            }
        }

        public virtual string GetCidade
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Cidade != null ? this.Enderecos[0].Cidade.Nome : "";

                return "";
            }
        }

        public virtual string GetSiglaEstado
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].Cidade != null ? this.Enderecos[0].Cidade.Estado.Sigla : "";

                return "";
            }
        }

        public static string GerarNumeroCliente()
        {
            Cliente aux = new Cliente();
            aux.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Cliente cliente = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Cliente>(aux);
            if (cliente == null)
            {
                return "1CM";
            }
            else
            {
                return (cliente.Id + 1) + "CM";
            }
        }

        public static bool ExisteClienteComEsteCodigo(string codigo)
        {
            Cliente aux = new Cliente();
            aux.AdicionarFiltro(Filtros.Eq("Codigo", codigo));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Cliente cliente = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Cliente>(aux);

            return cliente != null && cliente.Id > 0;
        }

        public static bool ExisteClienteDiferenteDesseIdComEsteCodigo(string codigo, int id)
        {
            Cliente aux = new Cliente();
            aux.AdicionarFiltro(Filtros.Eq("Codigo", codigo));
            aux.AdicionarFiltro(Filtros.NaoIgual("Id", id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Cliente cliente = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Cliente>(aux);

            return cliente != null && cliente.Id > 0;
        }

        public static bool JaExisteClienteComOLogin(string login)
        {
            Cliente user = new Cliente();
            user.AdicionarFiltro(Filtros.Distinct());
            user.AdicionarFiltro(Filtros.Max("Id"));
            user.AdicionarFiltro(Filtros.Eq("Login", login));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Cliente func = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Cliente>(user);
            return func != null && func.Id > 0;
        }

        public static bool JaExisteClienteComOLoginDiferenteDesseId(string login, int id)
        {
            Cliente user = new Cliente();
            user.AdicionarFiltro(Filtros.Distinct());
            user.AdicionarFiltro(Filtros.Max("Id"));
            user.AdicionarFiltro(Filtros.Eq("Login", login));
            user.AdicionarFiltro(Filtros.NaoIgual("Id", id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Cliente func = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Cliente>(user);
            return func != null && func.Id > 0;
        }

        public static IList<Cliente> ConsultarClientesPelaUnidade(int unidade)
        {
            Cliente aux = new Cliente();
            aux.MultiEmpresa = false;
            aux.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            if (unidade > 0)
                aux.AdicionarFiltro(Filtros.Eq("Emp", unidade));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(aux);
        }

        public static IList<Cliente> ConsultarClientesPelaUnidadePedidos(int unidade)
        {
            Cliente aux = new Cliente();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Ou(Filtros.Eq("Emp", unidade), Filtros.Eq("Emp", 0)));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(aux);
        }

        public static IList<Cliente> FiltrarRelatorio(int somenteClientes, string codigo, string nomeRazaoFantasiaApelido, string cpfCnpj, char unidade, string status, int idEstado, int idCidade, int idOrigem)
        {
            Cliente cliente = new Cliente();

            cliente.AdicionarFiltro(Filtros.Distinct());
            cliente.AdicionarFiltro(Filtros.Eq("EhCliente", somenteClientes == 1));
            cliente.AdicionarFiltro(Filtros.Eq("EhFornecedor", somenteClientes != 1));
            cliente.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));

            if (!string.IsNullOrEmpty(codigo))
                cliente.AdicionarFiltro(Filtros.Like("Codigo", codigo));

            if (unidade != 'I')
                cliente.AdicionarFiltro(Filtros.Eq("Unidade", unidade));

            if (!string.IsNullOrEmpty(nomeRazaoFantasiaApelido))
            {
                Filtro[] filtros = new Filtro[2];

                filtros[0] = Filtros.Like("ApelidoNomeFantasia", nomeRazaoFantasiaApelido);
                filtros[1] = Filtros.Like("NomeRazaoSocial", nomeRazaoFantasiaApelido);

                cliente.AdicionarFiltro(Filtros.Ou(filtros));
            }

            if (!string.IsNullOrEmpty(cpfCnpj))
                cliente.AdicionarFiltro(Filtros.Like("CpfCnpj", cpfCnpj));

            if (status != "N")
                cliente.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            if (idCidade > 0 || idEstado > 0)
            {
                cliente.AdicionarFiltro(Filtros.CriarAlias("Enderecos", "ends"));
                cliente.AdicionarFiltro(Filtros.CriarAlias("ends.Cidade", "cid"));

                if (idCidade > 0)
                    cliente.AdicionarFiltro(Filtros.Eq("cid.Id", idCidade));

                if (idEstado > 0)
                {
                    cliente.AdicionarFiltro(Filtros.CriarAlias("cid.Estado", "est"));
                    cliente.AdicionarFiltro(Filtros.Eq("est.Id", idEstado));
                }

            }
            if (idOrigem > 0)
                cliente.AdicionarFiltro(Filtros.Eq("Origem.Id", idOrigem));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Cliente>(cliente);
        }

        public virtual string GetDescricaoCompletaDoEndereco
        {
            get
            {
                if (this.Enderecos != null && this.Enderecos.Count > 0)
                    return this.Enderecos[0].GetDescricaoFormatada;
                return "";
            }
        }

        public virtual string GetDescricaoTipo
        {
            get
            {
                return (this.ehCliente && this.ehFornecedor ? "Cliente e fornecedor" : this.ehCliente ? "Cliente" : this.ehFornecedor ? "Fornecedor" : "N/I");
            }
        }

        public virtual ClassificacaoCliente GetUltimaClassificacao
        {
            get
            {
                return this.Classificacoes != null && this.Classificacoes.Count > 0 ?
                    this.Classificacoes.OrderBy(classi => classi.Data).Last() : null;
            }
        }

        public virtual string GetDescricaoTipoPessoa
        {
            get
            {
                return Pessoa.FISICA == this.Tipo ? "Física" : "Jurídica";
            }
        }

        public virtual List<string> GetEmailsNotificacoes
        {
            get
            {
                List<string> retorno = new List<string>();

                if (!string.IsNullOrEmpty(this.Email) && this.EmailRecebeNotificacoes)
                    retorno.Add(this.Email);

                if (this.Contatos != null)
                {
                    //Adicionando e-mail do primeiro contato
                    if (this.Contatos.Count > 0 && this.Contatos[0] != null && !string.IsNullOrEmpty(this.Contatos[0].Email) && this.Contatos[0].RecebeNotificacoes)
                        retorno.Add(this.Contatos[0].Email);

                    //Adicionando e-mail do segundo contato
                    if (this.Contatos.Count > 1 && this.Contatos[1] != null && !string.IsNullOrEmpty(this.Contatos[1].Email) && this.Contatos[1].RecebeNotificacoes)
                        retorno.Add(this.Contatos[1].Email);

                    //Adicionando e-mail do terceiro contato
                    if (this.Contatos.Count > 2 && this.Contatos[2] != null && !string.IsNullOrEmpty(this.Contatos[2].Email) && this.Contatos[2].RecebeNotificacoes)
                        retorno.Add(this.Contatos[2].Email);
                }

                return retorno.Distinct().ToList();
            }
        }
    }
}
