using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using Persistencia.Fabrica;
using Persistencia.Filtros;


namespace Modelo
{
    public partial class OrdemServico : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static OrdemServico ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrdemServico classe = new OrdemServico();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<OrdemServico>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual OrdemServico ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<OrdemServico>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual OrdemServico Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<OrdemServico>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual OrdemServico SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<OrdemServico>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrdemServico> SalvarTodos(IList<OrdemServico> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrdemServico>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<OrdemServico> SalvarTodos(params OrdemServico[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<OrdemServico>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<OrdemServico>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<OrdemServico>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<OrdemServico> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrdemServico obj = Activator.CreateInstance<OrdemServico>();
            return fabrica.GetDAOBase().ConsultarTodos<OrdemServico>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrdemServico> ConsultarOrdemAcendente(int qtd)
        {
            OrdemServico ee = new OrdemServico();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Numero"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<OrdemServico> ConsultarOrdemDescendente(int qtd)
        {
            OrdemServico ee = new OrdemServico();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Numero"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de OrdemServico
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<OrdemServico> Filtrar(int qtd)
        {
            OrdemServico estado = new OrdemServico();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(estado);
        }

        /// <summary>
        /// Retorna o ultimo OrdemServico Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo OrdemServico</returns>
        public virtual OrdemServico UltimoInserido()
        {
            OrdemServico estado = new OrdemServico();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<OrdemServico>(estado);
        }

        /// <summary>
        /// Consulta todos os OrdemServicos armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os OrdemServicos armazenados ordenados pelo Nome</returns>
        public static IList<OrdemServico> ConsultarTodosOrdemAlfabetica()
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.SetOrderDesc("Data"));
            ordem.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);
        }

        public static string GerarNumeroOS()
        {
            OrdemServico aux = new OrdemServico();
            aux.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrdemServico os = fabrica.GetDAOBase().ConsultarUnicoComFiltro<OrdemServico>(aux);
            if (os == null)
            {
                return "OS1";
            }
            else
            {
                return "OS" + (os.Id + 1);
            }
        }

        public virtual string GetNomesCorresponsaveis
        {
            get
            {
                string retorno = "";

                if (this.CoResponsaveis != null && this.CoResponsaveis.Count > 0)
                {
                    foreach (Funcionario item in this.CoResponsaveis)
                    {
                        if (item == this.CoResponsaveis[this.CoResponsaveis.Count - 1])
                            retorno += item.NomeRazaoSocial;
                        else
                            retorno += item.NomeRazaoSocial + "; ";
                    }
                }
                return retorno;
            }
        }

        public virtual string GetDescricaoDepartamento
        {
            get
            {
                return this.Setor != null && this.Setor.Departamento != null ? this.Setor.Departamento.Nome : "";
            }
        }

        public virtual string GetDescricaoSetor
        {
            get
            {
                return this.Setor != null ? this.Setor.Nome : "";
            }
        }

        public virtual string GetResponsavel
        {
            get
            {
                return this.Responsavel != null ? this.Responsavel.NomeRazaoSocial : "";
            }
        }

        public virtual string GetTipoOS
        {
            get
            {
                return this.Tipo != null ? this.Tipo.Nome : "";
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

        public virtual Detalhamento GetUltimaObservacao
        {
            get
            {
                if (this.Observacoes != null && this.Observacoes.Count > 0)
                    return this.Observacoes[this.Observacoes.Count - 1];
                else
                    return null;
            }
        }

        public static string GerarNumeroCodigo()
        {
            OrdemServico aux = new OrdemServico();
            aux.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            OrdemServico ordem = fabrica.GetDAOBase().ConsultarUnicoComFiltro<OrdemServico>(aux);
            if (ordem == null)
            {
                return "1";
            }
            else
            {
                return (Convert.ToInt32(ordem.Codigo) + 1).ToString();
            }
        }

        /// <summary>
        /// Filtrar OSs
        /// </summary>
        /// <param name="codigo">O código da OS</param>
        /// <param name="numeroPedido"></param>
        /// <param name="pesquisarEOrdenarPor">0 - Data de Criação; 1 - Data de Vencimento; 2 - Data de Encerramento</param>
        /// <param name="dataDe"></param>
        /// <param name="dataAte"></param>
        /// <param name="idCliente"></param>
        /// <param name="idResponsavel"></param>
        /// <param name="status"></param>
        /// <param name="descricao"></param>
        /// <param name="permissao"></param>
        /// <param name="idUsuarioLogado"></param>
        /// <param name="estadoDaOS"></param>
        /// <param name="idTipoOS"></param>
        /// <param name="unidade"></param>
        /// <returns></returns>
        public static IList<OrdemServico> Filtrar(string codigo, string numeroPedido, int pesquisarEOrdenarPor, DateTime dataDe, DateTime dataAte, int idCliente,
            int idResponsavel, string status, string descricao, Permissao permissao, int idUsuarioLogado, string estadoDaOS, int idTipoOS, int ordenacao, int idDepartamento,
            int faturadas)
        {
            if (permissao == null)
                return null;

            OrdemServico ordem = new OrdemServico();
            ordem.MultiEmpresa = false;
            ordem.AdicionarFiltro(Filtros.Distinct());
            //ordem.Setor.Departamento

            if (!string.IsNullOrEmpty(codigo))
                ordem.AdicionarFiltro(Filtros.Like("Codigo", codigo));

            if (!string.IsNullOrEmpty(descricao))
                ordem.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            if (idTipoOS > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Tipo", "tipOS"));
                ordem.AdicionarFiltro(Filtros.Eq("tipOS.Id", idTipoOS));
            }

            ordem.AdicionarFiltro(Filtros.CriarAlias("Pedido", "ped"));
            ordem.AdicionarFiltro(Filtros.Like("ped.Codigo", numeroPedido));

            if (idCliente > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("ped.Cliente", "cli"));
                ordem.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
            }

            if (faturadas > 0)
                ordem.AdicionarFiltro(Filtros.Eq("SemCusto", faturadas == 2));

            ordem.AdicionarFiltro(Filtros.CriarAlias("Responsavel", "resp"));

            //Se o usuário não possui permissão para acessar nenhuma OS trazer somente as OS que ele for responsavel
            if (permissao != null && permissao.AcessaOS == Permissao.NENHUMA)
                ordem.AdicionarFiltro(Filtros.Eq("resp.Id", idUsuarioLogado));
            else
            {
                if (idResponsavel > 0)
                    ordem.AdicionarFiltro(Filtros.Eq("resp.Id", idResponsavel));
            }

            if (permissao != null || idDepartamento > 0)
                ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));

            if (permissao != null)
            {
                if (permissao.AcessaOS == Permissao.SETOR)
                {
                    Funcao funcaoSetor = permissao.Funcao != null && permissao.Funcao.Setor != null ? permissao.Funcao : null;
                    if (funcaoSetor == null)
                        return null;
                    ordem.AdicionarFiltro(Filtros.Eq("set.Id", funcaoSetor.Setor.Id));
                }
                else if (permissao.AcessaOS == Permissao.DEPARTAMENTO && idDepartamento == 0)
                {
                    Funcao funcaoDepartamento = permissao.Funcao != null && permissao.Funcao.Setor != null && permissao.Funcao.Setor.Departamento != null ? permissao.Funcao : null;
                    if (funcaoDepartamento == null)
                        return null;
                    ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                    ordem.AdicionarFiltro(Filtros.Eq("dept.Id", funcaoDepartamento.Setor.Departamento.Id));
                }
            }

            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            if (status != "N")
                ordem.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            //Todos
            //B = Aberta
            //AbertaEVencida
            //A = Aprovada
            //R = Reprovada
            //Encerrada
            //ComPedidoAdiamento
            //ComPedidoAdiamentoSemParecer
            //P = PendentesAprovacao
            //Seguindo os chars da classe ordemServico
            if (estadoDaOS != "Todos" && !string.IsNullOrEmpty(estadoDaOS))
            {
                if (estadoDaOS.Equals("Encerrada"))
                    ordem.AdicionarFiltro(Filtros.Maior("DataEncerramento", SqlDate.MinValue));
                else if (estadoDaOS.Equals("ComPedidoAdiamento") || estadoDaOS.Equals("EncerradaComPedido") || estadoDaOS.Equals("ComPedidoAdiamentoSemParecer"))
                {
                    ordem.AdicionarFiltro(Filtros.CriarAlias("SolicitacoesAdiamento", "solisAd"));
                    if (estadoDaOS.Equals("ComPedidoAdiamentoSemParecer"))
                    {
                        //Solicitação nem aceita e nem negada
                        ordem.AdicionarFiltro(Filtros.NaoIgual("solisAd.Parecer", SolicitacaoAdiamento.ACEITA));
                        ordem.AdicionarFiltro(Filtros.NaoIgual("solisAd.Parecer", SolicitacaoAdiamento.NEGADA));
                    }
                }
                else if (estadoDaOS.Equals("AbertaEVencida"))
                    ordem.AdicionarFiltro(Filtros.Eq("Status", OrdemServico.ABERTA));
                else if (estadoDaOS.Length == 1)
                {
                    ordem.AdicionarFiltro(Filtros.Eq("Status", Convert.ToChar(estadoDaOS)));
                    ordem.AdicionarFiltro(Filtros.MenorOuIgual("DataEncerramento", SqlDate.MinValue));
                }
            }

            if (pesquisarEOrdenarPor == 0)
            {
                ordem.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));
                if (ordenacao == 0)
                    ordem.AdicionarFiltro(Filtros.SetOrderAsc("Data"));
                else
                    ordem.AdicionarFiltro(Filtros.SetOrderDesc("Data"));
            }

            IList<OrdemServico> ordens = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);
            //Estados Complexos
            //EncerradaComPedido
            //EncerradaSemPedido
            //Vencida
            //EncerradaComPedidoAposVencimento
            //Abertas e Vencidas
            if (estadoDaOS.Equals("EncerradaComPedido"))
                ordens = ordens.Where(x => x.IsEncerradaNoPrazoComPedidoAdiamento).ToList();
            else if (estadoDaOS.Equals("EncerradaSemPedido"))
                ordens = ordens.Where(x => x.IsEncerradaNoPrazoSemPedidoAdiamento).ToList();
            else if (estadoDaOS.Equals("Vencida"))
                ordens = ordens.Where(x => x.IsVencida).ToList();
            else if (estadoDaOS.Equals("EncerradaComPedidoAposVencimento"))
                ordens = ordens.Where(x => x.IsEncerradaNoPrazoComPedidoAdiamentoAposVencimento).ToList();
            else if (estadoDaOS.Equals("AbertaEVencida"))
                ordens = ordens.Where(aux => aux.IsVencida).ToList();

            //Data de Vencimento
            if (pesquisarEOrdenarPor == 1)
            {
                if (ordenacao == 0)
                    ordens = ordens.Where(aux => aux.GetDataVencimento.CompareTo(dataDe) >= 0 && aux.GetDataVencimento.CompareTo(dataAte) <= 0).OrderBy(aux => aux.GetDataVencimento).ToList();
                else
                    ordens = ordens.Where(aux => aux.GetDataVencimento.CompareTo(dataDe) >= 0 && aux.GetDataVencimento.CompareTo(dataAte) <= 0).OrderByDescending(aux => aux.GetDataVencimento).ToList();
            }
            //Data de Encerramento
            else if (pesquisarEOrdenarPor == 2)
            {
                if (ordenacao == 0)
                    ordens = ordens.Where(aux => aux.GetDataEncerramento.CompareTo(dataDe) >= 0 && aux.GetDataEncerramento.CompareTo(dataAte) <= 0).OrderBy(aux => aux.GetDataEncerramento).ToList();
                else
                    ordens = ordens.Where(aux => aux.GetDataEncerramento.CompareTo(dataDe) >= 0 && aux.GetDataEncerramento.CompareTo(dataAte) <= 0).OrderByDescending(aux => aux.GetDataEncerramento).ToList();
            }
            return ordens;
        }

        public virtual string GetNumeroPedido
        {
            get
            {
                return this.Pedido != null ? this.Pedido.Codigo : "";
            }
        }

        public virtual Cliente GetCliente
        {
            get
            {
                return this.Pedido != null && this.Pedido.Cliente != null ? this.Pedido.Cliente : null;
            }
        }

        public virtual string GetNomeCliente
        {
            get
            {
                return this.Pedido != null && this.Pedido.Cliente != null ? this.Pedido.Cliente.NomeRazaoSocial : "";
            }
        }

        public virtual string GetEmailCliente
        {
            get
            {
                Cliente aux = this.GetCliente;
                return (aux != null ? aux.Email : "");
            }
        }

        public virtual string GetUnidade
        {
            get
            {
                return this.Setor != null ? this.Setor.Departamento.GetUnidade : "";
            }
        }

        public virtual string GetNomeResponsavel
        {
            get
            {
                return this.Responsavel != null ? this.Responsavel.NomeRazaoSocial : "";
            }
        }

        public virtual string GetDescricaoDoStatusDaOS
        {
            get
            {
                if (this.DataEncerramento != SqlDate.MinValue)
                    return "Encerrada";

                switch (this.Status)
                {
                    case OrdemServico.ABERTA:
                        return "Aberta";

                    case OrdemServico.APROVADA:
                        return "Aprovada";

                    case OrdemServico.REPROVADA:
                        return "Reprovada";

                    case OrdemServico.PENDENTEAPROVACAO:
                        return "Pendente de Aprovação";

                    default:
                        return "";
                }
            }
        }

        public static IList<OrdemServico> ConsultarOdensQueSeraoRenovadasAutomaticamente()
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            ordem.AdicionarFiltro(Filtros.Menor("Data", DateTime.Now));

            ordem.AdicionarFiltro(Filtros.Eq("JaRenovada", false));
            ordem.AdicionarFiltro(Filtros.Eq("Renovavel", true));

            ordem.AdicionarFiltro(Filtros.MenorOuIgual("DataEncerramento", SqlDate.MinValue));

            ordem.AdicionarFiltro(Filtros.Eq("Ativo", true));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);
        }

        public static IList<OrdemServico> ConsultarOrdensComVencimentoNosProximosDias(int qtdDiasAVencer)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            ordem.AdicionarFiltro(Filtros.Eq("Ativo", true));

            DateTime datade = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime dataAte = datade.AddDays(qtdDiasAVencer);

            ordem.AdicionarFiltro(Filtros.MenorOuIgual("DataEncerramento", SqlDate.MinValue));

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Between("PrazoPadrao", datade, dataAte);
            filtros[1] = Filtros.Between("PrazoLegal", datade, dataAte);
            filtros[2] = Filtros.Between("PrazoDiretoria", datade, dataAte);
            ordem.AdicionarFiltro(Filtros.Ou(filtros));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();

            IList<OrdemServico> ordensAux = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);

            if (ordensAux != null && ordensAux.Count > 0)
            {
                IList<OrdemServico> ordensQueVencemNoPeriodo = ordensAux.Where(x => x.GetDataVencimento >= datade && x.GetDataVencimento <= dataAte).ToList();

                return ordensQueVencemNoPeriodo != null && ordensQueVencemNoPeriodo.Count > 0 ? ordensQueVencemNoPeriodo : new List<OrdemServico>();
            }

            return new List<OrdemServico>();
        }

        public static IList<OrdemServico> ConsultarOrdensVencidasOntemAindaAbertas()
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            ordem.AdicionarFiltro(Filtros.Eq("Ativo", true));

            DateTime datade = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            datade = datade.AddDays(-1);
            DateTime dataAte = new DateTime(datade.Year, datade.Month, datade.Day, 23, 59, 59); ;

            ordem.AdicionarFiltro(Filtros.MenorOuIgual("DataEncerramento", SqlDate.MinValue));

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Between("PrazoPadrao", datade, dataAte);
            filtros[1] = Filtros.Between("PrazoLegal", datade, dataAte);
            filtros[2] = Filtros.Between("PrazoDiretoria", datade, dataAte);
            ordem.AdicionarFiltro(Filtros.Ou(filtros));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrdemServico> ordensAux = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);


            if (ordensAux != null && ordensAux.Count > 0)
            {
                IList<OrdemServico> ordensQueVencemNoPeriodo = ordensAux.Where(x => x.GetDataVencimento >= datade && x.GetDataVencimento <= dataAte).ToList();

                return ordensQueVencemNoPeriodo != null && ordensQueVencemNoPeriodo.Count > 0 ? ordensQueVencemNoPeriodo : new List<OrdemServico>();
            }

            return new List<OrdemServico>();

        }

        public static IList<OrdemServico> FiltrarOrdensDoUsuarioLogadoComMesmosFiltros(string codigo, string numeroPedido, int pesquisarEOrdenarPor, DateTime dataDe, DateTime dataAte,
            int idCliente, string status, string descricao, int idUsuarioLogado, string estadoDaOS, int idTipoOS, int ordenacao, int idDepartamento,
            int faturadas)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            if (!string.IsNullOrEmpty(codigo))
                ordem.AdicionarFiltro(Filtros.Like("Codigo", codigo));

            if (!string.IsNullOrEmpty(descricao))
                ordem.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            if (idTipoOS > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Tipo", "tipOS"));
                ordem.AdicionarFiltro(Filtros.Eq("tipOS.Id", idTipoOS));
            }

            ordem.AdicionarFiltro(Filtros.CriarAlias("Pedido", "ped"));
            ordem.AdicionarFiltro(Filtros.Like("ped.Codigo", numeroPedido));

            if (idCliente > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("ped.Cliente", "cli"));
                ordem.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
            }

            ordem.AdicionarFiltro(Filtros.CriarAlias("Responsavel", "resp"));
            ordem.AdicionarFiltro(Filtros.Eq("resp.Id", idUsuarioLogado));

            if (status != "N")
                ordem.AdicionarFiltro(Filtros.Eq("Ativo", status == "T"));

            if (faturadas > 0)
                ordem.AdicionarFiltro(Filtros.Eq("SemCusto", faturadas == 2));

            //Todos
            //B = Aberta
            //AbertaEVencida
            //A = Aprovada
            //R = Reprovada
            //Encerrada
            //ComPedidoAdiamento
            //P = PendentesAprovacao
            //EncerradaComPedido
            //EncerradaSemPedido
            //Vencida
            //EncerradaComPedidoAposVencimento
            //Seguindo os chars da classe ordemServico
            if (estadoDaOS != "Todos" && !string.IsNullOrEmpty(estadoDaOS))
            {
                if (estadoDaOS.Equals("Encerrada"))
                    ordem.AdicionarFiltro(Filtros.Maior("DataEncerramento", SqlDate.MinValue));
                else if (estadoDaOS.Equals("ComPedidoAdiamento") || estadoDaOS.Equals("EncerradaComPedido") || estadoDaOS.Equals("ComPedidoAdiamentoSemParecer"))
                {
                    ordem.AdicionarFiltro(Filtros.CriarAlias("SolicitacoesAdiamento", "solisAd"));
                    if (estadoDaOS.Equals("ComPedidoAdiamentoSemParecer"))
                    {
                        //Solicitação nem aceita e nem negada
                        ordem.AdicionarFiltro(Filtros.NaoIgual("solisAd.Parecer", SolicitacaoAdiamento.ACEITA));
                        ordem.AdicionarFiltro(Filtros.NaoIgual("solisAd.Parecer", SolicitacaoAdiamento.NEGADA));
                    }
                }
                else if (estadoDaOS.Equals("AbertaEVencida"))
                    ordem.AdicionarFiltro(Filtros.Eq("Status", OrdemServico.ABERTA));
                else if (estadoDaOS.Length == 1)
                {
                    ordem.AdicionarFiltro(Filtros.Eq("Status", Convert.ToChar(estadoDaOS)));
                    ordem.AdicionarFiltro(Filtros.MenorOuIgual("DataEncerramento", SqlDate.MinValue));
                }
            }

            if (pesquisarEOrdenarPor == 0)
            {
                ordem.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));
                if (ordenacao == 0)
                    ordem.AdicionarFiltro(Filtros.SetOrderAsc("Data"));
                else
                    ordem.AdicionarFiltro(Filtros.SetOrderDesc("Data"));
            }

            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }
            IList<OrdemServico> ordens = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);

            //Estados Complexos
            //EncerradaComPedido
            //EncerradaSemPedido
            //Vencida
            //EncerradaComPedidoAposVencimento
            if (estadoDaOS.Equals("EncerradaComPedido"))
                ordens = ordens.Where(x => x.IsEncerradaNoPrazoComPedidoAdiamento).ToList();
            else if (estadoDaOS.Equals("EncerradaSemPedido"))
                ordens = ordens.Where(x => x.IsEncerradaNoPrazoSemPedidoAdiamento).ToList();
            else if (estadoDaOS.Equals("Vencida"))
                ordens = ordens.Where(x => x.IsVencida).ToList();
            else if (estadoDaOS.Equals("EncerradaComPedidoAposVencimento"))
                ordens = ordens.Where(x => x.IsEncerradaNoPrazoComPedidoAdiamentoAposVencimento).ToList();
            else if (estadoDaOS.Equals("AbertaEVencida"))
                ordens = ordens.Where(aux => aux.IsVencida).ToList();

            //Data de Vencimento
            if (pesquisarEOrdenarPor == 1)
            {
                if (ordenacao == 0)
                    ordens = ordens.Where(aux => aux.GetDataVencimento.CompareTo(dataDe) >= 0 && aux.GetDataVencimento.CompareTo(dataAte) <= 0).OrderBy(aux => aux.GetDataVencimento).ToList();
                else
                    ordens = ordens.Where(aux => aux.GetDataVencimento.CompareTo(dataDe) >= 0 && aux.GetDataVencimento.CompareTo(dataAte) <= 0).OrderByDescending(aux => aux.GetDataVencimento).ToList();
            }
            //Data de Encerramento
            else if (pesquisarEOrdenarPor == 2)
            {
                if (ordenacao == 0)
                    ordens = ordens.Where(aux => aux.GetDataEncerramento.CompareTo(dataDe) >= 0 && aux.GetDataEncerramento.CompareTo(dataAte) <= 0).OrderBy(aux => aux.GetDataEncerramento).ToList();
                else
                    ordens = ordens.Where(aux => aux.GetDataEncerramento.CompareTo(dataDe) >= 0 && aux.GetDataEncerramento.CompareTo(dataAte) <= 0).OrderByDescending(aux => aux.GetDataEncerramento).ToList();
            }
            return ordens;
        }

        public virtual DateTime GetDataVencimento
        {
            get
            {
                DateTime retorno;
                if (this.PrazoDiretoria == SqlDate.MinValue && this.PrazoLegal != SqlDate.MinValue)
                    retorno = this.PrazoLegal;
                else if (this.PrazoDiretoria != SqlDate.MinValue)
                    retorno = this.PrazoDiretoria;
                else
                    retorno = this.PrazoPadrao;
                return Convert.ToDateTime(retorno.ToShortDateString() + " 23:59:59");
            }
        }

        public virtual String GetDescricaoPrazoVencimento
        {
            get
            {
                if (this.PrazoDiretoria == SqlDate.MinValue && this.PrazoLegal != SqlDate.MinValue)
                    return "Prazo Legal";

                if (this.PrazoDiretoria != SqlDate.MinValue)
                    return "Prazo de Diretoria";

                return "Prazo Padrão";
            }
        }

        public virtual String GetDescricaoFaturada
        {
            get
            {
                return this.SemCusto ? "Não" : "Sim";
            }
        }

        public virtual string GetPrazoPadrao
        {
            get
            {
                return this.PrazoPadrao != SqlDate.MinValue ? this.PrazoPadrao.ToShortDateString() : "-";
            }
        }

        public virtual string GetOrgao
        {
            get
            {
                return this.Orgao != null ? this.Orgao.Nome : "";
            }
        }

        public virtual string GetDataProtocolo
        {
            get
            {
                return this.DataProtocoloEncerramento != SqlDate.MinValue ? this.DataProtocoloEncerramento.ToShortDateString() : "";
            }
        }

        public virtual string GetPrazoLegal
        {
            get
            {
                return this.PrazoLegal != SqlDate.MinValue ? this.PrazoLegal.ToShortDateString() : "-";
            }
        }

        public virtual string GetPrazoDiretoria
        {
            get
            {
                return this.PrazoDiretoria != SqlDate.MinValue ? this.PrazoDiretoria.ToShortDateString() : "-";
            }
        }

        public virtual string GetNumerosOSsVinculadas
        {
            get
            {
                string retorno = "";

                if (this.OssVinculadas != null && this.OssVinculadas.Count > 0)
                {
                    foreach (OrdemServico ordem in this.OssVinculadas)
                    {
                        if (ordem == this.OssVinculadas[this.OssVinculadas.Count - 1])
                            retorno += ordem.Codigo;
                        else
                            retorno += ordem.Codigo + "; ";
                    }
                }

                return retorno;
            }
        }

        public static int ObterQuantidadeDeOSNoPeriodo(DateTime dataDe, DateTime dataAte, int filtrarDataPor, int faturadas)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            if (filtrarDataPor > 0)
            {
                Filtro[] filtros = new Filtro[3];

                filtros[0] = Filtros.Between("PrazoPadrao", dataDe, dataAte);
                filtros[1] = Filtros.Between("PrazoLegal", dataDe, dataAte);
                filtros[2] = Filtros.Between("PrazoDiretoria", dataDe, dataAte);
                ordem.AdicionarFiltro(Filtros.Ou(filtros));
            }
            else
                ordem.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            if (faturadas > 0)
                ordem.AdicionarFiltro(Filtros.Eq("SemCusto", faturadas == 2));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrdemServico> ordensAux = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);

            if (filtrarDataPor > 0)
            {
                if (ordensAux != null && ordensAux.Count > 0)
                {
                    IList<OrdemServico> ordensQueVencemNoPeriodo = ordensAux.Where(x => x.GetDataVencimento >= dataDe && x.GetDataVencimento <= dataAte).ToList();
                    return ordensQueVencemNoPeriodo != null && ordensQueVencemNoPeriodo.Count > 0 ? ordensQueVencemNoPeriodo.Count : 0;
                }
                return 0;
            }

            return ordensAux != null && ordensAux.Count > 0 ? ordensAux.Count : 0;
        }

        public static int ObterQuantidadeDeOSDoDepartamentoEDoSetorNoPeriodo(DateTime dataDe, DateTime dataAte, int idDepartamento, int idSetor, int filtrarDataPor, int faturadas)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            if (filtrarDataPor > 0)
            {
                Filtro[] filtros = new Filtro[3];

                filtros[0] = Filtros.Between("PrazoPadrao", dataDe, dataAte);
                filtros[1] = Filtros.Between("PrazoLegal", dataDe, dataAte);
                filtros[2] = Filtros.Between("PrazoDiretoria", dataDe, dataAte);
                ordem.AdicionarFiltro(Filtros.Ou(filtros));
            }
            else
                ordem.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));

            if (idSetor > 0)
                ordem.AdicionarFiltro(Filtros.Eq("set.Id", idSetor));

            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            if (faturadas > 0)
                ordem.AdicionarFiltro(Filtros.Eq("SemCusto", faturadas == 2));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrdemServico> ordensAux = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);

            if (filtrarDataPor > 0)
            {
                if (ordensAux != null && ordensAux.Count > 0)
                {
                    IList<OrdemServico> ordensQueVencemNoPeriodo = ordensAux.Where(x => x.GetDataVencimento >= dataDe && x.GetDataVencimento <= dataAte).ToList();
                    return ordensQueVencemNoPeriodo != null && ordensQueVencemNoPeriodo.Count > 0 ? ordensQueVencemNoPeriodo.Count : 0;
                }
                return 0;
            }

            return ordensAux != null && ordensAux.Count > 0 ? ordensAux.Count : 0;
        }

        public static int ObterQuantidadeDeOSDoDepartamentoEDoTipoOSNoPeriodo(DateTime dataDe, DateTime dataAte, int idDepartamento, int idTipoOS, int idSetor, int filtrarDataPor, int faturadas)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            if (filtrarDataPor > 0)
            {
                Filtro[] filtros = new Filtro[3];

                filtros[0] = Filtros.Between("PrazoPadrao", dataDe, dataAte);
                filtros[1] = Filtros.Between("PrazoLegal", dataDe, dataAte);
                filtros[2] = Filtros.Between("PrazoDiretoria", dataDe, dataAte);
                ordem.AdicionarFiltro(Filtros.Ou(filtros));
            }
            else
                ordem.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));

            if (idSetor > 0)
                ordem.AdicionarFiltro(Filtros.Eq("set.Id", idSetor));

            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            if (idTipoOS > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Tipo", "tipoOS"));
                ordem.AdicionarFiltro(Filtros.Eq("tipoOS.Id", idTipoOS));
            }

            if (faturadas > 0)
                ordem.AdicionarFiltro(Filtros.Eq("SemCusto", faturadas == 2));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrdemServico> ordensAux = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);

            if (filtrarDataPor > 0)
            {
                if (ordensAux != null && ordensAux.Count > 0)
                {
                    IList<OrdemServico> ordensQueVencemNoPeriodo = ordensAux.Where(x => x.GetDataVencimento >= dataDe && x.GetDataVencimento <= dataAte).ToList();
                    return ordensQueVencemNoPeriodo != null && ordensQueVencemNoPeriodo.Count > 0 ? ordensQueVencemNoPeriodo.Count : 0;
                }
                return 0;
            }

            return ordensAux != null && ordensAux.Count > 0 ? ordensAux.Count : 0;
        }

        public static IList<OrdemServico> ConsultarOrdensComVencimentoNoPeriodoDoDepartamento(DateTime dataDe, DateTime dataAte, int idDepartamento, int idSetor)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Eq("Ativo", true));
            ordem.AdicionarFiltro(Filtros.Distinct());

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Between("PrazoPadrao", dataDe, dataAte);
            filtros[1] = Filtros.Between("PrazoLegal", dataDe, dataAte);
            filtros[2] = Filtros.Between("PrazoDiretoria", dataDe, dataAte);
            ordem.AdicionarFiltro(Filtros.Ou(filtros));

            ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
            if (idSetor > 0)
                ordem.AdicionarFiltro(Filtros.Eq("set.Id", idSetor));

            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);
        }

        public static int ObterQuantidadeDeOSConcluidasNoPeriodo(DateTime dataDe, DateTime dataAte, int idDepartamento, int faturadas)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            ordem.AdicionarFiltro(Filtros.Between("DataEncerramento", dataDe, dataAte));

            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            if (faturadas > 0)
                ordem.AdicionarFiltro(Filtros.Eq("SemCusto", faturadas == 2));

            ordem.AdicionarFiltro(Filtros.Count("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            object o = fabrica.GetDAOBase().ConsultarProjecao(ordem);

            if (o != null)
            {
                return Convert.ToInt32(o.ToString());
            }

            return 0;
        }

        public static int ObterQuantidadeDeOSConcluidasEDoTipoOSNoPeriodo(DateTime dataDe, DateTime dataAte, int idTipoOS, int idDepartamento, int faturadas)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            ordem.AdicionarFiltro(Filtros.Between("DataEncerramento", dataDe, dataAte));

            if (idTipoOS > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Tipo", "tipoOS"));
                ordem.AdicionarFiltro(Filtros.Eq("tipoOS.Id", idTipoOS));
            }

            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            if (faturadas > 0)
                ordem.AdicionarFiltro(Filtros.Eq("SemCusto", faturadas == 2));

            ordem.AdicionarFiltro(Filtros.Count("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            object o = fabrica.GetDAOBase().ConsultarProjecao(ordem);

            if (o != null)
            {
                return Convert.ToInt32(o.ToString());
            }

            return 0;
        }

        public static int ObterQuantidadeDeOSDoFuncionarioNoDepartamentoNoPeriodo(DateTime dataDe, DateTime dataAte, int idFuncionario, int idDepartamento)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Between("PrazoPadrao", dataDe, dataAte);
            filtros[1] = Filtros.Between("PrazoLegal", dataDe, dataAte);
            filtros[2] = Filtros.Between("PrazoDiretoria", dataDe, dataAte);
            ordem.AdicionarFiltro(Filtros.Ou(filtros));


            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            ordem.AdicionarFiltro(Filtros.CriarAlias("Responsavel", "resp"));
            ordem.AdicionarFiltro(Filtros.Eq("resp.Id", idFuncionario));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrdemServico> retorno = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);

            if (retorno != null && retorno.Count > 0)
            {
                IList<OrdemServico> retornoAux = retorno.Where(x => x.GetDataVencimento >= dataDe && x.GetDataVencimento <= dataAte).ToList();
                return retornoAux != null && retornoAux.Count > 0 ? retornoAux.Count : 0;
            }


            return 0;
        }

        public static int ObterQuantidadeDeOSConcluidasDoFuncionarioNoDepartamentoENoPeriodo(DateTime dataDe, DateTime dataAte, int idFuncionario, int idDepartamento)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Between("PrazoPadrao", dataDe, dataAte);
            filtros[1] = Filtros.Between("PrazoLegal", dataDe, dataAte);
            filtros[2] = Filtros.Between("PrazoDiretoria", dataDe, dataAte);
            ordem.AdicionarFiltro(Filtros.Ou(filtros));


            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            ordem.AdicionarFiltro(Filtros.CriarAlias("Responsavel", "resp"));
            ordem.AdicionarFiltro(Filtros.Eq("resp.Id", idFuncionario));

            ordem.AdicionarFiltro(Filtros.Maior("DataEncerramento", SqlDate.MinValue));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrdemServico> retorno = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);

            if (retorno != null && retorno.Count > 0)
            {
                IList<OrdemServico> retornoAux = retorno.Where(x => x.GetDataVencimento >= dataDe && x.GetDataVencimento <= dataAte && (x.SolicitacoesAdiamento == null || x.SolicitacoesAdiamento.Count == 0)).ToList();
                return retornoAux != null && retornoAux.Count > 0 ? retornoAux.Count : 0;
            }


            return 0;
        }

        public static int ObterQuantidadeDeOSJustificadasDoFuncionarioNoDepartamentoENoPeriodo(DateTime dataDe, DateTime dataAte, int idFuncionario, int idDepartamento)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Distinct());

            Filtro[] filtros = new Filtro[3];

            filtros[0] = Filtros.Between("PrazoPadrao", dataDe, dataAte);
            filtros[1] = Filtros.Between("PrazoLegal", dataDe, dataAte);
            filtros[2] = Filtros.Between("PrazoDiretoria", dataDe, dataAte);
            ordem.AdicionarFiltro(Filtros.Ou(filtros));


            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            ordem.AdicionarFiltro(Filtros.CriarAlias("Responsavel", "resp"));
            ordem.AdicionarFiltro(Filtros.Eq("resp.Id", idFuncionario));

            ordem.AdicionarFiltro(Filtros.MenorOuIgual("DataEncerramento", SqlDate.MinValue));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrdemServico> retorno = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);

            if (retorno != null && retorno.Count > 0)
            {
                IList<OrdemServico> retornoAux = retorno.Where(x => x.GetDataVencimento >= dataDe && x.GetDataVencimento <= dataAte && x.SolicitacoesAdiamento != null && x.SolicitacoesAdiamento.Count > 0 && x.SolicitacoesAdiamento[0].Data <= x.GetDataVencimento).ToList();
                return retornoAux != null && retornoAux.Count > 0 ? retornoAux.Count : 0;
            }

            return 0;
        }

        public static IList<OrdemServico> ObterOssDoFuncionarioNoPeriodo(DateTime dataDe, DateTime dataAte, int idTipoOS, int idFuncionario, int idDepartamento)
        {
            OrdemServico ordem = new OrdemServico();
            ordem.AdicionarFiltro(Filtros.Eq("Ativo", true));
            ordem.AdicionarFiltro(Filtros.Distinct());

            if (idDepartamento > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Setor", "set"));
                ordem.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
                ordem.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));
            }

            if (idTipoOS > 0)
            {
                ordem.AdicionarFiltro(Filtros.CriarAlias("Tipo", "tipoOS"));
                ordem.AdicionarFiltro(Filtros.Eq("tipoOS.Id", idTipoOS));
            }

            ordem.AdicionarFiltro(Filtros.CriarAlias("Responsavel", "resp"));
            ordem.AdicionarFiltro(Filtros.Eq("resp.Id", idFuncionario));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<OrdemServico> retorno = fabrica.GetDAOBase().ConsultarComFiltro<OrdemServico>(ordem);
            if (retorno != null)
                return retorno.Where(aux => aux.GetDataVencimento >= dataDe && aux.GetDataVencimento <= dataAte).ToList();
            return retorno;
        }

        public virtual bool PossuiSolicitacaoAdiamento
        {
            get
            {
                return this.SolicitacoesAdiamento != null && this.SolicitacoesAdiamento.Count > 0;
            }
        }

        public virtual bool IsEncerradaNoPrazoSemPedidoAdiamento
        {
            get
            {
                DateTime dataEncerramento = this.GetDataEncerramento;
                return dataEncerramento.CompareTo(SqlDate.MinValue) > 0 && dataEncerramento.CompareTo(this.GetDataVencimento) < 1 && !this.PossuiSolicitacaoAdiamento;
            }
        }

        public virtual bool IsEncerradaNoPrazoComPedidoAdiamento
        {
            get
            {
                DateTime dataEncerramento = this.GetDataEncerramento;
                return dataEncerramento.CompareTo(SqlDate.MinValue) > 0 && dataEncerramento.CompareTo(this.GetDataVencimento) < 1 && this.PossuiSolicitacaoAdiamento &&
                    (this.SolicitacoesAdiamento.Count(soli => soli.Parecer == SolicitacaoAdiamento.ACEITA && soli.GetDataAntiga.CompareTo(soli.Data) < 0) < 1);
            }
        }

        public virtual bool IsVencida
        {
            get
            {
                DateTime dataEncerramento = this.GetDataEncerramento;
                DateTime dataVencimento = this.GetDataVencimento;

                return (dataEncerramento.CompareTo(SqlDate.MinValue) > 0 && dataEncerramento.CompareTo(dataVencimento) > 0) ||
                    (dataEncerramento.CompareTo(SqlDate.MinValue) < 1 && DateTime.Now.CompareTo(dataVencimento) > 0);
            }
        }

        public virtual bool IsEncerradaNoPrazoComPedidoAdiamentoAposVencimento
        {
            get
            {
                DateTime dataEncerramento = this.GetDataEncerramento;
                DateTime dataVencimento = this.GetDataVencimento;

                //Data de encerramento menor que a data de vencimento e possui alguma solicitação aceita que possui data posterior à data de vencimento antiga
                return (dataEncerramento.CompareTo(SqlDate.MinValue) > 0 && dataEncerramento.CompareTo(dataVencimento) < 1) && this.PossuiSolicitacaoAdiamento &&
                    (this.SolicitacoesAdiamento.Count(soli => soli.Parecer == SolicitacaoAdiamento.ACEITA && soli.GetDataAntiga.CompareTo(soli.Data) < 0) > 0);
            }
        }

        public static int ObterQuantidadeDeOSDoFuncionarioEDoTipoOSNoPeriodo(DateTime dataDe, DateTime dataAte, int idTipoOS, int idFuncionario, int idDepartamento)
        {
            IList<OrdemServico> retorno = OrdemServico.ObterOssDoFuncionarioNoPeriodo(dataDe, dataAte, idTipoOS, idFuncionario, idDepartamento);
            if (retorno != null && retorno.Count > 0)
            {
                IList<OrdemServico> retornoAux = retorno.Where(x => x.GetDataVencimento >= dataDe && x.GetDataVencimento <= dataAte).ToList();
                return retornoAux != null && retornoAux.Count > 0 ? retornoAux.Count : 0;
            }

            return 0;
        }

        public static int QtdOSEncerradaNoPrazoComPedidoAdiamentoAposVencimentoDoFuncionarioEDoTipoOSNoPeriodo(DateTime dataDe, DateTime dataAte, int idTipoOS, int idResponsavel, int idDepartamento)
        {
            IList<OrdemServico> retorno = OrdemServico.ObterOssDoFuncionarioNoPeriodo(dataDe, dataAte, idTipoOS, idResponsavel, idDepartamento);
            if (retorno != null && retorno.Count > 0)
            {
                IList<OrdemServico> retornoAux = retorno.Where(x => x.IsEncerradaNoPrazoComPedidoAdiamentoAposVencimento).ToList();
                return retornoAux != null && retornoAux.Count > 0 ? retornoAux.Count : 0;
            }

            return 0;
        }

        public static int QtdOSVencidasDoFuncionarioEDoTipoOSNoPeriodo(DateTime dataDe, DateTime dataAte, int idTipoOS, int idResponsavel, int idDepartamento)
        {
            IList<OrdemServico> retorno = OrdemServico.ObterOssDoFuncionarioNoPeriodo(dataDe, dataAte, idTipoOS, idResponsavel, idDepartamento);
            if (retorno != null && retorno.Count > 0)
            {
                IList<OrdemServico> retornoAux = retorno.Where(x => x.IsVencida).ToList();
                return retornoAux != null && retornoAux.Count > 0 ? retornoAux.Count : 0;
            }

            return 0;
        }

        public static int QtdOSEncerradaNoPrazoComPedidoAdiamentoDoFuncionarioEDoTipoOSNoPeriodo(DateTime dataDe, DateTime dataAte, int idTipoOS, int idFuncionario, int idDepartamento)
        {
            IList<OrdemServico> retorno = OrdemServico.ObterOssDoFuncionarioNoPeriodo(dataDe, dataAte, idTipoOS, idFuncionario, idDepartamento);
            if (retorno != null && retorno.Count > 0)
            {
                IList<OrdemServico> retornoAux = retorno.Where(x => x.IsEncerradaNoPrazoComPedidoAdiamento).ToList();
                return retornoAux != null && retornoAux.Count > 0 ? retornoAux.Count : 0;
            }

            return 0;
        }

        public static int QtdOSEncerradaNoPrazoSemPedidoAdiamentoDoFuncionarioEDoTipoOSNoPeriodo(DateTime dataDe, DateTime dataAte, int idTipoOS, int idFuncionario, int idDepartamento)
        {
            IList<OrdemServico> retorno = OrdemServico.ObterOssDoFuncionarioNoPeriodo(dataDe, dataAte, idTipoOS, idFuncionario, idDepartamento);
            if (retorno != null && retorno.Count > 0)
            {
                IList<OrdemServico> retornoAux = retorno.Where(x => x.IsEncerradaNoPrazoSemPedidoAdiamento).ToList();
                return retornoAux != null && retornoAux.Count > 0 ? retornoAux.Count : 0;
            }

            return 0;
        }

        public virtual bool GetFuncionarioPodeAlterarProtocoloOuArquivos(Funcionario funcionario)
        {
            if (funcionario == null)
                return false;
            return funcionario.Equals(this.Responsavel) || (this.CoResponsaveis != null && this.CoResponsaveis.Contains(funcionario));
        }

        public virtual bool IsEncerrada
        {
            get
            {
                return this.DataEncerramento != SqlDate.MinValue && this.DataEncerramento <= Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy 23:59:59"));
            }
        }

        public virtual DateTime GetDataEncerramento
        {
            get
            {
                return this.PossuiProtocolo ? this.DataProtocoloEncerramento : this.DataEncerramento;
            }
        }

        public virtual String GetDataEncerramentoString
        {
            get
            {
                DateTime dataAux = this.GetDataEncerramento;
                return dataAux.CompareTo(SqlDate.MinValue) <= 0 ? "" : dataAux.ToShortDateString();
            }
        }

        public virtual List<SolicitacaoAdiamento> GetSolicitacoesAdiamentoAceitas
        {
            get
            {
                return this.SolicitacoesAdiamento != null ?
                    this.SolicitacoesAdiamento.Where(soli => soli.Parecer == SolicitacaoAdiamento.ACEITA).OrderBy(soli => soli.GetDataAntiga).ToList() :
                    new List<SolicitacaoAdiamento>();
            }
        }

        public virtual bool PossuiReservaDeVeiculoAberta
        {
            get
            {
                return new Reserva().PossuiAlgumaReservaAbertaParaAOS(this.Id);
            }
        }

        public static OrdemServico ConsultarPeloCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                throw new ArgumentException("Código inválido!");
            OrdemServico aux = new OrdemServico();
            aux.AdicionarFiltro(Filtros.MaxResults(1));
            aux.AdicionarFiltro(Filtros.Eq("Codigo", codigo));
            return new FabricaDAONHibernateBase().GetDAOBase().ConsultarUnicoComFiltro<OrdemServico>(aux);
        }

        public virtual bool IsPesquisaSatisfacaoRespondida
        {
            get
            {
                return this.IsEncerrada && this.PesquisaSatisfacao != null && this.PesquisaSatisfacao.IsRespondida;

            }
        }

        public virtual string GetDescricaoMediaSatisfacao
        {
            get
            {
                if (!this.IsPesquisaSatisfacaoRespondida)
                    return "";
                int mediaSatisfacao = this.GetMediaSatisfacao;
                return new RespostaPesquisaSatisfacao() { Resposta = mediaSatisfacao }.GetDescricaoSatisfacao;
            }
        }

        public virtual int GetMediaSatisfacao
        {
            get
            {
                if (!this.IsPesquisaSatisfacaoRespondida)
                    return 0;
                return this.PesquisaSatisfacao.Respostas.Sum(respo => respo.Resposta) / this.PesquisaSatisfacao.Respostas.Count;
            }
        }

        public virtual decimal ValorTotal
        {
            get
            {
                return (this.ValorNominal != null ? this.ValorNominal : 0) - (this.Desconto != null ? this.Desconto : 0);
            }
        }

        public virtual string GetDescricaoOSMatriz
        {
            get
            {
                return new StringBuilder()
                    .Append(this.Codigo)
                    .Append(" - ").Append(this.Data.ToShortDateString())
                    .Append(" - ").Append(this.Descricao)
                    .Append(this.Responsavel != null ? " - " + this.Responsavel.NomeRazaoSocial : "")
                    .ToString();
            }
        }

        public virtual String GetDescricaoCompleta
        {
            get
            {
                StringBuilder str = new StringBuilder();
                str.Append(this.codigo).Append(" - ")
                        .Append(this.data.ToString("dd/MM/yyyy")).Append(" - ")
                        .Append(this.GetDescricaoDoStatusDaOS).Append(": ")
                          .Append(this.Tipo != null ? this.Tipo.Nome + " - " : "")
                        .Append(this.descricao);
                return str.ToString();

            }
        }
        public virtual String GetDescricaoSimplificada
        {
            get
            {
                return new StringBuilder().Append(this.Tipo != null ? this.Tipo.Nome + " - " : "").Append(this.descricao).ToString();
            }
        }
    }
}
