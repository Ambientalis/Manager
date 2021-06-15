using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Funcao: ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Funcao ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Funcao classe = new Funcao();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Funcao>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Funcao ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Funcao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Funcao Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Funcao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Funcao SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Funcao>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Funcao> SalvarTodos(IList<Funcao> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Funcao>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Funcao> SalvarTodos(params Funcao[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Funcao>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Funcao>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Funcao>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Funcao> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Funcao obj = Activator.CreateInstance<Funcao>();
            return fabrica.GetDAOBase().ConsultarTodos<Funcao>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Funcao> ConsultarOrdemAcendente(int qtd)
        {
            Funcao ee = new Funcao();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Funcao> ConsultarOrdemDescendente(int qtd)
        {
            Funcao ee = new Funcao();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Funcao> Filtrar(int qtd)
        {
            Funcao estado = new Funcao();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual Funcao UltimoInserido()
        {
            Funcao estado = new Funcao();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Funcao>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<Funcao> ConsultarTodosOrdemAlfabetica()
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Cargo", "carg"));
            aux.AdicionarFiltro(Filtros.SetOrderAsc("carg.Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarTodosAtivosInativosOrdemAlfabetica()
        {
            Funcao aux = new Funcao();            
            aux.AdicionarFiltro(Filtros.CriarAlias("Cargo", "carg"));
            aux.AdicionarFiltro(Filtros.SetOrderAsc("carg.Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public virtual string GetDescricao 
        {
            get 
            {
                return this.Cargo.Nome + " - " + this.Setor.Departamento.Nome + " - " + this.Setor.Nome;
            } 
        }

        public virtual string GetNomeCargo 
        {
            get 
            {
                return this.Cargo.Nome;
            } 
        }

        public virtual string GetNomeUnidade
        {
            get
            {
                return this.Setor.Departamento.GetUnidade;
            }
        }

        public virtual string GetNomeDepartamento 
        {
            get 
            {
                return this.Setor.Departamento.Nome;
            } 
        }

        public virtual string GetNomeSetor 
        {
            get 
            {
                return this.Setor.Nome;
            } 
        }

        public virtual string GetAtivo
        {
            get
            {
                return this.Ativo ? "Sim" : "Não";
            }
        }

        public static IList<Funcao> ConsultarFuncoesDoFuncionario(int idFuncionario)
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Funcionarios", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Id", idFuncionario));            
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarFuncoesQueAdiamOSDaEmpresa()
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "perms"));

            Filtro[] filtros = new Filtro[3];
                    
            filtros[0] = Filtros.Eq("perms.AdiaPrazoLegalOS", Permissao.TODOS);
            filtros[1] = Filtros.Eq("perms.AdiaPrazoDiretoriaOS", Permissao.TODOS);
            filtros[2] = Filtros.Eq("perms.AdiaVencimentoOS", Permissao.TODOS);
            aux.AdicionarFiltro(Filtros.Ou(filtros));
                        
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarFuncoesQueAdiamOSDoDepartamento(int idDepartamento)
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "perms"));

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Eq("perms.AdiaPrazoLegalOS", Permissao.DEPARTAMENTO);
            filtros[1] = Filtros.Eq("perms.AdiaPrazoDiretoriaOS", Permissao.DEPARTAMENTO);
            filtros[2] = Filtros.Eq("perms.AdiaVencimentoOS", Permissao.DEPARTAMENTO);
            aux.AdicionarFiltro(Filtros.Ou(filtros));

            aux.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
            aux.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
            aux.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarFuncoesQueAdiamOSDoSetor(int idSetor)
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "perms"));

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Eq("perms.AdiaPrazoLegalOS", Permissao.SETOR);
            filtros[1] = Filtros.Eq("perms.AdiaPrazoDiretoriaOS", Permissao.SETOR);
            filtros[2] = Filtros.Eq("perms.AdiaVencimentoOS", Permissao.SETOR);
            aux.AdicionarFiltro(Filtros.Ou(filtros));

            aux.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));            
            aux.AdicionarFiltro(Filtros.Eq("set.Id", idSetor));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarFuncoesQueAdiamOSDoResponsavel(int idResponsavel)
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "perms"));

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Eq("perms.AdiaPrazoLegalOS", Permissao.RESPONSAVEL);
            filtros[1] = Filtros.Eq("perms.AdiaPrazoDiretoriaOS", Permissao.RESPONSAVEL);
            filtros[2] = Filtros.Eq("perms.AdiaVencimentoOS", Permissao.RESPONSAVEL);
            aux.AdicionarFiltro(Filtros.Ou(filtros));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcionarios", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Id", idResponsavel));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarFuncoesQueAprovamOSDaEmpresa()
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "perms"));
            aux.AdicionarFiltro(Filtros.Eq("perms.AprovaOS", Permissao.TODOS));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarFuncoesQueAprovamOSDoDepartamento(int idDepartamento)
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "perms"));
            aux.AdicionarFiltro(Filtros.Eq("perms.AprovaOS", Permissao.DEPARTAMENTO));            

            aux.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
            aux.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
            aux.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarFuncoesQueAprovamOSDoSetor(int idSetor)
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "perms"));
            aux.AdicionarFiltro(Filtros.Eq("perms.AprovaOS", Permissao.SETOR));

            aux.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
            aux.AdicionarFiltro(Filtros.Eq("set.Id", idSetor));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }

        public static IList<Funcao> ConsultarFuncoesQueAprovamOSDoResponsavel(int idResponsavel)
        {
            Funcao aux = new Funcao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.CriarAlias("Permissoes", "perms"));
            aux.AdicionarFiltro(Filtros.Eq("perms.AprovaOS", Permissao.RESPONSAVEL));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcionarios", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Id", idResponsavel));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Funcao>(aux);
        }
    }
}
