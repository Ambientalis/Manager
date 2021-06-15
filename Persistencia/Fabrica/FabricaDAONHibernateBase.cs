using Persistencia.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Fabrica
{
    /// <summary>
    /// Fábrica de DaoNHibernate
    /// </summary>
    public class FabricaDAONHibernateBase
    {
        /// <summary>
        /// Obtém um DAOBase específico para NHibernate
        /// </summary>
        /// <returns></returns>
        public IDAOBase GetDAOBase()
        {
            return new DAOBaseNHibernate();
        }
    }
}
