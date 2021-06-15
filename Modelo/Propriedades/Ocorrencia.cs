using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Persistencia.Modelo;

namespace Modelo
{
    [Serializable]
    [Class(Table = "ocorrencias", Name = "Modelo.Ocorrencia, Modelo")]
    public partial class Ocorrencia: ObjetoBase
    {
        public Ocorrencia(int id) { this.Id = id; }
        public Ocorrencia(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public Ocorrencia() { }

        #region ___________ Atributos ___________

        private string descricao;
        private DateTime data;        
        private TipoOcorrenciaVeiculo tipoOcorrenciaVeiculo;
        private Reserva reserva;        

        #endregion

        #region _________ Propriedades __________

        [Property(Type = "StringClob")]
        [Column(1, Name = "descricao", SqlType = "text")]
        public virtual string Descricao
        {            
            get { return descricao; }
            set { descricao = value; }
        }

        [Property(Column = "data")]
        public virtual DateTime Data
        {
            get
            {
                if (data <= SqlDateTime.MinValue.Value)
                    return SqlDateTime.MinValue.Value;
                else
                    return data;
            }
            set { data = value; }
        }

        [ManyToOne(Class = "Modelo.TipoOcorrenciaVeiculo, Modelo", Column = "id_tipo_ocorrencia_veiculo")]
        public virtual TipoOcorrenciaVeiculo TipoOcorrenciaVeiculo
        {
            get { return tipoOcorrenciaVeiculo; }
            set { tipoOcorrenciaVeiculo = value; }
        }

        [ManyToOne(Class = "Modelo.Reserva, Modelo", Column = "id_reserva")]
        public virtual Reserva Reserva
        {
            get { return reserva; }
            set { reserva = value; }
        }

        
        #endregion
    }
}
