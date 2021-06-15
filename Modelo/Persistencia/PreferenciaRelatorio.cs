using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class PreferenciaRelatorio: ObjetoBase
    {
        # region __________________ Padrão ____________________

        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static PreferenciaRelatorio ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            PreferenciaRelatorio classe = new PreferenciaRelatorio();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<PreferenciaRelatorio>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual PreferenciaRelatorio ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<PreferenciaRelatorio>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual PreferenciaRelatorio Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<PreferenciaRelatorio>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual PreferenciaRelatorio SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<PreferenciaRelatorio>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<PreferenciaRelatorio> SalvarTodos(IList<PreferenciaRelatorio> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<PreferenciaRelatorio>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<PreferenciaRelatorio> SalvarTodos(params PreferenciaRelatorio[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<PreferenciaRelatorio>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<PreferenciaRelatorio>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<PreferenciaRelatorio>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<PreferenciaRelatorio> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            PreferenciaRelatorio obj = Activator.CreateInstance<PreferenciaRelatorio>();
            return fabrica.GetDAOBase().ConsultarTodos<PreferenciaRelatorio>(obj);
        }

        /// <summary>
        /// Filtra uma certa quantidade de PreferenciaRelatorio
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<PreferenciaRelatorio> Filtrar(int qtd)
        {
            PreferenciaRelatorio estado = new PreferenciaRelatorio();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<PreferenciaRelatorio>(estado);
        }

        /// <summary>
        /// Retorna o ultimo PreferenciaRelatorio Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo PreferenciaRelatorio</returns>
        public virtual PreferenciaRelatorio UltimoInserido()
        {
            PreferenciaRelatorio estado = new PreferenciaRelatorio();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<PreferenciaRelatorio>(estado);
        }

        #endregion

        public static IList<string> ConsultarPreferencias(string urlTela, Funcionario funcionario)
        {
            PreferenciaRelatorio aux = new PreferenciaRelatorio();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.CriarAlias("Tela", "T"));
            aux.AdicionarFiltro(Filtros.Eq("T.Url", urlTela));
            aux.AdicionarFiltro(Filtros.CriarAlias("Funcionario", "U"));
            aux.AdicionarFiltro(Filtros.Eq("U.Id", funcionario.Id));

            IList<PreferenciaRelatorio> lista = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<PreferenciaRelatorio>(aux);
            if (lista != null && lista.Count > 0)
            {
                return lista[lista.Count - 1].Preferencia.Split(';').ToList<string>();
            }
            return new List<string>();
        }

        public static PreferenciaRelatorio Consultar(string urlTela, Funcionario funcionario)
        {
            PreferenciaRelatorio aux = new PreferenciaRelatorio();
            aux.AdicionarFiltro(Filtros.Distinct());
            aux.AdicionarFiltro(Filtros.CriarAlias("Tela", "T"));
            aux.AdicionarFiltro(Filtros.Eq("T.Url", urlTela));
            aux.AdicionarFiltro(Filtros.CriarAlias("Funcionario", "U"));
            aux.AdicionarFiltro(Filtros.Eq("U.Id", funcionario.Id));

            IList<PreferenciaRelatorio> lista = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<PreferenciaRelatorio>(aux);
            if (lista != null && lista.Count > 0)
                return lista[lista.Count - 1];
            return null;
        }
    }
}
