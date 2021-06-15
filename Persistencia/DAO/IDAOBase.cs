using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Modelo;
using System.Collections;

namespace Persistencia.DAO
{
    /// <summary>
    /// Interface que define os métodos gerais dos objetos DAOBase de cada tecnologia
    /// </summary>
    public interface IDAOBase
    {
        /// <summary>
        /// Consulta um objeto por um ID
        /// </summary>
        /// <param name="o">O objeto com o Id preenchido</param>
        /// <returns>O objeto consultado ou nulo</returns>
        T ConsultarPorID<T>(T o) where T : ObjetoBase;

        /// <summary>
        /// Exclui um objeto por um id, se o id não existir é lançada a exceção
        /// StaleStateException que deve ser tratada na interface
        /// </summary>
        /// <param name="o">O objeto a ser excluído com o id preenchido</param>
        bool Excluir<T>(T o) where T : ObjetoBase;

        /// <summary>
        /// Inclui um novo objeto ou altera um objeto existente. Se o id estiver vazio
        /// o objeto é incluido, se o id estiver preenchido e existir, o objeto é alterado,
        /// se o id estiver preenchido e não existeri é lançada a exceção
        /// StaleStateException que deve ser tratada na interface
        /// </summary>
        /// <param name="o">O objeto a ser salvo com o id preenchido</param>
        /// <returns>O objeto salvo</returns>
        T Salvar<T>(T o) where T : ObjetoBase;

        /// <summary>
        /// Salva um Objeto Com um Id Predefinido
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns>O Objeto Salvo</returns>
        T SalvarComId<T>(T o) where T : ObjetoBase;

        /// <summary>
        /// Dada uma lista de objetos, em cada um: Se o id estiver vazio
        /// o objeto é incluido, se o id estiver preenchido e existir, o objeto é alterado,
        /// se o id estiver preenchido e não existir é lançada a exceção
        /// StaleStateException que deve ser tratada na interface
        /// </summary>
        /// <param name="lista">A lista de objetos serem salvos com o id preenchido</param>
        /// <returns>A lista de objetos salvos</returns>
        IList<T> SalvarTodos<T>(IList<T> lista) where T : ObjetoBase;

        /// <summary>
        /// Obtém todos os objetos de um determinado tipo no banco de dados
        /// </summary>
        /// <param name="o">O tipo de objeto a ser consultado</param>
        /// <returns>A lista dos objetos retornados pelo BD</returns>
        IList<T> ConsultarTodos<T>(T o) where T : ObjetoBase;

        /// <summary>
        /// Executa uma consulta com filtro
        /// </summary>
        /// <param name="o">O objeto a ser consultado com sua lista de filtros preenchida</param>
        /// <returns>A lista de objetos retornados pelo BD</returns>
        IList<T> ConsultarComFiltro<T>(T o) where T : ObjetoBase;

        /// <summary>
        /// Executa uma consulta com filtro, porém retorna um único objeto
        /// </summary>
        /// <param name="o">O objeto a ser consultado com sua lista de filtros preenchida</param>
        /// <returns>O único resultado, null se não existir ou lança HibernateException se tiver mais de um</returns>
        T ConsultarUnicoComFiltro<T>(T o) where T : ObjetoBase;

        /// <summary>
        /// Destaca um objeto da sessão corrente
        /// </summary>
        /// <param name="o">O objeto a ser destacado</param>
        bool DestacarObjeto<T>(T o) where T : ObjetoBase;

        /// <summary>
        /// Executa uma SQl na Sessão Corrente
        /// </summary>
        /// <param name="s">A string que representa a Query</param>
        void ExecutarComandoSql(string s);

        /// <summary>
        /// Retorna um objeto resultado de um projeção
        /// </summary>
        /// <param name="o">O objeto com os filtros de projeção</param>
        /// <returns>Um valor que representa a projeção. Ex: Somatório de um atributo</returns>
        object ConsultarProjecao(ObjetoBase o);

        IList ConsultaSQL(string SQL);
    }
}
