using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Agrupa uma função de filtro a ser adicionada a uma consulta
    /// </summary>
    public abstract class Filtro : IFiltro
    {
        private String _atributo = null;
        private Object _valor1 = null;
        private Object _valor2 = null;

        /// <summary>
        /// String que representa o Nome de um atributo
        /// </summary>
        protected String Atributo
        {
            get { return _atributo; }
            set { _atributo = value; }
        }

        /// <summary>
        /// Objeto que representa um valor a ser utilizado em um filtro
        /// </summary>
        protected Object Valor1
        {
            get { return _valor1; }
            set { _valor1 = value; }
        }

        /// <summary>
        /// Objeto que representa um outro valor a ser utilizado em filtros 
        /// que necessitem de mais de um valor
        /// </summary>
        protected Object Valor2
        {
            get { return _valor2; }
            set { _valor2 = value; }
        }


        /// <summary>
        /// Adiciona um filtro em um objeto Criteria para consultas com filtro
        /// </summary>
        /// <param name="c">Criteria onde será adicionado o filtro</param>
        public abstract void adicionarFiltro(ref ICriteria c);

    }
}