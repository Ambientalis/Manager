using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.ClassificacaoCliente, Modelo", Table = "classificacoes_clientes")]
    public partial class ClassificacaoCliente : ObjetoBase
    {
        public ClassificacaoCliente(int id) { this.Id = id; }
        public ClassificacaoCliente(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public ClassificacaoCliente() { }

        #region ___________ Atributos ___________

        private int classificacao;
        private DateTime data;
        private Cliente cliente;

        #endregion

        #region __________ Propriedades _________

        [Property(Column = "classificacao")]
        public virtual int Classificacao
        {
            get { return classificacao; }
            set { classificacao = value; }
        }

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        [ManyToOne(Name = "Cliente", Class = "Modelo.Cliente, Modelo", Column = "id_cliente")]
        public virtual Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        #endregion
    }
}
