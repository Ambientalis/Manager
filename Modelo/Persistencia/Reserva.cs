using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Reserva : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Reserva ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Reserva classe = new Reserva();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Reserva>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Reserva ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Reserva>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Reserva Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Reserva>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Reserva SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Reserva>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Reserva> SalvarTodos(IList<Reserva> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Reserva>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Reserva> SalvarTodos(params Reserva[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Reserva>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Reserva>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Reserva>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Reserva> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Reserva obj = Activator.CreateInstance<Reserva>();
            return fabrica.GetDAOBase().ConsultarTodos<Reserva>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Reserva> ConsultarOrdemAcendente(int qtd)
        {
            Reserva ee = new Reserva();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Reserva>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Reserva> ConsultarOrdemDescendente(int qtd)
        {
            Reserva ee = new Reserva();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Nome"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Reserva>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Reserva> Filtrar(int qtd)
        {
            Reserva estado = new Reserva();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Reserva>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual Reserva UltimoInserido()
        {
            Reserva estado = new Reserva();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Reserva>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<Reserva> ConsultarTodosOrdemAlfabetica()
        {
            Reserva aux = new Reserva();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Nome"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Reserva>(aux);
        }

        public static bool ExisteAlgumaReservaParaOVeiculoNestePeriodoDeOutraVisita(int idVeiculo, DateTime dataInicio, DateTime dataFim, Reserva reserva)
        {
            Reserva aux = new Reserva();

            aux.AdicionarFiltro(Filtros.Distinct());

            aux.AdicionarFiltro(Filtros.CriarAlias("Veiculo", "veic"));
            aux.AdicionarFiltro(Filtros.Eq("veic.Id", idVeiculo));

            if (reserva != null && reserva.Id > 0)
            {
                aux.AdicionarFiltro(Filtros.NaoIgual("Id", reserva.Id));
            }

            aux.AdicionarFiltro(Filtros.MenorOuIgual("DataInicio", dataFim));
            aux.AdicionarFiltro(Filtros.MaiorOuIgual("DataFim", dataInicio));

            aux.AdicionarFiltro(Filtros.Eq("Status", Reserva.APROVADA));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Reserva> reservasExistentes = fabrica.GetDAOBase().ConsultarComFiltro<Reserva>(aux);

            if (reservasExistentes == null || reservasExistentes.Count == 0)
                return false;

            foreach (Reserva item in reservasExistentes)
            {
                if ((item.DataInicio >= dataInicio && item.DataFim <= dataFim) || (item.DataInicio < dataInicio && item.DataFim <= dataFim) || (item.DataInicio >= dataInicio && item.DataFim > dataFim) || (item.DataInicio < dataInicio && item.DataFim > dataFim))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual string GetDataInicio
        {
            get
            {
                return this.DataInicio != SqlDate.MinValue ? this.DataInicio.ToString("dd/MM/yyyy HH:mm") : "";
            }
        }

        public virtual string GetDataFim
        {
            get
            {
                return this.DataFim != SqlDate.MinValue ? this.DataFim.ToString("dd/MM/yyyy HH:mm") : "";
            }
        }

        public virtual string GetNomeTipo
        {
            get
            {
                return this.TipoReservaVeiculo != null ? this.TipoReservaVeiculo.Descricao : "";
            }
        }

        public virtual string GetDescricaoNomeCliente
        {
            get
            {

                if (this.TipoReservaVeiculo != null && this.TipoReservaVeiculo.TipoVisitaOS)
                {
                    return this.Visita.GetNomeCliente;
                }

                return "";
            }
        }

        public virtual string GetDescricaoVeiculo
        {
            get
            {
                return this.Veiculo != null ? this.Veiculo.Descricao + " - " + this.Veiculo.Placa : "";
            }
        }

        public virtual string GetNomeResponsavel
        {
            get
            {
                return this.Responsavel != null ? this.Responsavel.NomeRazaoSocial : "";
            }
        }

        public virtual string GetQuilometragem
        {
            get
            {
                return this.Quilometragem.ToString("N2");
            }
        }

        public virtual string GetConsumo
        {
            get
            {
                return this.Consumo.ToString("N2");
            }
        }

        public static IList<Reserva> Filtrar(DateTime dataDe, DateTime dataAte, string descricao, int idTipoReserva, int idVeiculo, int idResponsavel, char status)
        {
            Reserva aux = new Reserva();

            aux.AdicionarFiltro(Filtros.Distinct());

            if (!string.IsNullOrEmpty(descricao))
                aux.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            if (idTipoReserva > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("TipoReservaVeiculo", "tip"));
                aux.AdicionarFiltro(Filtros.Eq("tip.Id", idTipoReserva));
            }

            if (idVeiculo > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Veiculo", "veic"));
                aux.AdicionarFiltro(Filtros.Eq("veic.Id", idVeiculo));
            }

            if (idResponsavel > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Responsavel", "resp"));
                aux.AdicionarFiltro(Filtros.Eq("resp.Id", idResponsavel));
            }

            aux.AdicionarFiltro(Filtros.MaiorOuIgual("DataInicio", dataDe));
            aux.AdicionarFiltro(Filtros.MenorOuIgual("DataFim", dataAte));

            if (status != 'T')
                aux.AdicionarFiltro(Filtros.Eq("Status", status));

            aux.AdicionarFiltro(Filtros.SetOrderDesc("DataInicio"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Reserva>(aux);
        }

        public static bool ConsultarReservasComBoletimPendenteDoUsuario(int idUsuarioLogado, DateTime dataReferencia)
        {
            Reserva aux = new Reserva();

            aux.AdicionarFiltro(Filtros.Distinct());

            if (idUsuarioLogado > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Responsavel", "resp"));
                aux.AdicionarFiltro(Filtros.Eq("resp.Id", idUsuarioLogado));
            }

            dataReferencia = dataReferencia.AddDays(-3);

            aux.AdicionarFiltro(Filtros.MenorOuIgual("DataFim", dataReferencia));

            aux.AdicionarFiltro(Filtros.Ou(Filtros.Eq("Quilometragem", decimal.Zero), Filtros.Eq("Consumo", decimal.Zero)));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList<Reserva> reservasPendentes = fabrica.GetDAOBase().ConsultarComFiltro<Reserva>(aux);

            return reservasPendentes != null && reservasPendentes.Count > 0;
        }

        public virtual string GetDescricaoStatus
        {
            get
            {
                return this.Status == Reserva.APROVADA ? "Aprovada" : this.Status == Reserva.ENCERRADA ? "Encerrada" : "Aguardando Aprovação";
            }
        }

        public virtual bool PossuiAlgumaReservaAbertaParaAOS(int idOS)
        {
            Reserva aux = new Reserva();
            //Alguma Reserva que não esteja encerrada para a OS
            aux.AdicionarFiltro(Filtros.Count("Id"));
            aux.AdicionarFiltro(Filtros.NaoIgual("Status", Reserva.ENCERRADA));
            aux.AdicionarFiltro(Filtros.CriarAlias("Visita", "vis"));
            aux.AdicionarFiltro(Filtros.CriarAlias("vis.OrdemServico", "os"));
            aux.AdicionarFiltro(Filtros.Eq("os.Id", idOS));
            object retorno = new FabricaDAONHibernateBase().GetDAOBase().ConsultarProjecao(aux);
            return retorno != null && Convert.ToInt32(retorno) > 0;
        }
    }
}
