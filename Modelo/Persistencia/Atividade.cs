using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Atividade : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Atividade ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Atividade classe = new Atividade();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Atividade>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Atividade ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Atividade>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Atividade Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Atividade>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Atividade SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Atividade>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Atividade> SalvarTodos(IList<Atividade> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Atividade>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Atividade> SalvarTodos(params Atividade[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Atividade>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Atividade>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Atividade>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Atividade> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Atividade obj = Activator.CreateInstance<Atividade>();
            return fabrica.GetDAOBase().ConsultarTodos<Atividade>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Atividade> ConsultarOrdemAcendente(int qtd)
        {
            Atividade ee = new Atividade();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Atividade>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Atividade> ConsultarOrdemDescendente(int qtd)
        {
            Atividade ee = new Atividade();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Atividade>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Atividade> Filtrar(int qtd)
        {
            Atividade estado = new Atividade();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Atividade>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual Atividade UltimoInserido()
        {
            Atividade estado = new Atividade();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Atividade>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<Atividade> ConsultarTodosOrdemAlfabetica()
        {
            Atividade aux = new Atividade();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Atividade>(aux);
        }

        public virtual string GetNomeResponsavel
        {
            get
            {
                return this.Executor != null ? this.Executor.NomeRazaoSocial.ToString() : "";
            }
        }

        public virtual string GetNomeTipoAtividade
        {
            get
            {
                return this.TipoAtividade != null ? this.TipoAtividade.Nome : "";
            }
        }

        public virtual string GetNumeroOS
        {
            get
            {
                return this.OrdemServico != null ? this.OrdemServico.Codigo : "";
            }
        }

        public virtual string GetData
        {
            get
            {
                return this.Data.ToShortDateString();
            }
        }

        public virtual string GetNumeroPedido
        {
            get
            {
                return this.OrdemServico != null && this.OrdemServico.Pedido != null ? this.OrdemServico.Pedido.Codigo : "";
            }
        }

        public virtual string GetNomeCliente
        {
            get
            {
                return this.OrdemServico != null ? this.OrdemServico.GetNomeCliente : "";
            }
        }        

        public virtual Detalhamento GetUltimoDelhamento
        {
            get
            {
                if (this.Detalhamentos != null && this.Detalhamentos.Count > 0)
                    return this.Detalhamentos[this.Detalhamentos.Count - 1];
                else
                    return null;
            }
        }

        public static IList<Atividade> Filtrar(string numeroOS, string numeroPedido, DateTime dataDe, DateTime dataAte, int idCliente, int idResponsavel, string status, int idTipoAtividade, string descricao, Permissao permissao, int idUsuarioLogado)
        {
            if (permissao == null)
                return null;

            Atividade atividade = new Atividade();
            atividade.AdicionarFiltro(Filtros.Distinct());

            atividade.AdicionarFiltro(Filtros.CriarAlias("OrdemServico", "ord"));

            if (!string.IsNullOrEmpty(numeroOS))
                atividade.AdicionarFiltro(Filtros.Like("ord.Codigo", numeroOS));

            if (permissao != null && permissao.AcessaOS == Permissao.SETOR)
            {
                Funcao funcaoSetor = permissao.Funcao != null && permissao.Funcao.Setor != null ? permissao.Funcao : null;

                if (funcaoSetor == null)
                    return null;

                atividade.AdicionarFiltro(Filtros.CriarAlias("ord.Setor", "set"));
                atividade.AdicionarFiltro(Filtros.Eq("set.Id", funcaoSetor.Setor.Id));
            }

            if (permissao != null && permissao.AcessaOS == Permissao.DEPARTAMENTO)
            {
                Funcao funcaoDepartamento = permissao.Funcao != null && permissao.Funcao.Setor != null && permissao.Funcao.Setor.Departamento != null ? permissao.Funcao : null;

                if (funcaoDepartamento == null)
                    return null;

                atividade.AdicionarFiltro(Filtros.CriarAlias("ord.Setor", "set"));
                atividade.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                atividade.AdicionarFiltro(Filtros.Eq("dept.Id", funcaoDepartamento.Setor.Departamento.Id));
            }

            atividade.AdicionarFiltro(Filtros.CriarAlias("Executor", "resp"));

            //Se o usuário não possui permissão para acessar nenhuma OS trazer somente as OS que ele for responsavel
            if (permissao != null && permissao.AcessaOS == Permissao.NENHUMA)
                atividade.AdicionarFiltro(Filtros.Eq("resp.Id", idUsuarioLogado));
            else
            {
                if (idResponsavel > 0)
                    atividade.AdicionarFiltro(Filtros.Eq("resp.Id", idResponsavel));
            }

            atividade.AdicionarFiltro(Filtros.CriarAlias("ord.Pedido", "ped"));

            if (!string.IsNullOrEmpty(numeroPedido))
                atividade.AdicionarFiltro(Filtros.Like("ped.Codigo", numeroPedido));

            if (!string.IsNullOrEmpty(descricao))
                atividade.AdicionarFiltro(Filtros.Like("Descricao", descricao));


            atividade.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));
            atividade.AdicionarFiltro(Filtros.SetOrderDesc("Data"));


            if (idTipoAtividade > 0)
            {
                atividade.AdicionarFiltro(Filtros.CriarAlias("TipoAtividade", "tipVisit"));
                atividade.AdicionarFiltro(Filtros.Eq("tipVisit.Id", idTipoAtividade));
            }


            if (idCliente > 0)
            {
                atividade.AdicionarFiltro(Filtros.CriarAlias("ped.Cliente", "cli"));
                atividade.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
            }



            if (status != "N")
                atividade.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Atividade>(atividade);
        }

        public static IList<Atividade> FiltrarAtividadesDoUsuarioLogadoComMesmosFrutos(string numeroOS, string numeroPedido, DateTime dataDe, DateTime dataAte, int idCliente, string status, int idTipoAtividade, string descricao, int idUsuarioLogado)
        {
            Atividade atividade = new Atividade();
            atividade.AdicionarFiltro(Filtros.Distinct());

            atividade.AdicionarFiltro(Filtros.CriarAlias("OrdemServico", "ord"));

            if (!string.IsNullOrEmpty(numeroOS))
                atividade.AdicionarFiltro(Filtros.Like("ord.Codigo", numeroOS));

            
            atividade.AdicionarFiltro(Filtros.CriarAlias("Executor", "resp"));
            atividade.AdicionarFiltro(Filtros.Eq("resp.Id", idUsuarioLogado));
          

            atividade.AdicionarFiltro(Filtros.CriarAlias("ord.Pedido", "ped"));

            if (!string.IsNullOrEmpty(numeroPedido))
                atividade.AdicionarFiltro(Filtros.Like("ped.Codigo", numeroPedido));

            if (!string.IsNullOrEmpty(descricao))
                atividade.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            atividade.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            atividade.AdicionarFiltro(Filtros.SetOrderDesc("Data"));


            if (idTipoAtividade > 0)
            {
                atividade.AdicionarFiltro(Filtros.CriarAlias("TipoAtividade", "tipVisit"));
                atividade.AdicionarFiltro(Filtros.Eq("tipVisit.Id", idTipoAtividade));
            }

            if (idCliente > 0)
            {
                atividade.AdicionarFiltro(Filtros.CriarAlias("ped.Cliente", "cli"));
                atividade.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
            }

            if (status != "N")
                atividade.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Atividade>(atividade);
        }

        public virtual Pedido GetPedido
        {
            get
            {
                return this.OrdemServico != null && this.OrdemServico.Pedido != null ? this.OrdemServico.Pedido : null;
            }
        }

        public virtual OrdemServico GetOS
        {
            get
            {
                return this.OrdemServico != null ? this.OrdemServico : null;
            }
        }
    }
}
