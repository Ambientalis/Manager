using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Name = "Modelo.TipoVisita, Modelo", Table = "tipos_visitas")]
    public partial class TipoVisita: ObjetoBase
    {
        public TipoVisita(int id) { this.Id = id; }
        public TipoVisita(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TipoVisita() { }

        #region ___________ Atributos ___________

        private string nome;
        private IList<Visita> visitas;
        
        #endregion

        #region _________ Propriedades __________

        [Property(Column = "nome")]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [Bag(Name = "Visitas", Table = "visitas", Cascade = "delete")]
        [Key(2, Column = "id_tipo_visita")]
        [OneToMany(3, Class = "Modelo.Visita, Modelo")]
        public virtual IList<Visita> Visitas
        {
            get { return visitas; }
            set { visitas = value; }
        }

        #endregion
    }
}
