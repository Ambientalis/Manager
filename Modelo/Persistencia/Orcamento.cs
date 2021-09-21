using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Persistencia.Utilitarios;


namespace Modelo
{
    public partial class Orcamento : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static Orcamento ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Orcamento classe = new Orcamento();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<Orcamento>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual Orcamento ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<Orcamento>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Orcamento Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Orcamento>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual Orcamento SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<Orcamento>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Orcamento> SalvarTodos(IList<Orcamento> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Orcamento>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<Orcamento> SalvarTodos(params Orcamento[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<Orcamento>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<Orcamento>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<Orcamento>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<Orcamento> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            Orcamento obj = Activator.CreateInstance<Orcamento>();
            return fabrica.GetDAOBase().ConsultarTodos<Orcamento>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Orcamento> ConsultarOrdemAcendente(int qtd)
        {
            Orcamento ee = new Orcamento();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Numero"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Orcamento>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<Orcamento> ConsultarOrdemDescendente(int qtd)
        {
            Orcamento ee = new Orcamento();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Numero"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Orcamento>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<Orcamento> Filtrar(int qtd)
        {
            Orcamento estado = new Orcamento();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Orcamento>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual Orcamento UltimoInserido()
        {
            Orcamento estado = new Orcamento();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<Orcamento>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<Orcamento> ConsultarTodosOrdemAlfabetica()
        {
            Orcamento aux = new Orcamento();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Numero"));
            aux.AdicionarFiltro(Filtros.Eq("Ativo", true));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<Orcamento>(aux);
        }

        public virtual string GetDescricaoFormasDePagamento
        {
            get
            {
                if (this.formasDePagamento == null || this.formasDePagamento.Count < 1)
                {
                    return "";
                }
                return String.Join("<br />", this.formasDePagamento.Select(forma => forma.GetDescricaoFormatada).ToArray());
            }
        }

        public virtual string GetDescricaoItens
        {
            get
            {
                if (this.itens == null || this.itens.Count < 1)
                {
                    return "";
                }
                return String.Join("<br />", this.itens.Select(item => item.DescricaoCompleta).ToArray());
            }
        }

        public virtual bool IsRecusado
        {
            get
            {
                return Orcamento.RECUSADO.Equals(this.status);
            }
        }

        public static IList GetConsultarOrcamentosCancelados()
        {
            String sql = "SELECT id,  status   FROM [ambientalis].[dbo].[orcamentos]   where status = 'Cancelado'";
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            IList retorno = fabrica.GetDAOBase().ConsultaSQL(sql);
            return retorno;
        }


        public static void PostCancellStatus()
        {
            try
            {
                String sql = $"update  [ambientalis].[dbo].[orcamentos] set  status ='Arquivar'  where status = 'Cancelado' and id = 6017";
                FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
                fabrica.GetDAOBase().ExecutarComandoSql(sql);
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
    }
}
