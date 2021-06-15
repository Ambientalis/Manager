using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "cidades", Name = "Modelo.Cidade, Modelo")]
    public partial class Cidade : ObjetoBase
    {
        public Cidade(int id) { this.Id = id; }
        public Cidade(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Cidade() { }

        #region ___________ Atributos ___________

        private string nome;
        private Estado estado;

        #endregion

        [ManyToOne(Name = "Estado", Column = "id_estado", Class = "Modelo.Estado, Modelo")]
        public virtual Estado Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
