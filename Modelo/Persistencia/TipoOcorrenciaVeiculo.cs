using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class TipoOcorrenciaVeiculo: ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static TipoOcorrenciaVeiculo ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            TipoOcorrenciaVeiculo classe = new TipoOcorrenciaVeiculo();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<TipoOcorrenciaVeiculo>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual TipoOcorrenciaVeiculo ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<TipoOcorrenciaVeiculo>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual TipoOcorrenciaVeiculo Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<TipoOcorrenciaVeiculo>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual TipoOcorrenciaVeiculo SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<TipoOcorrenciaVeiculo>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<TipoOcorrenciaVeiculo> SalvarTodos(IList<TipoOcorrenciaVeiculo> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<TipoOcorrenciaVeiculo>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<TipoOcorrenciaVeiculo> SalvarTodos(params TipoOcorrenciaVeiculo[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<TipoOcorrenciaVeiculo>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<TipoOcorrenciaVeiculo>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<TipoOcorrenciaVeiculo>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<TipoOcorrenciaVeiculo> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            TipoOcorrenciaVeiculo obj = Activator.CreateInstance<TipoOcorrenciaVeiculo>();
            return fabrica.GetDAOBase().ConsultarTodos<TipoOcorrenciaVeiculo>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<TipoOcorrenciaVeiculo> ConsultarOrdemAcendente(int qtd)
        {
            TipoOcorrenciaVeiculo ee = new TipoOcorrenciaVeiculo();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Descricao"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TipoOcorrenciaVeiculo>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<TipoOcorrenciaVeiculo> ConsultarOrdemDescendente(int qtd)
        {
            TipoOcorrenciaVeiculo ee = new TipoOcorrenciaVeiculo();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Descricao"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TipoOcorrenciaVeiculo>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<TipoOcorrenciaVeiculo> Filtrar(int qtd)
        {
            TipoOcorrenciaVeiculo estado = new TipoOcorrenciaVeiculo();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TipoOcorrenciaVeiculo>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual TipoOcorrenciaVeiculo UltimoInserido()
        {
            TipoOcorrenciaVeiculo estado = new TipoOcorrenciaVeiculo();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<TipoOcorrenciaVeiculo>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<TipoOcorrenciaVeiculo> ConsultarTodosOrdemAlfabetica()
        {
            TipoOcorrenciaVeiculo aux = new TipoOcorrenciaVeiculo();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Descricao"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<TipoOcorrenciaVeiculo>(aux);
        }
    }
}
