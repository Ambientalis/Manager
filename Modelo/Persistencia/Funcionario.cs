using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;

namespace Modelo
{
    public partial class Funcionario : Pessoa
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Funcionario ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Funcionario classe = new Funcionario();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Funcionario>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Funcionario ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Funcionario>(this);
        }



        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Funcionario Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Funcionario>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Funcionario SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Funcionario>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Funcionario> SalvarTodos(IList<Funcionario> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Funcionario>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Funcionario> SalvarTodos(params Funcionario[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Funcionario>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Funcionario>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Funcionario>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Funcionario> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Funcionario obj = Activator.CreateInstance<Funcionario>();
            return fabrica.GetDAOBase().ConsultarTodos<Funcionario>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Funcionario> ConsultarOrdemAcendente(int qtd)
        {
            Funcionario ee = new Funcionario();
            ee.AdicionarFiltro(Filtros.Eq("Ativo", true));
            ee.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Funcionario> ConsultarOrdemDescendente(int qtd)
        {
            Funcionario ee = new Funcionario();
            ee.AdicionarFiltro(Filtros.Eq("Ativo", true));
            ee.AdicionarFiltro(Filtros.SetOrderDesc("NomeRazaoSocial"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Funcionario
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Funcionario> Filtrar(int qtd)
        {
            Funcionario estado = new Funcionario();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Funcionario Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Funcionario</returns>
        public virtual Funcionario UltimoInserido()
        {
            Funcionario estado = new Funcionario();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Funcionario>(estado);
        }

        /// <summary>
        /// Consulta todos os Funcionarios armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Funcionarios armazenados ordenados pelo Nome</returns>
        public static IList<Funcionario> ConsultarTodosOrdemAlfabetica()
        {
            Funcionario aux = new Funcionario();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(aux);
        }

        public static IList<Funcionario> FiltrarPorLoginEq(string login)
        {
            Funcionario user = new Funcionario();
            user.AdicionarFiltro(Filtros.Eq("Login", login));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(user);
        }

        public static bool JaExisteFuncionarioComOLogin(string login)
        {
            Funcionario user = new Funcionario();
            user.AdicionarFiltro(Filtros.Distinct());
            user.AdicionarFiltro(Filtros.Max("Id"));
            user.AdicionarFiltro(Filtros.Eq("Login", login));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Funcionario func = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Funcionario>(user);
            return func != null && func.Id > 0;
        }

        public static IList<Funcionario> FiltrarPorNome(string nome)
        {
            Funcionario user = new Funcionario();
            if (nome != "")
                user.AdicionarFiltro(Filtros.Like("NomeRazaoSocial", nome));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(user);
        }

        public static bool ValidarUsuario(ref Funcionario user)
        {
            Funcionario a = new Funcionario();
            a.AdicionarFiltro(Filtros.Eq("Login", user.Login));
            a.AdicionarFiltro(Filtros.Eq("Senha", user.Senha));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Funcionario> funcs = fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(a);
            if (funcs != null && funcs.Count > 0)
            {
                user = funcs[0];
                return true;
            }
            return false;
        }

        public static IList<Funcionario> Filtrar(string codigo, string nomeApelido, string cpf, string status, int idFuncao, int filtrarVendedores, int unidade)
        {
            Funcionario funcionario = new Funcionario();

            funcionario.AdicionarFiltro(Filtros.Distinct());

            if (!string.IsNullOrEmpty(codigo))
                funcionario.AdicionarFiltro(Filtros.Like("Codigo", codigo));

            if (!string.IsNullOrEmpty(nomeApelido))
            {
                Filtro[] filtros = new Filtro[2];

                filtros[0] = Filtros.Like("ApelidoNomeFantasia", nomeApelido);
                filtros[1] = Filtros.Like("NomeRazaoSocial", nomeApelido);

                funcionario.AdicionarFiltro(Filtros.Ou(filtros));
            }

            if (unidade > -1)
                funcionario.AdicionarFiltro(Filtros.Eq("Emp", unidade));

            if (!string.IsNullOrEmpty(cpf))
                funcionario.AdicionarFiltro(Filtros.Like("CpfCnpj", cpf));

            if (status != "N")
                funcionario.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            if (filtrarVendedores > 0)
                funcionario.AdicionarFiltro(Filtros.Eq("Vendedor", filtrarVendedores == 1));

            if (idFuncao > 0)
            {
                funcionario.AdicionarFiltro(Filtros.CriarAlias("Funcoes", "funcs"));
                funcionario.AdicionarFiltro(Filtros.Eq("funcs.Id", idFuncao));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(funcionario);
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

        public virtual string GetDescricaoFuncoes
        {
            get
            {
                string retorno = "";
                if (this.Funcoes != null && this.Funcoes.Count > 0)
                {
                    foreach (Funcao item in this.Funcoes)
                    {
                        if (item == this.Funcoes[this.Funcoes.Count - 1])
                            retorno += item.GetDescricao;
                        else
                            retorno += item.GetDescricao + "; ";
                    }
                }

                return retorno;
            }
        }

        public virtual string GetDescricaoVendedor
        {
            get
            {
                return this.Vendedor ? "Sim" : "";
            }
        }

        public static string GerarNumeroCodigo()
        {
            Funcionario aux = new Funcionario();
            aux.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Funcionario funcionario = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Funcionario>(aux);
            if (funcionario == null)
            {
                return "1";
            }
            else
            {
                return (funcionario.Id + 1).ToString();
            }
        }

        public static IList<Funcionario> FiltrarPermissoes(int idDepartamento, int idCargo, int idUsuario, string status)
        {
            Funcionario funcionario = new Funcionario();

            funcionario.AdicionarFiltro(Filtros.Distinct());

            funcionario.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));

            funcionario.AdicionarFiltro(Filtros.CriarAlias("Funcoes", "func"));

            if (idDepartamento > 0 || idCargo > 0)
            {

                if (status != "N")
                    funcionario.AdicionarFiltro(Filtros.Eq("func.Ativo", status == "T"));

                if (idCargo > 0)
                {
                    funcionario.AdicionarFiltro(Filtros.CriarAlias("func.Cargo", "carg"));
                    funcionario.AdicionarFiltro(Filtros.Eq("carg.Id", idCargo));
                }

                if (idDepartamento > 0)
                {
                    funcionario.AdicionarFiltro(Filtros.CriarAlias("func.Setor", "set"));
                    funcionario.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                    funcionario.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
                }
            }

            if (idUsuario > 0)
            {
                funcionario.AdicionarFiltro(Filtros.Eq("Id", idUsuario));
            }

            if (status != "N")
                funcionario.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(funcionario);
        }

        public static bool JaExisteFuncionarioComOLoginDiferenteDesseId(string login, int id)
        {
            Funcionario user = new Funcionario();
            user.AdicionarFiltro(Filtros.Distinct());
            user.AdicionarFiltro(Filtros.Eq("Login", login));
            user.AdicionarFiltro(Filtros.NaoIgual("Id", id));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Funcionario func = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Funcionario>(user);
            return func != null && func.Id > 0;
        }

        public static IList<Funcionario> ConsultarFuncionariosQueSaoGestoresDiretoresDoDepartamentoDaOS(Departamento departamento)
        {
            if (departamento == null || departamento.Id == 0)
                return null;

            Funcionario funcionario = new Funcionario();

            funcionario.AdicionarFiltro(Filtros.Distinct());
            funcionario.AdicionarFiltro(Filtros.Eq("Ativo", true));

            //trazendo somente os funcionarios que sejam diretores ou gestores do departamento

            funcionario.AdicionarFiltro(Filtros.CriarAlias("Funcoes", "func"));

            funcionario.AdicionarFiltro(Filtros.CriarAlias("func.Cargo", "carg"));
            funcionario.AdicionarFiltro(Filtros.Ou(Filtros.Eq("carg.Id", 1), Filtros.Eq("carg.Id", 2)));   // diretor e gestor usam ids 1e 2 respectivamente.

            funcionario.AdicionarFiltro(Filtros.CriarAlias("func.Setor", "set"));
            funcionario.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
            funcionario.AdicionarFiltro(Filtros.Eq("dept.Id", departamento.Id));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(funcionario);
        }

        public static IList<Funcionario> ConsultarVendedoresOrdemAlfabetica()
        {
            Funcionario aux = new Funcionario();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("NomeRazaoSocial"));
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Eq("Vendedor", true));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcionario>(aux);
        }
    }
}

