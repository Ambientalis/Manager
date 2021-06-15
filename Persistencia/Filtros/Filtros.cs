using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Classe contendo os métodos estáticos de criação e configuração de filtros
    /// </summary>
    public class Filtros
    {
        /// <summary>
        /// Cria um fitro de comparação de igualdade
        /// </summary>
        /// <param name="atributo">O Nome do atributo a ser comparado</param>
        /// <param name="valor">O valor que este atributo deve ter no banco de dados</param>
        public static Filtro Eq(String atributo, Object valor)
        {
            return new FiltroEq(atributo, valor);
        }

        /// <summary>
        /// Cria um filtro de comparação de parte de string
        /// </summary>
        /// <param name="atributo">O Nome do atributo a ser comparado</param>
        /// <param name="valor">Parte do valor que o atributo deve ter, por exemplo,
        /// au poderia retornar paulo, augusto, etc</param>
        public static Filtro Like(String atributo, Object valor)
        {
            return new FiltroLike(atributo, valor);
        }

        /// <summary>
        /// Cria um fitro de comparação entre dois valores
        /// </summary>
        /// <param name="atributo">Nome do atributo a ser comparado</param>
        /// <param name="valorInicial">O valor mínimo (inicial) que este atributo deve ter</param>
        /// <param name="valorFinal">O valor máximo (final) que este atributo deve ter</param>
        public static Filtro Between(String atributo, Object valorInicial, Object valorFinal)
        {
            return new FiltroBetween(atributo, valorInicial, valorFinal);
        }

        /// <summary>
        /// Cria um alias (apelido) para uma associação. 
        /// Por exemplo, um objeto Cliente tem uma associação com uma coleção de Funcionalidades
        /// com o Nome Funcionalidades.
        /// <para>
        /// Fazendo CriarAlias(cliente,"Funcionalidades", "Funcionalidade") está se criando
        /// um alias (Funcionalidade) respresentado a associaçao do cliente com as funcionalidades
        /// <para>
        /// Com isso é possível utilizar outro filtro comparando atributos da Funcionalidade do cliente
        /// </para>
        /// </para>
        /// </summary>
        /// <param name="nomeAssociacao">O Nome dado à associação (atributo que a representa)</param>
        /// <param name="alias">O Nome do alias que representará a associação</param>
        public static Filtro CriarAlias(String nomeAssociacao, String alias)
        {
            return new FiltroCreateAlias(nomeAssociacao, alias);
        }

        /// <summary>
        /// Cria um filtro de consulta em exemplo, onde um objeto com alguns valores de atributos preenchidos
        ///  é passado (o exemplo) e é retornado todos os objetos que possuem aqueles valores em seus
        ///  atributos.
        ///  <para>
        ///  As associações não são utilizadas neste tipo de consulta
        ///  </para>
        /// </summary>
        /// <param name="objetoExemplo">O objeto com alguns atributos preenchidos (o objeto de exemplo)</param>
        public static Filtro Example(Object objetoExemplo)
        {
            return new FiltroExample(objetoExemplo);
        }

        /// <summary>
        /// Define a quantidade máxima de resultados a ser retornada por uma consulta
        /// </summary>
        /// <param name="quantidadeResultadosMax">A quantidade máxima de resultados</param>
        public static Filtro MaxResults(int quantidadeResultadosMax)
        {
            return new FiltroMaxResults(quantidadeResultadosMax);
        }

        /// <summary>
        /// Define uma ordem ascendente para um atributo em uma consulta
        /// </summary>
        /// <param name="atributo">O atributo que será ordenado ascendentemente</param>
        public static Filtro SetOrderAsc(String atributo)
        {
            return new FiltroOrderAsc(atributo);
        }

        /// <summary>
        /// Define uma ordem descendente para um atributo em uma consulta
        /// </summary>
        /// <param name="atributo">O atributo que será ordenado descendentemente</param>
        public static Filtro SetOrderDesc(String atributo)
        {
            return new FiltroOrderDesc(atributo);
        }

        /// <summary>
        /// Define uma subconsulta sobre uma consulta
        /// </summary>
        /// <param name="atributo">O nome da associação</param>
        public static Filtro SubConsulta(String atributo)
        {
            return new FiltroCreateCriteria(atributo);
        }

        /// <summary>
        /// Define uma subconsulta sobre uma consulta, porém definindo o alias
        /// </summary>
        /// <param name="atributo">O nome da associação</param>
        /// <param name="atributo">O alias que representará a associação</param>
        public static Filtro SubConsulta(String atributo, String alias)
        {
            return new FiltroCreateCriteria(atributo, alias);
        }

        /// <summary>
        /// Define que o resultado não deve ter objetos repetidos
        /// </summary>
        public static Filtro Distinct()
        {
            return new FiltroDistinct();
        }

        /// <summary>
        /// Define um filtro para consultar objetos que não pertencem a uma lista
        /// </summary>
        /// <param name="obj">Lista ou objeto a ser(em) negado(s)</param>
        public static Filtro NotIn(object obj)
        {
            return new FiltroNotIn(obj);
        }

        /// <summary>
        /// Define um filtro para consultar objetos que têm atributos que pertencem a lista passada
        /// </summary>
        /// <param name="atributo">O nome do atributo que deve conter na lista</param>
        /// <param name="lista">A lista de possíveis valores para o atributo</param>
        public static Filtro In(String atributo, Object lista)
        {
            return new FiltroIn(atributo, lista);
        }

        /// <summary>
        /// Filtro para retornar o ojeto que tem a maior propriedade passada
        /// </summary>
        /// <param name="propriedade">A propriedade que deve ser consultada</param>
        public static Filtro Max(object propriedade)
        {
            return new FiltroMax(propriedade.ToString());
        }

        /// <summary>
        /// Filtro para retornar o ojeto que tem a maior propriedade passada
        /// </summary>
        /// <param name="propriedade">A propriedade que deve ser consultada</param>
        public static Filtro Min(object propriedade)
        {
            return new FiltroMin(propriedade.ToString());
        }

        /// <summary>
        /// Filtro para retornar os objetos cujo atributo passado seja nulo
        /// </summary>
        /// <param name="atributo">o atributo que deve ser nulo</param>
        public static Filtro IsNull(String atributo)
        {
            return new FiltroIsNull(atributo);
        }

        /// <summary>
        /// Filtro para retornar os objetos cujo atributo passado não tem valor nulo
        /// </summary>
        /// <param name="atributo">o atributo que não pode ser nulo</param>
        public static Filtro IsNotNull(String atributo)
        {
            return new FiltroIsNotNull(atributo);
        }

        /// <summary>
        /// Define uma uma associação para ser trazida do banco ávidamente
        /// </summary>
        /// <param name="atributo">A associação que virá ávidamente</param>
        public static Filtro FetchJoin(String atributo)
        {
            return new FiltroFetchJoin(atributo);
        }

        /// <summary>
        /// Filtro para retornar os objetos cujo o valor do atributo passado seja maior que o valor de referência
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <param name="valor">O valor de comparação</param>
        public static Filtro Maior(String atributo, Object valor)
        {
            return new FiltroMaior(atributo, valor);
        }

        /// <summary>
        /// Filtro para retornar os objetos cujo o valor do atributo seja maior ou igual que o valor de referência
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <param name="valor">O valor de comparação</param>
        public static Filtro MaiorOuIgual(String atributo, Object valor)
        {
            return new FiltroMaiorOuIgual(atributo, valor);
        }

        /// <summary>
        /// Filtro para retornar os objetos cujo o valor do atributo seja menor que o valor de referência
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <param name="valor">O valor de comparação</param>
        public static Filtro Menor(String atributo, Object valor)
        {
            return new FiltroMenor(atributo, valor);
        }

        /// <summary>
        /// Filtro para retornar os objetos cujo o valor do atributo seja menor ou igual que o valor de referência
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <param name="valor">O valor de comparação</param>
        public static Filtro MenorOuIgual(String atributo, Object valor)
        {
            return new FiltroMenorOuIgual(atributo, valor);
        }

        /// <summary>
        /// Filtro para retornar a soma de um atributo
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <returns>Um objeto que representa a somatório </returns>
        public static Filtro Soma(String atributo)
        {
            return new FiltroSoma(atributo);
        }

        /// <summary>
        /// Filtro para retornar a quantidade de objetos persistidos
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <returns>Um objeto que representa a quantidade </returns>
        public static Filtro Count(String atributo)
        {
            return new FiltroCount(atributo);
        }

        /// <summary>
        /// Filtro para retornar o valor máximo de um atributo
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <returns>Um objeto que representa o valor máximo </returns>
        public static Filtro ValorMaximo(String atributo)
        {
            return new FiltroValorMaximo(atributo);
        }

        /// <summary>
        /// Filtro para retornar o valor mínimo de um atributo
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <returns>Um objeto que representa o valor mínimo</returns>
        public static Filtro ValorMinimo(String atributo)
        {
            return new FiltroValorMinimo(atributo);
        }

        /// <summary>
        /// Filtro para especificar um valor que um objeto não pode ter
        /// </summary>
        /// <param name="atributo">O atributo de referência</param>
        /// <returns>O valor do atributo que não pode estar na lista</returns>
        public static Filtro NaoIgual(String atributo, Object valor)
        {
            return new FiltroNaoIgual(atributo, valor);
        }

        /// <summary>
        /// Filtro para especificar uma faixa de valores a serem retornados em uma lista
        /// </summary>
        /// <param name="inicio">O valor numérico do inicio da lista</param>
        /// <param name="fim">O valor numérico do fim da lista</param>
        /// <returns>A faixa de objetos entre a faixa de valores definida</returns>
        /// <!--Esse filtro não guarda os objetos no cache-->
        public static Filtro FaixaResultado(int inicio, int fim)
        {
            return new FiltroFaixaResultado(inicio, fim);
        }

        /// <summary>
        /// Filtro que especifica se os objetos consultados devem ser mantidos no cache
        /// </summary>
        /// <param name="manterObjetosNoCache">O valor boleano que indica se os objetos devem ficar no cache</param>
        /// <returns>Definição se os objetos da consulta ficarão no cache</returns>
        public static Filtro Cache(bool manterObjetosNoCache)
        {
            return new FiltroCache(manterObjetosNoCache);
        }

        /// <summary>
        /// Filtro que implementa um ou entre filtros
        /// </summary>
        /// <param name="filtros">FiltroEq, FiltroNaoIgual, FiltroPropriedadeNaoIgual, FiltroLike, FiltroMaior, FiltroMaiorOuIgual, FiltroMenor, FiltroMenorOuIgual, FiltroBetween, FiltroIsNull, FiltroNotNull</param>
        /// <returns>Filtra de acordo com os filtros passados</returns>
        public static Filtro Ou(params Filtro[] filtros)
        {
            try
            {
                filtros.Cast<IFiltroOu>().ToList<IFiltroOu>();
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("Alguns filtros passados como parâmetro não implementam a interface IFitrolOu");
            }
            return new FiltroOu(filtros);
        }
    }
}
