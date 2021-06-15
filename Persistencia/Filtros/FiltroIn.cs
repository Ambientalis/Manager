using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;
using Persistencia.Modelo;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para consultar uma lista de objetos onde um de seus atributos está em uma lista passada
    /// </summary>
    public class FiltroIn : Filtro
    {
        /// <summary>
        /// Construtor do filtro
        /// </summary>
        /// <param name="atributo">O atributo a ser verificado</param>
        /// <param name="lista">A lista com os possíveis valores do atributo</param>
        public FiltroIn(String atributo, object lista)
        {
            this.Atributo = atributo;
            this.Valor1 = lista;
        }

        /// <summary>
        /// Adiciona o filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref ICriteria c)
        {
            c.Add(Expression.In(Atributo, this.CarregarIdentificadores()));
        }

        /// <summary>
        /// recupera tos os identificadores de uma lista
        /// </summary>
        /// <returns></returns>
        private IList CarregarIdentificadores()
        {
            IList lista = new List<int>();

            try
            {
                lista.Add(((ObjetoBase)Valor1).Id);
            }
            catch (Exception)
            {
                foreach (ObjetoBase ob in (IList)this.Valor1)
                {
                    lista.Add(ob.Id);
                }
            }
            return lista;
        }
    }
}
