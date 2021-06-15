using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;


namespace Modelo
{
    public partial class Permissao : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Permissao ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Permissao classe = new Permissao();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Permissao>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Permissao ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Permissao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Permissao Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Permissao>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Permissao SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Permissao>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Permissao> SalvarTodos(IList<Permissao> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Permissao>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Permissao> SalvarTodos(params Permissao[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Permissao>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Permissao>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Permissao>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Permissao> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Permissao obj = Activator.CreateInstance<Permissao>();
            return fabrica.GetDAOBase().ConsultarTodos<Permissao>(obj);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Permissao
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Permissao> Filtrar(int qtd)
        {
            Permissao estado = new Permissao();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Permissao Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Permissao</returns>
        public virtual Permissao UltimoInserido()
        {
            Permissao estado = new Permissao();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Permissao>(estado);
        }

        public static IList<Permissao> Filtrar(int idDepartamento, int idCargo, int idUsuario, string status)
        {
            Permissao permissao = new Permissao();
            permissao.AdicionarFiltro(Filtros.Distinct());

            if (idDepartamento > 0 || idCargo > 0)
            {
                permissao.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
                permissao.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

                if (idCargo > 0)
                {
                    permissao.AdicionarFiltro(Filtros.CriarAlias("func.Cargo", "carg"));
                    permissao.AdicionarFiltro(Filtros.Eq("carg.Id", idCargo));
                }

                if (idDepartamento > 0)
                {
                    permissao.AdicionarFiltro(Filtros.CriarAlias("func.Setor", "set"));
                    permissao.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                    permissao.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
                }
            }

            if (idUsuario > 0)
            {

                permissao.AdicionarFiltro(Filtros.CriarAlias("Funcionario", "usu"));
                permissao.AdicionarFiltro(Filtros.Eq("usu.Ativo", true));
                permissao.AdicionarFiltro(Filtros.Eq("usu.Id", idUsuario));
            }

            if (status != "N")
                permissao.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(permissao);
        }

        public virtual string GetNomeUsuario
        {
            get
            {
                return this.Funcionario != null ? this.Funcionario.NomeRazaoSocial : "";
            }
        }

        public virtual string GetDescricaoFuncao
        {
            get
            {
                return this.Funcao != null ? this.Funcao.GetDescricao : "";
            }
        }

        public virtual string GetNomeFuncao
        {
            get { return this.Funcao.GetNomeCargo; }
        }

        public virtual string GetNomeSetor
        {
            get { return this.Funcao.GetNomeSetor; }
        }

        public virtual string GetNomeUnidade
        {
            get
            {
                return this.Funcao != null && this.Funcao.Setor != null && this.Funcao.Setor.Departamento != null ?
                    this.Funcao.Setor.Departamento.GetUnidade : "N/I";
            }
        }

        public virtual string GetNomeDepartamento
        {
            get
            {
                return this.Funcao != null && this.Funcao.Setor != null && this.Funcao.Setor.Departamento != null ?
                    this.Funcao.Setor.Departamento.Nome : "N/I";
            }
        }


        public static Permissao GetPermissaoDaFuncaoEFuncionario(int idFuncionario, int idFuncao)
        {
            Permissao permissao = new Permissao();

            permissao.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            permissao.AdicionarFiltro(Filtros.Eq("func.Id", idFuncao));

            permissao.AdicionarFiltro(Filtros.CriarAlias("Funcionario", "usu"));
            permissao.AdicionarFiltro(Filtros.Eq("usu.Id", idFuncionario));

            permissao.AdicionarFiltro(Filtros.Distinct());

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Permissao>(permissao);
        }

        public static IList<Permissao> ConsultarPermissoesQueAdiamOSDaEmpresa()
        {
            Permissao aux = new Permissao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));

            Filtro[] filtros = new Filtro[2];

            filtros[0] = Filtros.Eq("AdiaPrazoLegalOS", Permissao.TODOS);
            filtros[1] = Filtros.Eq("AdiaPrazoDiretoriaOS", Permissao.TODOS);
            aux.AdicionarFiltro(Filtros.Ou(filtros));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(aux);
        }

        public static IList<Permissao> ConsultarPermissoesQueAdiamOSDoDepartamento(int idDepartamento)
        {
            Permissao aux = new Permissao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));

            Filtro[] filtros = new Filtro[2];

            filtros[0] = Filtros.Eq("AdiaPrazoLegalOS", Permissao.DEPARTAMENTO);
            filtros[1] = Filtros.Eq("AdiaPrazoDiretoriaOS", Permissao.DEPARTAMENTO);
            aux.AdicionarFiltro(Filtros.Ou(filtros));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

            aux.AdicionarFiltro(Filtros.CriarAlias("func.Setor", "set"));
            aux.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
            aux.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(aux);
        }

        public static IList<Permissao> ConsultarPermissoesQueAdiamOSDoSetor(int idSetor)
        {
            Permissao aux = new Permissao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));

            Filtro[] filtros = new Filtro[2];

            filtros[0] = Filtros.Eq("AdiaPrazoLegalOS", Permissao.SETOR);
            filtros[1] = Filtros.Eq("AdiaPrazoDiretoriaOS", Permissao.SETOR);
            aux.AdicionarFiltro(Filtros.Ou(filtros));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

            aux.AdicionarFiltro(Filtros.CriarAlias("func.Setor", "set"));
            aux.AdicionarFiltro(Filtros.Eq("set.Id", idSetor));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(aux);
        }

        public static IList<Permissao> ConsultarPermissoesQueAdiamOSDoResponsavel(int idResponsavel)
        {
            Permissao aux = new Permissao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));

            Filtro[] filtros = new Filtro[2];

            filtros[0] = Filtros.Eq("AdiaPrazoLegalOS", Permissao.RESPONSAVEL);
            filtros[1] = Filtros.Eq("AdiaPrazoDiretoriaOS", Permissao.RESPONSAVEL);
            aux.AdicionarFiltro(Filtros.Ou(filtros));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

            aux.AdicionarFiltro(Filtros.CriarAlias("func.Funcionarios", "funcions"));
            aux.AdicionarFiltro(Filtros.Eq("funcions.Id", idResponsavel));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(aux);
        }


        public static IList<Permissao> ConsultarPermissoesQueAprovamOSDaEmpresa()
        {
            Permissao aux = new Permissao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Eq("AprovaOS", Permissao.TODOS));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(aux);
        }

        public static IList<Permissao> ConsultarPermissoesQueAprovamOSDoDepartamento(int idDepartamento)
        {
            Permissao aux = new Permissao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Eq("AprovaOS", Permissao.DEPARTAMENTO));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

            aux.AdicionarFiltro(Filtros.CriarAlias("func.Setor", "set"));
            aux.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
            aux.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(aux);
        }

        public static IList<Permissao> ConsultarPermissoesQueAprovamOSDoSetor(int idSetor)
        {
            Permissao aux = new Permissao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Eq("AprovaOS", Permissao.SETOR));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

            aux.AdicionarFiltro(Filtros.CriarAlias("func.Setor", "set"));
            aux.AdicionarFiltro(Filtros.Eq("set.Id", idSetor));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(aux);
        }

        public static IList<Permissao> ConsultarPermissoesQueAprovamOSDoResponsavel(int idResponsavel)
        {
            Permissao aux = new Permissao();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Eq("AprovaOS", Permissao.RESPONSAVEL));

            aux.AdicionarFiltro(Filtros.CriarAlias("Funcao", "func"));
            aux.AdicionarFiltro(Filtros.Eq("func.Ativo", true));

            aux.AdicionarFiltro(Filtros.CriarAlias("func.Funcionarios", "funcions"));
            aux.AdicionarFiltro(Filtros.Eq("funcions.Id", idResponsavel));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Permissao>(aux);
        }
    }
}
