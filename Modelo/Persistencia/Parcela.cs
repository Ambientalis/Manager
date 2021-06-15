using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class Parcela : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Parcela ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Parcela classe = new Parcela();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Parcela>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Parcela ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Parcela>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Parcela Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Parcela>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Parcela SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Parcela>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Parcela> SalvarTodos(IList<Parcela> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Parcela>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Parcela> SalvarTodos(params Parcela[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Parcela>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Parcela>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Parcela>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Parcela> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Parcela obj = Activator.CreateInstance<Parcela>();
            return fabrica.GetDAOBase().ConsultarTodos<Parcela>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Parcela> ConsultarOrdemAcendente(int qtd)
        {
            Parcela ee = new Parcela();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("DataEmissao"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Parcela>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Parcela> ConsultarOrdemDescendente(int qtd)
        {
            Parcela ee = new Parcela();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("DataEmissao"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Parcela>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Parcela> Filtrar(int qtd)
        {
            Parcela estado = new Parcela();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Parcela>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual Parcela UltimoInserido()
        {
            Parcela estado = new Parcela();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Parcela>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<Parcela> ConsultarTodosOrdemAlfabetica()
        {
            Parcela aux = new Parcela();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("DataEmissao"));
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Parcela>(aux);
        }

        public virtual bool IsAberta
        {
            get
            {
                return !this.IsPaga && !this.IsVencida && (this.dataVencimento != null &&
                    this.dataVencimento.CompareTo(Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00")) >= 0);
            }
        }

        public virtual bool IsPaga
        {
            get
            {
                return this.dataPagamento != null && this.dataPagamento.CompareTo(SqlDate.MinValue) > 0;
            }
        }

        public virtual bool IsVencida
        {
            get
            {
                return !this.IsPaga && this.dataVencimento != null &&
                    this.dataVencimento.CompareTo(SqlDate.MinValue) > 0 && this.dataVencimento.CompareTo(Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00")) < 0;
            }
        }

        public virtual String GetDescricaoStatus
        {
            get
            {
                return this.IsPaga ? (this.MovimentacaoFinanceira.IsEntrada ? "Recebida" : "Paga") : (this.IsVencida ? "Vencida" : this.IsFaturada ? "Faturada" : "Aberta");
            }
        }

        public virtual decimal ValorTotal
        {
            get
            {
                return (this.valorNominal != null ? this.valorNominal : 0) - (this.descontos != null ? this.descontos : 0) + (this.acrescimos != null ? this.acrescimos : 0);
            }
        }

        public virtual string GetDescricaoCaixa
        {
            get
            {
                return this.Caixa != null ? this.Caixa.Descricao : "";
            }
        }

        public virtual bool IsFaturada
        {
            get
            {
                return this.Fatura != null;
            }
        }

        public virtual bool IsPodeEditar
        {
            get
            {
                return !this.IsFaturada && !this.IsPaga;
            }
        }

        public virtual String GetDescricaoCompleta
        {
            get
            {
                return new StringBuilder().Append(this.dataVencimento.ToShortDateString()).Append(" ")
               .Append(this.GetNumeroFormatado).Append(" - ")
               .Append(String.Format("{0:c}", this.ValorTotal))
               .Append(" (").Append(this.GetDescricaoStatus).Append(")").ToString();
            }
        }

        public virtual String GetNumeroFormatado
        {
            get
            {
                return "(" + this.Numero + "/" + this.Quantidade + ")";
            }
        }
    }
}
