using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Visita : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Visita ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Visita classe = new Visita();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Visita>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Visita ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Visita>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Visita Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Visita>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Visita SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Visita>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Visita> SalvarTodos(IList<Visita> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Visita>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Visita> SalvarTodos(params Visita[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Visita>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Visita>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Visita>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Visita> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Visita obj = Activator.CreateInstance<Visita>();
            return fabrica.GetDAOBase().ConsultarTodos<Visita>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Visita> ConsultarOrdemAcendente(int qtd)
        {
            Visita ee = new Visita();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Visita>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Visita> ConsultarOrdemDescendente(int qtd)
        {
            Visita ee = new Visita();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Visita>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Visita> Filtrar(int qtd)
        {
            Visita estado = new Visita();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Visita>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual Visita UltimoInserido()
        {
            Visita estado = new Visita();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Visita>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<Visita> ConsultarTodosOrdemAlfabetica()
        {
            Visita aux = new Visita();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Visita>(aux);
        }

        public virtual string GetDataInicio
        {
            get
            {
                return this.DataInicio.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public virtual string GetDataFim
        {
            get
            {
                return this.DataFim.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public virtual string GetNomeResponsavel
        {
            get
            {
                return this.Visitante != null ? this.Visitante.NomeRazaoSocial.ToString() : "";
            }
        }

        public virtual string GetAceita
        {
            get
            {
                return this.Aceita ? "Sim" : this.EmailAceitou != null && this.EmailAceitou != "" ? "Não" : "";
            }
        }

        public virtual string GetNomeCliente
        {
            get
            {
                return this.OrdemServico != null ? this.OrdemServico.GetNomeCliente : "";
            }
        }

        public virtual string GetNumeroOS
        {
            get
            {
                return this.OrdemServico != null ? this.OrdemServico.Codigo : "";
            }
        }

        public virtual string GetNumeroPedido
        {
            get
            {
                return this.OrdemServico != null && this.OrdemServico.Pedido != null ? this.OrdemServico.Pedido.Codigo : "";
            }
        }

        public virtual Cliente GetCliente
        {
            get
            {
                return this.OrdemServico != null && this.OrdemServico.Pedido != null && this.OrdemServico.Pedido.Cliente != null ? this.OrdemServico.Pedido.Cliente : null;
            }
        }

        public virtual string GetNomeTipoVisita
        {
            get
            {
                return this.TipoVisita != null ? this.TipoVisita.Nome : "";
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

        public static IList<Visita> Filtrar(string numeroOS, string numeroPedido, DateTime dataDe, DateTime dataAte, int idCliente, int idResponsavel, string status, int idTipoVisita, string descricao, Permissao permissao, int idUsuarioLogado, int ordenacao)
        {
            return Visita.Filtrar(numeroOS, numeroPedido, dataDe, dataAte, idCliente, idResponsavel, status, idTipoVisita, descricao, 0, permissao, idUsuarioLogado, ordenacao);
        }

        public static IList<Visita> Filtrar(string numeroOS, string numeroPedido, DateTime dataDe, DateTime dataAte, int idCliente, int idResponsavel, string status, int idTipoVisita, string descricao, int idDepartamento, Permissao permissao, int idUsuarioLogado, int ordenacao)
        {
            if (permissao == null)
                return null;

            Visita visita = new Visita();
            visita.AdicionarFiltro(Filtros.Distinct());
            if (ordenacao == 0)
                visita.AdicionarFiltro(Filtros.SetOrderDesc("DataInicio"));
            else visita.AdicionarFiltro(Filtros.SetOrderAsc("DataInicio"));
            visita.AdicionarFiltro(Filtros.CriarAlias("OrdemServico", "ord"));
            if (!string.IsNullOrEmpty(numeroOS))
                visita.AdicionarFiltro(Filtros.Like("ord.Codigo", numeroOS));

            if (permissao != null && permissao.AcessaOS == Permissao.SETOR)
            {
                Funcao funcaoSetor = permissao.Funcao != null && permissao.Funcao.Setor != null ? permissao.Funcao : null;

                if (funcaoSetor == null)
                    return null;

                visita.AdicionarFiltro(Filtros.CriarAlias("ord.Setor", "set"));
                visita.AdicionarFiltro(Filtros.Eq("set.Id", funcaoSetor.Setor.Id));
            }

            if (permissao != null && permissao.AcessaOS == Permissao.DEPARTAMENTO)
            {
                Funcao funcaoDepartamento = permissao.Funcao != null && permissao.Funcao.Setor != null && permissao.Funcao.Setor.Departamento != null ? permissao.Funcao : null;

                if (funcaoDepartamento == null)
                    return null;

                visita.AdicionarFiltro(Filtros.CriarAlias("ord.Setor", "set"));
                visita.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                visita.AdicionarFiltro(Filtros.Eq("dept.Id", funcaoDepartamento.Setor.Departamento.Id));
            }

            visita.AdicionarFiltro(Filtros.CriarAlias("Visitante", "resp"));

            //Se o usuário não possui permissão para acessar nenhuma OS trazer somente as OS que ele for responsavel
            if (permissao != null && permissao.AcessaOS == Permissao.NENHUMA)
                visita.AdicionarFiltro(Filtros.Eq("resp.Id", idUsuarioLogado));
            else
                if (idResponsavel > 0)
                    visita.AdicionarFiltro(Filtros.Eq("resp.Id", idResponsavel));

            visita.AdicionarFiltro(Filtros.CriarAlias("ord.Pedido", "ped"));

            if (!string.IsNullOrEmpty(numeroPedido))
                visita.AdicionarFiltro(Filtros.Like("ped.Codigo", numeroPedido));

            if (!string.IsNullOrEmpty(descricao))
                visita.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            visita.AdicionarFiltro(Filtros.MaiorOuIgual("DataInicio", dataDe));
            visita.AdicionarFiltro(Filtros.MenorOuIgual("DataFim", dataAte));
            visita.AdicionarFiltro(Filtros.SetOrderDesc("DataInicio"));

            if (idTipoVisita > 0)
            {
                visita.AdicionarFiltro(Filtros.CriarAlias("TipoVisita", "tipVisit"));
                visita.AdicionarFiltro(Filtros.Eq("tipVisit.Id", idTipoVisita));
            }

            if (idCliente > 0)
            {
                visita.AdicionarFiltro(Filtros.CriarAlias("ped.Cliente", "cli"));
                visita.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
            }

            if (status != "N")
                visita.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            if (idDepartamento > 0)
            {
                visita.AdicionarFiltro(Filtros.CriarAlias("ord.Setor", "set"));
                visita.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dep"));
                visita.AdicionarFiltro(Filtros.Eq("dep.Id", idDepartamento));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Visita>(visita);
        }

        public static IList<Visita> FiltrarVisitasDoUsuarioLogadoComMesmosFiltros(string numeroOS, string numeroPedido, DateTime dataDe, DateTime dataAte, int idCliente, string status, int idTipoVisita, string descricao, int idUsuarioLogado, int ordenacao)
        {
            Visita visita = new Visita();
            visita.AdicionarFiltro(Filtros.Distinct());

            visita.AdicionarFiltro(Filtros.CriarAlias("OrdemServico", "ord"));

            if (!string.IsNullOrEmpty(numeroOS))
                visita.AdicionarFiltro(Filtros.Like("ord.Codigo", numeroOS));

            visita.AdicionarFiltro(Filtros.CriarAlias("Visitante", "resp"));
            visita.AdicionarFiltro(Filtros.Eq("resp.Id", idUsuarioLogado));

            visita.AdicionarFiltro(Filtros.CriarAlias("ord.Pedido", "ped"));

            if (!string.IsNullOrEmpty(numeroPedido))
                visita.AdicionarFiltro(Filtros.Like("ped.Codigo", numeroPedido));

            if (!string.IsNullOrEmpty(descricao))
                visita.AdicionarFiltro(Filtros.Like("Descricao", descricao));


            visita.AdicionarFiltro(Filtros.MaiorOuIgual("DataInicio", dataDe));
            visita.AdicionarFiltro(Filtros.MenorOuIgual("DataFim", dataAte));

            if (ordenacao == 0)
                visita.AdicionarFiltro(Filtros.SetOrderAsc("DataInicio"));
            else
                visita.AdicionarFiltro(Filtros.SetOrderDesc("DataInicio"));

            if (idTipoVisita > 0)
            {
                visita.AdicionarFiltro(Filtros.CriarAlias("TipoVisita", "tipVisit"));
                visita.AdicionarFiltro(Filtros.Eq("tipVisit.Id", idTipoVisita));
            }

            if (idCliente > 0)
            {
                visita.AdicionarFiltro(Filtros.CriarAlias("ped.Cliente", "cli"));
                visita.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
            }

            if (status != "N")
                visita.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Visita>(visita);
        }

        public static IList<Visita> ConsultarVisitasNosProximosDias(int qtdDiasVisita)
        {
            Visita visita = new Visita();
            visita.AdicionarFiltro(Filtros.Distinct());

            visita.AdicionarFiltro(Filtros.Eq("Ativo", true));

            DateTime datade = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime dataAte = datade.AddDays(qtdDiasVisita);

            visita.AdicionarFiltro(Filtros.Between("DataInicio", datade, dataAte));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Visita>(visita);
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

        public virtual string GetDadosDoVisitante
        {
            get
            {
                return this.Visitante != null ? this.Visitante.NomeRazaoSocial.ToString() + ", Telefone: " + this.Visitante.CelularCorporativo + ", E-mail: " + this.Visitante.EmailCorporativo : "";
            }
        }

        public virtual String GetDescricaoDepartamento
        {
            get
            {
                return this.OrdemServico != null && this.OrdemServico.Setor != null ?
                    this.OrdemServico.Setor.Departamento.Nome : "";
            }
        }
    }
}
