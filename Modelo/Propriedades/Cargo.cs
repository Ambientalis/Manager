using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.Cargo, Modelo", Table = "cargos")]
    public partial class Cargo: ObjetoBase
    {
        public Cargo(int id) { this.Id = id; }
        public Cargo(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Cargo() { }

        #region ___________ Atributos ___________

        private string nome;
        private IList<Funcao> funcoes;

            
        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Funcoes", Table = "funcoes", Cascade = "delete")]
        [Key(2, Column = "id_cargo")]
        [OneToMany(3, Class = "Modelo.Funcao, Modelo")]
        public virtual IList<Funcao> Funcoes
        {
            get { return funcoes; }
            set { funcoes = value; }
        }

        #endregion
    }
}
