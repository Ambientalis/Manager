using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Subclass(Name = "Modelo.Receita, Modelo", Extends = "Modelo.MovimentacaoFinanceira, Modelo", DiscriminatorValue = "receita")]
    public partial class Receita : MovimentacaoFinanceira
    {
        public Receita(int id)
        {
            this.Tipo = MovimentacaoFinanceira.ENTRADA;
            this.Id = id;
        }
        public Receita(Object id)
        {
            this.Tipo = MovimentacaoFinanceira.ENTRADA;
            this.Id = Convert.ToInt32("0" + id.ToString());
        }
        public Receita()
        {
            this.Tipo = MovimentacaoFinanceira.ENTRADA;
        }
    }
}
