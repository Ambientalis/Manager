using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class ItemOrcamento : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static ItemOrcamento ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ItemOrcamento classe = new ItemOrcamento();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<ItemOrcamento>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual ItemOrcamento ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<ItemOrcamento>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual ItemOrcamento Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<ItemOrcamento>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual ItemOrcamento SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<ItemOrcamento>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ItemOrcamento> SalvarTodos(IList<ItemOrcamento> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ItemOrcamento>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ItemOrcamento> SalvarTodos(params ItemOrcamento[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ItemOrcamento>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<ItemOrcamento>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<ItemOrcamento>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<ItemOrcamento> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ItemOrcamento obj = Activator.CreateInstance<ItemOrcamento>();
            return fabrica.GetDAOBase().ConsultarTodos<ItemOrcamento>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ItemOrcamento> ConsultarOrdemAcendente(int qtd)
        {
            ItemOrcamento ee = new ItemOrcamento();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Numero"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ItemOrcamento>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ItemOrcamento> ConsultarOrdemDescendente(int qtd)
        {
            ItemOrcamento ee = new ItemOrcamento();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Numero"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ItemOrcamento>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<ItemOrcamento> Filtrar(int qtd)
        {
            ItemOrcamento estado = new ItemOrcamento();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ItemOrcamento>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual ItemOrcamento UltimoInserido()
        {
            ItemOrcamento estado = new ItemOrcamento();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ItemOrcamento>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<ItemOrcamento> ConsultarTodosOrdemAlfabetica()
        {
            ItemOrcamento aux = new ItemOrcamento();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Numero"));
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ItemOrcamento>(aux);
        }

        public virtual string DescricaoCompleta
        {
            get
            {
                return new StringBuilder().Append(this.numero).Append(" - ")
                    .Append(this.descricao)
                    .Append(" (").Append(this.quantidade).Append(" x ").Append(String.Format("{0:c}", this.ValorUnitario)).Append(") = ")
                    .Append(String.Format("{0:c}", this.ValorTotal)).ToString();
            }
        }

        public virtual decimal ValorTotal
        {
            get
            {
                decimal quantidadeAux = this.quantidade != null ? this.quantidade : 0;
                decimal valorAux = this.valorUnitario != null ? this.valorUnitario : 0;
                decimal descontoAux = this.desconto != null ? this.desconto : 0;
                return (valorAux * quantidadeAux) - descontoAux;
            }
        }
    }
}
