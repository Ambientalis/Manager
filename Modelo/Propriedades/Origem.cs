using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Origem, Modelo", Table = "origens")]
    public partial class Origem : ObjetoBase
    {
        public Origem(int id) { this.Id = id; }
        public Origem(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Origem() { }

        #region ___________ Atributos ___________

        private string nome;
        private IList<Pessoa> pessoas;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Pessoas", Table = "pessoas")]
        [Key(2, Column = "id_origem")]
        [OneToMany(3, Class = "Modelo.Pessoa, Modelo")]
        public virtual IList<Pessoa> Pessoas
        {
            get { return pessoas; }
            set { pessoas = value; }
        }

        #endregion
    }
}
