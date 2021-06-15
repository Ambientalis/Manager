using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    public class FiltroValorMinimo : Filtro
    {
        public FiltroValorMinimo(String atributo)
        {
            this.Atributo = atributo;
        }

        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.SetProjection(Projections.Min(this.Atributo));
        }
    }
}
