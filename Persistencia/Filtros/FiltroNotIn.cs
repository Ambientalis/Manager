using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NHibernate.Criterion;
using Persistencia.Modelo;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para negacao
    /// </summary>
    public class FiltroNotIn : Filtro
    {
        /// <summary>
        /// Construtor para criar o filtro
        /// </summary>
        /// <param name="obj">Lista ou objeto a ser(em) negado(s)</param>
        public FiltroNotIn(object obj)
        {
            this.Valor1 = obj;
        }

        /// <summary>
        /// Adiciona o filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.Add(Expression.Not(Expression.In("Id", this.CarregarIdentificadores())));
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
