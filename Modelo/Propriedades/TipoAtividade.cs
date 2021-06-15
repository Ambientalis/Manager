using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.TipoAtividade, Modelo", Table = "tipos_atividades")]
    public partial class TipoAtividade: ObjetoBase
    {
        public TipoAtividade(int id) { this.Id = id; }
        public TipoAtividade(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TipoAtividade() { }

        #region ___________ Atributos ___________

        private string nome;
        private IList<Atividade> atividades;
        
        #endregion

        #region __________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Atividades", Table = "atividades")]
        [Key(2, Column = "id_tipo_atividade")]
        [OneToMany(3, Class = "Modelo.Atividade, Modelo")]
        public virtual IList<Atividade> Atividades
        {
            get { return atividades; }
            set { atividades = value; }
        }

        #endregion
    }
}
