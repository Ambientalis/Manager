using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Pedido : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Pedido ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Pedido classe = new Pedido();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Pedido>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Pedido ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Pedido>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Pedido Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Pedido>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Pedido SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Pedido>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Pedido> SalvarTodos(IList<Pedido> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Pedido>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Pedido> SalvarTodos(params Pedido[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Pedido>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Pedido>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Pedido>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Pedido> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Pedido obj = Activator.CreateInstance<Pedido>();
            return fabrica.GetDAOBase().ConsultarTodos<Pedido>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Pedido> ConsultarOrdemAcendente(int qtd)
        {
            Pedido ee = new Pedido();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pedido>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Pedido> ConsultarOrdemDescendente(int qtd)
        {
            Pedido ee = new Pedido();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pedido>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Pedido> Filtrar(int qtd)
        {
            Pedido estado = new Pedido();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pedido>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual Pedido UltimoInserido()
        {
            Pedido estado = new Pedido();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Pedido>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<Pedido> ConsultarTodosOrdemAlfabetica()
        {
            Pedido aux = new Pedido();
            aux.AdicionarFiltro(Filtros.SetOrderDesc("Codigo"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pedido>(aux);
        }

        public virtual string GetNomeCliente
        {
            get
            {
                return this.Cliente != null ? this.Cliente.NomeRazaoSocial : "";
            }
        }

        public virtual string GetNomeVendedor
        {
            get
            {
                return this.Vendedor != null ? this.Vendedor.NomeRazaoSocial : "";
            }
        }

        public virtual string GetDescricaoTipo
        {
            get
            {
                return this.TipoPedido != null ? this.TipoPedido.Nome : "";
            }
        }

        public virtual string GetNumerosOSs
        {
            get
            {
                string retorno = "";

                if (this.OrdensServico != null && this.OrdensServico.Count > 0)
                {
                    foreach (OrdemServico item in this.OrdensServico)
                    {
                        if (item == this.OrdensServico[this.OrdensServico.Count - 1])
                            retorno += item.Codigo;
                        else
                            retorno += item.Codigo + "; ";
                    }
                }
                return retorno;
            }
        }

        public virtual string GetNumerosOrcamento
        {
            get
            {
                return this.Orcamento != null ? this.Orcamento.Numero : "";
            }
        }

        public static IList<Pedido> Filtrar(string codigo, int idTipoPedido, DateTime dataDe, DateTime dataAte, int idCliente, int status, int contratoFixo, int idVendedor)
        {
            return Pedido.Filtrar(codigo, idTipoPedido, dataDe, dataAte, idCliente, status, contratoFixo, idVendedor, 0);
        }

        public static IList<Pedido> Filtrar(string codigo, int idTipoPedido, DateTime dataDe, DateTime dataAte, int idCliente, int status, int contratoFixo, int idVendedor, int idDepartamento)
        {
            Pedido pedido = Pedido.GetFiltrosPedido(codigo, idTipoPedido, dataDe, dataAte, idCliente, status, contratoFixo, idVendedor, idDepartamento);

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Pedido> pedidos = fabrica.GetDAOBase().ConsultarComFiltro<Pedido>(pedido);

            if (!string.IsNullOrEmpty(codigo))
            {
                Pedido pedido2 = Pedido.GetFiltrosPedido(null, idTipoPedido, dataDe, dataAte, idCliente, status, contratoFixo, idVendedor, idDepartamento);
                pedido2.AdicionarFiltro(Filtros.CriarAlias("Orcamento", "orc"));
                pedido2.AdicionarFiltro(Filtros.Like("orc.Numero", codigo));
                IList<Pedido> pedidosDoOrcamento = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<Pedido>(pedido2);
                List<Pedido> retorno = new List<Pedido>();
                retorno.AddRange(pedidos);
                retorno.AddRange(pedidosDoOrcamento);
                return retorno.Distinct().OrderByDescending(aux => aux.Data).ToList();
            }
            return pedidos;
        }

        private static Pedido GetFiltrosPedido(string codigo, int idTipoPedido, DateTime dataDe, DateTime dataAte, int idCliente, int status, int contratoFixo, int idVendedor, int idDepartamento)
        {
            Pedido pedido = new Pedido();
            pedido.AdicionarFiltro(Filtros.Distinct());

            if (!string.IsNullOrEmpty(codigo))
                pedido.AdicionarFiltro(Filtros.Like("Codigo", codigo));

            if (idTipoPedido > 0)
            {
                pedido.AdicionarFiltro(Filtros.CriarAlias("TipoPedido", "tip"));
                pedido.AdicionarFiltro(Filtros.Eq("tip.Id", idTipoPedido));
            }

            pedido.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));
            pedido.AdicionarFiltro(Filtros.SetOrderDesc("Data"));

            if (idCliente > 0)
            {
                pedido.AdicionarFiltro(Filtros.CriarAlias("Cliente", "cli"));
                pedido.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
            }

            if (idVendedor > 0)
            {
                pedido.AdicionarFiltro(Filtros.CriarAlias("Vendedor", "vend"));
                pedido.AdicionarFiltro(Filtros.Eq("vend.Id", idVendedor));
            }

            if (status > 0)
                pedido.AdicionarFiltro(Filtros.Eq("Ativo", status == 1));

            if (contratoFixo > 0)
                pedido.AdicionarFiltro(Filtros.Eq("ContratoFixo", contratoFixo == 1));

            if (idDepartamento > 0)
            {
                pedido.AdicionarFiltro(Filtros.CriarAlias("OrdensServico", "os"));
                pedido.AdicionarFiltro(Filtros.CriarAlias("os.Setor", "setor"));
                pedido.AdicionarFiltro(Filtros.CriarAlias("setor.Departamento", "dep"));
                pedido.AdicionarFiltro(Filtros.Eq("dep.Id", idDepartamento));
            }
            return pedido;
        }

        public virtual string PedidoMotivoNaoPodeSerExcluido
        {
            get
            {
                if (this.OrdensServico != null && this.OrdensServico.Count > 0)
                {
                    string motivo = this.PedidoMotivoNaoPodeSerExcluidoRecursivo(this.OrdensServico);
                    if (motivo != "")
                        return motivo;
                }

                return "";
            }
        }

        private string PedidoMotivoNaoPodeSerExcluidoRecursivo(IList<OrdemServico> ordens)
        {
            foreach (OrdemServico ordem in ordens)
            {
                if (ordem.Visitas != null && ordem.Visitas.Count > 0)
                    return "Não é possível excluir pedidos com Ordens de Serviço que já possuam visitas cadastradas";

                if (ordem.Atividades != null && ordem.Atividades.Count > 0)
                    return "Não é possível excluir pedidos com Ordens de Serviço que já possuam atividades cadastradas";

                if (ordem.DataEncerramento != SqlDate.MinValue || ordem.DataEncerramento != SqlDate.MaxValue)
                    return "Não é possível excluir pedidos com Ordens de Serviço que já estão encerradas";

                if (ordem.OssVinculadas != null && ordem.OssVinculadas.Count > 0)
                {
                    string motivo = this.PedidoMotivoNaoPodeSerExcluidoRecursivo(ordem.OssVinculadas);
                    if (motivo != "")
                        return motivo;
                }
            }

            return "";
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

        public static string GerarNumeroCodigo()
        {
            Pedido aux = new Pedido();
            aux.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Pedido pedido = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Pedido>(aux);
            if (pedido == null)
            {
                return "1";
            }
            else
            {
                return (pedido.Id + 1).ToString();
            }
        }

        public static bool ExistePedidoComEsteCodigoDiferenteDesse(string codigo, Pedido pedido)
        {
            Pedido aux = new Pedido();

            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.Eq("Codigo", codigo));

            if (pedido != null && pedido.Id > 0)
            {
                aux.AdicionarFiltro(Filtros.NaoIgual("Id", pedido.Id));
            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Pedido pedidoComOCodigo = fabrica.GetDAOBase().ConsultarUnicoComFiltro<Pedido>(aux);

            return pedidoComOCodigo != null && pedidoComOCodigo.Id > 0;
        }

        public static IList<Pedido> ConsultarPedidosDoClienteParaAcompanhamento(int idCliente, bool pesquisarSomenteComOsAberta)
        {
            if (idCliente <= 0)
                return null;

            Pedido pedido = new Pedido();
            pedido.AdicionarFiltro(Filtros.Distinct());

            pedido.AdicionarFiltro(Filtros.SetOrderDesc("Data"));

            if (idCliente > 0)
            {
                pedido.AdicionarFiltro(Filtros.CriarAlias("Cliente", "cli"));
                pedido.AdicionarFiltro(Filtros.Eq("cli.Id", idCliente));
            }

            if (pesquisarSomenteComOsAberta)
            {
                pedido.AdicionarFiltro(Filtros.CriarAlias("OrdensServico", "ordens"));

                pedido.AdicionarFiltro(Filtros.Eq("ordens.Ativo", pesquisarSomenteComOsAberta));
                pedido.AdicionarFiltro(Filtros.Eq("ordens.DataEncerramento", SqlDate.MinValue));

            }

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Pedido>(pedido);
        }

        public static int ObterQuantidadeDePedidosNoPeriodo(DateTime dataDe, DateTime dataAte)
        {
            Pedido pedido = new Pedido();
            pedido.AdicionarFiltro(Filtros.Distinct());

            pedido.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            pedido.AdicionarFiltro(Filtros.Count("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            object o = fabrica.GetDAOBase().ConsultarProjecao(pedido);

            if (o != null)
            {
                return Convert.ToInt32(o.ToString());
            }

            return 0;

        }

        public static int ObterQuantidadeDePedidosDoDepartamentoNoPeriodo(DateTime dataDe, DateTime dataAte, int idDepartamento)
        {
            Pedido pedido = new Pedido();
            pedido.AdicionarFiltro(Filtros.Distinct());

            pedido.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            pedido.AdicionarFiltro(Filtros.CriarAlias("OrdensServico", "ordens"));
            pedido.AdicionarFiltro(Filtros.CriarAlias("ordens.Setor", "set"));
            pedido.AdicionarFiltro(Filtros.CriarAlias("set.Departamento", "dept"));
            pedido.AdicionarFiltro(Filtros.Eq("dept.Id", idDepartamento));

            pedido.AdicionarFiltro(Filtros.Count("Id"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            object o = fabrica.GetDAOBase().ConsultarProjecao(pedido);
            return o != null ? Convert.ToInt32(o.ToString()) : 0;
        }

        public virtual String GetDescricaoDepartamentos
        {
            get
            {
                if (this.OrdensServico == null)
                    return "";
                IList<Departamento> deps = new List<Departamento>();
                StringBuilder aux = new StringBuilder();
                foreach (OrdemServico os in this.OrdensServico)
                    if (os.Setor != null && !deps.Contains(os.Setor.Departamento))
                    {
                        deps.Add(os.Setor.Departamento);
                        aux.Append(os.Setor.Departamento.Nome).Append("; ");
                    }
                return aux.ToString().Trim();
            }
        }

        public static IList<Pedido> ConsultarPedidosRelatorio(DateTime dataDe, DateTime dataAte, int dataDeCriacao)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (dataDeCriacao == 1)
            {
                Pedido pedido = new Pedido();
                pedido.AdicionarFiltro(Filtros.Distinct());
                pedido.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));
                return fabrica.GetDAOBase().ConsultarComFiltro(pedido);
            }
            else
            {
                IList<OrdemServico> ordens = OrdemServico.ConsultarOrdensComVencimentoNoPeriodoDoDepartamento(dataDe, dataAte, 0, 0);
                ordens = ordens.Where(x => x.GetDataVencimento >= dataDe && x.GetDataVencimento <= dataAte).ToList();
                return ordens.Select(ordem => ordem.Pedido).Distinct().ToList();
            }
        }

        public virtual bool IsPossuiDivergenciaDeValores
        {
            get
            {
                return this.Receita != null && this.Receita.ValorTotal != this.GetValorOss;
            }
        }

        public virtual decimal GetValorOss
        {
            get
            {
                decimal retorno = 0;
                if (this.OrdensServico != null)
                    retorno = this.OrdensServico.Sum(os => os.ValorTotal);
                return retorno;
            }
        }

        public virtual string GetStatusContrato
        {
            get
            {
                if (this.IsContratoAceito)
                    return "Contrato lido. Data: " + this.DataAceiteContrato.ToString("dd/MM/yyyy HH:mm:ss");
                else if (this.IsContratoEnviado)
                    return "Aguardando leitura. Envio: " + this.DataEnvioContrato.ToString("dd/MM/yyyy HH:mm:ss");
                else return "Não gerado";
            }
        }

        public virtual string GetStatusContratoSimplificado
        {
            get
            {
                if (this.IsContratoAceito)
                    return "Lido";
                else if (this.IsContratoEnviado)
                    return "Aguardando leitura";
                else return "Não gerado";
            }
        }

        public virtual bool IsContratoAceito
        {
            get
            {
                return this.DataAceiteContrato != null && this.DataAceiteContrato.CompareTo(SqlDate.MinValue) > 0;
            }
        }

        public virtual bool IsContratoEnviado
        {
            get
            {
                return this.DataEnvioContrato != null && this.DataEnvioContrato.CompareTo(SqlDate.MinValue) > 0;
            }
        }

        public virtual string GetDescricaoOrdensServico
        {
            get
            {
                if (this.OrdensServico == null || this.OrdensServico.Count < 1)
                {
                    return "";
                }
                return String.Join("<br />", this.OrdensServico.Select(os => os.GetDescricaoCompleta).ToArray());
            }
        }

        public virtual String GetDescricaoOrdensServicoSimplificada
        {
            get
            {
                if (this.OrdensServico == null || this.OrdensServico.Count < 1)
                {
                    return "";
                }
                return String.Join("<br />", this.OrdensServico.Select(os => os.GetDescricaoSimplificada).ToArray());
            }
        }

        public virtual string ProximoCodigo()
        {
            Pedido pedido = new Pedido();
            pedido.AdicionarFiltro(Filtros.ValorMaximo("Codigo"));
            Object ultimoCodigo = new FabricaDAONHibernateBase().GetDAOBase().ConsultarProjecao(pedido);
            if (ultimoCodigo != null)
            {
                int ultimoCodigoInt = 0;
                if (Int32.TryParse(ultimoCodigo.ToString(), out ultimoCodigoInt))
                    return (ultimoCodigoInt + 1).ToString("#000000000");
                else return ultimoCodigo + "_A";
            }
            else
                return "000000001";
        }

        public virtual string GetDescricaoUnidade
        {
            get
            {
                if (this.Emp == 0)
                    return "Todas";
                Unidade uni = new Unidade(this.Emp).ConsultarPorId();
                return (uni != null ? uni.Descricao : null);
            }
        }
    }
}
