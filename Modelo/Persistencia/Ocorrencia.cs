using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class Ocorrencia: ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Ocorrencia ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Ocorrencia classe = new Ocorrencia();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Ocorrencia>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Ocorrencia ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Ocorrencia>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Ocorrencia Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Ocorrencia>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Ocorrencia SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Ocorrencia>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Ocorrencia> SalvarTodos(IList<Ocorrencia> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Ocorrencia>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Ocorrencia> SalvarTodos(params Ocorrencia[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Ocorrencia>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Ocorrencia>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Ocorrencia>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Ocorrencia> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Ocorrencia obj = Activator.CreateInstance<Ocorrencia>();
            return fabrica.GetDAOBase().ConsultarTodos<Ocorrencia>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Ocorrencia> ConsultarOrdemAcendente(int qtd)
        {
            Ocorrencia ee = new Ocorrencia();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Descricao"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Ocorrencia>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Ocorrencia> ConsultarOrdemDescendente(int qtd)
        {
            Ocorrencia ee = new Ocorrencia();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Descricao"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Ocorrencia>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Ocorrencia> Filtrar(int qtd)
        {
            Ocorrencia estado = new Ocorrencia();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Ocorrencia>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual Ocorrencia UltimoInserido()
        {
            Ocorrencia estado = new Ocorrencia();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Ocorrencia>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<Ocorrencia> ConsultarTodosOrdemAlfabetica()
        {
            Ocorrencia aux = new Ocorrencia();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Descricao"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Ocorrencia>(aux);
        }       

        public virtual string GetData 
        {
            get 
            {
                return this.Data.ToShortDateString();
            }
        }

        public virtual string GetNomeTipo 
        {
            get 
            {
                return this.TipoOcorrenciaVeiculo != null ? this.TipoOcorrenciaVeiculo.Descricao : "";
            }
        } 

        public static IList<Ocorrencia> Filtrar(DateTime dataDe, DateTime dataAte, string descricao, int idTipoOcorrencia, int idVeiculo, int idResponsavel)
        {
            Ocorrencia aux = new Ocorrencia();

            aux.AdicionarFiltro(Filtros.Distinct());

            if (!string.IsNullOrEmpty(descricao))
                aux.AdicionarFiltro(Filtros.Like("Descricao", descricao));

            if (idTipoOcorrencia > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("TipoOcorrenciaVeiculo", "tip"));
                aux.AdicionarFiltro(Filtros.Eq("tip.Id", idTipoOcorrencia));
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

            aux.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));

            aux.AdicionarFiltro(Filtros.SetOrderDesc("Data"));

            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Ocorrencia>(aux);
        }
    }
}
