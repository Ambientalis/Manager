using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "tipos_reservas_veiculos", Name = "Modelo.TipoReservaVeiculo, Modelo")]
    public partial class TipoReservaVeiculo: ObjetoBase
    {
        public TipoReservaVeiculo(int id) { this.Id = id; }
        public TipoReservaVeiculo(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public TipoReservaVeiculo() { }

        #region ___________ Atributos ___________

        private string descricao;
        private bool tipoVisitaOS;
        private IList<Reserva> reservas;

        #endregion

        #region _________ Propriedades __________

        [Property(Column = "descricao")]
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Column = "tipo_visita_os", Type = "TrueFalse")]
        public virtual bool TipoVisitaOS
        {
            get { return tipoVisitaOS; }
            set { tipoVisitaOS = value; }
        }

        [Bag(Name = "Reservas", Table = "reservas", Cascade = "delete")]
        [Key(2, Column = "id_tipo_reserva_veiculo")]
        [OneToMany(3, Class = "Modelo.Reserva, Modelo")]
        public virtual IList<Reserva> Reservas
        {
            get { return reservas; }
            set { reservas = value; }
        }

        #endregion
    }
}
