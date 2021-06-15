using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class MovimentacaoFinanceira : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static MovimentacaoFinanceira ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            MovimentacaoFinanceira classe = new MovimentacaoFinanceira();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<MovimentacaoFinanceira>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual MovimentacaoFinanceira ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<MovimentacaoFinanceira>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual MovimentacaoFinanceira Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<MovimentacaoFinanceira>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual MovimentacaoFinanceira SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<MovimentacaoFinanceira>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<MovimentacaoFinanceira> SalvarTodos(IList<MovimentacaoFinanceira> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<MovimentacaoFinanceira>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<MovimentacaoFinanceira> SalvarTodos(params MovimentacaoFinanceira[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<MovimentacaoFinanceira>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<MovimentacaoFinanceira>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<MovimentacaoFinanceira>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<MovimentacaoFinanceira> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            MovimentacaoFinanceira obj = Activator.CreateInstance<MovimentacaoFinanceira>();
            return fabrica.GetDAOBase().ConsultarTodos<MovimentacaoFinanceira>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<MovimentacaoFinanceira> ConsultarOrdemAcendente(int qtd)
        {
            MovimentacaoFinanceira ee = new MovimentacaoFinanceira();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Data"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<MovimentacaoFinanceira>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<MovimentacaoFinanceira> ConsultarOrdemDescendente(int qtd)
        {
            MovimentacaoFinanceira ee = new MovimentacaoFinanceira();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Data"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<MovimentacaoFinanceira>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<MovimentacaoFinanceira> Filtrar(int qtd)
        {
            MovimentacaoFinanceira estado = new MovimentacaoFinanceira();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<MovimentacaoFinanceira>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual MovimentacaoFinanceira UltimoInserido()
        {
            MovimentacaoFinanceira estado = new MovimentacaoFinanceira();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<MovimentacaoFinanceira>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<MovimentacaoFinanceira> ConsultarTodosOrdemAlfabetica()
        {
            MovimentacaoFinanceira aux = new MovimentacaoFinanceira();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Data"));
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<MovimentacaoFinanceira>(aux);
        }

        public virtual bool IsEntrada
        {
            get
            {
                return this.Tipo == MovimentacaoFinanceira.ENTRADA;
            }
        }

        public virtual decimal ValorTotal
        {
            get
            {
                return (this.valorNominal != null ? this.valorNominal : 0) - (this.desconto != null ? this.desconto : 0) + (this.acrescimo != null ? this.acrescimo : 0);
            }
        }

        public virtual decimal GetValorNominalTotalParcelas
        {
            get
            {
                return this.Parcelas != null ? this.Parcelas.Sum(parc => parc.ValorNominal) : 0;
            }
        }

        public virtual decimal GetDescontoTotalParcelas
        {
            get
            {
                return this.Parcelas != null ? this.Parcelas.Sum(parc => parc.Descontos) : 0;
            }
        }

        public virtual decimal GetAcrescimoTotalParcelas
        {
            get
            {
                return this.Parcelas != null ? this.Parcelas.Sum(parc => parc.Acrescimos) : 0;
            }
        }

        public virtual decimal GetValorTotalParcelas
        {
            get
            {
                return this.Parcelas != null ? this.Parcelas.Sum(parc => parc.ValorTotal) : 0;
            }
        }

        public virtual void adicionarParcela(Parcela parcela)
        {
            if (this.Parcelas == null)
                return;
            this.Parcelas.Add(parcela);
            this.ValorNominal += parcela.ValorNominal;
            this.Desconto += parcela.Descontos;
            if (this.Id > 0)
                this.RenumerarParcelas();
        }

        public virtual void removerParcela(Parcela parcela)
        {
            if (this.Parcelas == null)
                return;
            this.Parcelas.Remove(parcela);
            this.ValorNominal -= parcela.ValorNominal;
            this.Desconto -= parcela.Descontos;
            if (this.Id > 0)
                this.RenumerarParcelas();
        }

        private void RenumerarParcelas()
        {
            if (this.Parcelas == null || this.Parcelas.Count < 1)
                return;

            IList<Parcela> parcelasOrdenadas = this.GetParcelasOrdenadasPeloNumero;
            for (int index = 0; index < parcelasOrdenadas.Count; index++)
            {
                parcelasOrdenadas[index].Numero = (index + 1);
                parcelasOrdenadas[index].Salvar();
            }
        }

        public virtual IList<Parcela> GetParcelasOrdenadasPeloNumero
        {
            get
            {
                if (this.Parcelas == null)
                    return null;
                return this.Parcelas.OrderBy(parce => parce.Numero).ToList();
            }
        }

        public virtual String GetDescricaoParcelas
        {
            get
            {
                if (this.parcelas == null || this.parcelas.Count < 1)
                {
                    return null;
                }
                return String.Join("<br />", this.parcelas.OrderBy(parc => parc.Numero).Select(parc => parc.GetDescricaoCompleta).ToArray());
            }
        }
    }
}
